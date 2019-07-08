using System;
using NetworkController;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// 
/// @authur Chenxi Sun and Khang Lieu
/// </summary>

namespace SpaceWars
{
   
    public class GameController
    {
        private SocketState theServer;
        
        private World theWorld;
        private string playerName;
        private int playerID;
        private bool connectionLost;

        public bool Connectionlost { get => connectionLost;}

        /// <summary>
        /// Single constructor for a GameController object.
        /// </summary>
        /// <param name="world">World that this controller will be updating</param>
        public GameController(World world)
        {
            theWorld = world;
            connectionLost = false;
        }

 
        public void FirstContact(SocketState ss)
        {
            ss.callMe = ReceiveStartup;
            Network.Send(ss.theSocket, playerName);

           Network.GetData(ss);
        }

       
        public void ReceiveStartup(SocketState ss)
        {
            string totalData = ss.sb.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");

            playerID = int.Parse(parts[0]);
           
            ss.sb.Remove(0, parts[0].Length);

            theWorld.setWorldSize(int.Parse(parts[1]));
            
            ss.sb.Remove(0, parts[1].Length);


            ss.callMe = ProcessMessage;
            Network.GetData(ss);
        }

        
        public void ProcessMessage(SocketState ss)
        {
            string totalData = ss.sb.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");

            // Loop until we have processed all messages.
            // We may have received more than one.

            foreach (string p in parts)
            {
                // Ignore empty strings added by the regex splitter
                if (p.Length == 0)
                    continue;
                // The regex splitter will include the last string even if it doesn't end with a '\n',
                // So we need to ignore it if this happens. 
                if (p[p.Length - 1] != '\n')
                    break;

                //MessageBox.Show(p);
                JObject obj = JObject.Parse(p);
              
                
                    Deserialize("star", obj, p);
               
                    Deserialize("ship", obj, p);
               
                    Deserialize("proj", obj, p);

                // Then remove it from the SocketState's growable buffer
                ss.sb.Remove(0, p.Length);
            }

            try
            {
                Network.GetData(ss);
            }
            catch (Exception)
            {
                connectionLost = true;
            }
        }

        /// <summary>
        /// Deserialize is passed a string that contains ship, star, or proj and a JSON object.
        /// Function determines if the object contains data for a ship, star, or projectille and then sends it to the world so the object can be updated.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="obj"></param>
        public void Deserialize(string s, JObject obj, string message)
        {
            JToken token = obj[s];
            if (token != null)
            {
                lock (theWorld)
                {
                    switch (s)
                    {
                        case "star":
                            theWorld.Update(JsonConvert.DeserializeObject<Star>(message));
                            break;
                        case "ship":
                            theWorld.Update(JsonConvert.DeserializeObject<Ship>(message));
                            break;
                        case "proj":
                            theWorld.Update(JsonConvert.DeserializeObject<Projectile>(message));
                            break;
                    }
                }
            }
        }



        /// <summary>
        /// Start attempting to connect to the server.
        /// Will return true if the server could be connected to.
        /// Else false.
        /// </summary>
        /// <param name="hostName"> server to connect to </param>
        /// <returns></returns>
        public bool ConnectToServer(string hostName, string playerName)
        {
            this.playerName = playerName;
            try
            {
                theServer = Network.ConnectToServer(FirstContact, hostName);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Send keyboard commands
        /// </summary>
        /// <param name="hostName"> server to connect to </param>
        /// <returns></returns>
        public void SendInput(string input)
        {
            if (theServer != null)
            {
                Network.Send(theServer.theSocket, "(\"" + input + "\")\n");
            }
        }
    }
}
