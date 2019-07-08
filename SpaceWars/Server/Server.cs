using NetworkController;

using System;
using System.Collections.Generic;
using System.IO;


using System.Threading.Tasks;
using System.Xml;

using System.Threading;



//Author Chenxi Sun and Khang Liu

namespace SpaceWars
{
    class Server
    {


        static void Main(string[] args)
        {


            string a = "abc";
            HashSet<string> a1 = new HashSet<string>();
            a1.Add(a);
            a=a.ToUpper();
            Console.WriteLine(a1.Contains(a));

            Console.WriteLine("Now Loading SpaceWars Server.");
            


            World world;
            ServerController.Settings settings;
            ReadSettings(out settings, out world); //grab settings from XML file. Defaults applied if a setting is not found
                                                   //world is generated based on settings.
            ServerController sc = new ServerController(settings, world);

            Thread runServer = new Thread(sc.StartGame); //start game in new thread
            runServer.Start();

            string input = Console.ReadLine();
            input = input.ToUpper();

            while (input != "STOP")
            {
                input = Console.ReadLine();
                input = input.ToUpper();
            }

            sc.StopGame();
        }

    /// <summary>
    /// read setting file if there
    /// 
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="world"></param>
        public static void ReadSettings(out ServerController.Settings settings, out World world)
        {
            //Default values - If the settings file does not contain these values, these settings will be applied by default
            int worldSize = 750;
            int MSPerFrame = 16;
            int FramesPerShot = 6;
            int RespawnRate = 300;

            int startingHitPoints = 5;
            int projectileSpeed = 15; //Units per frame
            double engineStrength = 0.08; //Units per frame
            double turningRate = 2; //Degrees
            int shipSize = 20; //Radius
            int starSize = 35; //Radius
            bool movingStars = false; //Extra feature

            world = new World();

            int starIDCounter = 0;

            //gets directory of project
            string parentLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string settingsLocation = parentLocation + "\\Resources\\settings.xml";

            try
            {
                //Using allows the reader to be disposed if an exception is thrown.
                //The outer try catch allows an exception to be caught (such as a FileNotFoundException, which
                // using or a single try/catch does not allow, as the reader cannot be disposed of for a bad filename)

                using (XmlReader reader = XmlReader.Create(settingsLocation))
                {
                    if (reader.Read() && reader.IsStartElement() && reader.Name == "SpaceSettings")
                    {
                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                switch (reader.Name)
                                {
                                    case "UniverseSize":
                                        worldSize = reader.ReadElementContentAsInt();
                                        break;

                                    case "MSPerFrame":
                                        MSPerFrame = reader.ReadElementContentAsInt();
                                        break;

                                    case "FramesPerShot":
                                        FramesPerShot = reader.ReadElementContentAsInt();
                                        break;

                                    case "RespawnRate":
                                        RespawnRate = reader.ReadElementContentAsInt();
                                        break;

                                    case "Star":
                                        bool readingStar = true;
                                        double x = 0;
                                        double y = 0;
                                        double mass = 0;

                                        while (readingStar)
                                        {
                                            reader.Read();
                                            if (reader.IsStartElement())
                                            {
                                                switch (reader.Name)
                                                {
                                                    case "x":
                                                        x = reader.ReadElementContentAsDouble();
                                                        break;
                                                    case "y":
                                                        y = reader.ReadElementContentAsDouble();
                                                        break;
                                                    case "mass":
                                                        mass = reader.ReadElementContentAsDouble();
                                                        readingStar = false;
                                                        break;
                                                }
                                            }
                                        }

                                        //Add the star to the world
                                        Vector2D loc = new Vector2D(x, y);
                                        world.Update(new Star(starIDCounter, loc, mass));
                                        starIDCounter++;
                                        break;

                                    case "StartingHitPoints":
                                        startingHitPoints = reader.ReadElementContentAsInt();
                                        break;

                                    case "ProjectileSpeed":
                                        projectileSpeed = reader.ReadElementContentAsInt();
                                        break;

                                    case "EngineStrength":
                                        engineStrength = reader.ReadElementContentAsDouble();
                                        break;

                                    case "TurningRate":
                                        turningRate = reader.ReadElementContentAsDouble();
                                        break;

                                    case "ShipSize":
                                        shipSize = reader.ReadElementContentAsInt();
                                        break;

                                    case "StarSize":
                                        starSize = reader.ReadElementContentAsInt();
                                        break;

                                    case "MovingStars":
                                        movingStars = reader.ReadElementContentAsBoolean();
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                       
                        Console.WriteLine("Settings file could not be properly read, using default values...");
                    }

                    reader.Close();
                }
            }
            catch (XmlException)
            {
                Console.WriteLine("Settings file could not be properly read, using default values...");

            }

            world.setWorldSize(worldSize);
            settings = new ServerController.Settings(worldSize, MSPerFrame, FramesPerShot, RespawnRate, startingHitPoints, projectileSpeed,
                engineStrength, turningRate, shipSize, starSize, movingStars);
        }


    }





}
