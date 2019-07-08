
Author Chenxi Sun and Khang lieu


11/11/2018

Finished with the static NetworkController got most of the designs thought out. The network will juggle with the connection and send the string 
to gameController. The gameController will deserialize the json. I will use a frame timer to time the frame and redraw the world every 10 ms. This should
give the game a frame per second rate of 69. This is above the normal human eye sight which is 60 frames per seconds.

I would probably set up different projects for each item of ships , world, projectile, and player. The ship would have a dir and loc parameter and id for 
identification. The world class would need three dictionaries one for ships, one for projectiles, and one for stars. This is so the gamecontroller once deserialized
the json can create the object in the world and keep track of it. Keeping track of it is important especially if the projectile reaches the edge of the map.
Since the projectile is not suppose to wraparound the isActive is false but we have to know which projectile. The ships projectile and stars all have a 
id which is their key in their dictionary.

The gamecontroller was designed with the purpose of recieving the json string once the networkcontroller establishes a connection. The gamecontroller will 
deserialize the object based on the tags if it is "ship", "proj", or "star". The deserialize method will lock the world as it is a racing condition to deserialize
objects using json.

11/12/2018

We worked on this today and decided to add explosion as an extra feature. The explosion will be circles expanding to max radius and then die off. We finished the 
gamecontroller by first initiating contact with the server. We have set it up so you can send input to the server like (uparrow) and other movement keys. There are also 
features like getData called when recieving so the client can draw the objects. The processMessage is used to make sure the message recieved is complete not 
just a partial message. The deserialized is used on the message and objects are drawn and sent to the client.

We also did the drawingpanel the drawingpanel is a class of its own and is extended the panel property. The helper drawing functions are all located in the 
drawingpanel. the shipdrawer the drawobjectwithtransform which is drawing the world stardrawer etc. When the onpaint is used the world will be locked as there
are racing condition to draw each type of objects like ships stars or projectiles.

We also did the playerpanel. The player panel is essentially placed on the right side of the panel. The playerpanel should repaint when the hp of anyship 
is changed the lock again is the world while all of the ship for each player is counted and their hp is repainted in the hp part of the panel. The score of each
player is also painted.

11/13/2018
For today we finished the spacewarclient. The spacewarclient will need to have a form of connectbutton and a main panel which will place the drawing panel and 
a side panel that will place the playerpanel. The client will also need to establish connection once the connect button is clicked like the network controller
We put all of the game control inthe spacewar client so when a key is pressed the info is sent to the server through the game controller. We did 
when key is pressed and when key is not pressed to solve the problem of when multiple keys are pressed or when nonmovement keys are pressed.
The extra feature is the explosion which we did through an explosion drawer and a separate object named explosion. 
So we finished the entire project. We worked together on one computer with one as a driver and another as a navigator. We would swap the role every day we meet
So we can get a feel of each others skill. Overall we worked well together as a team. With me doing the driver section on drawing panel and player panel and network 
controller and khang doing the driver section on network controller and other class objects like ships etc. We both did the spacewarclient. Overall the client
will operate on direction of the server and will resize from the default world size if the server sends a different size.



