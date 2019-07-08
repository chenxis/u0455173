Author Chenxi Sun and Khang Liu


11/20/2019

Me and Khang looked over the assignment and decided the basic structure on how to finish this assignment. The first structure is the xml reader and a console
application that should output a prompt for a connection. So we decided we needed a server console app project and a servercontroller project that is the 
model of mvc. The servercontroller is a class library project that will dictate what message is sent to the client.

11/23/2019

We started working on the assignment together we first set up the xml reader in the server which will first output a prompt and read any setting file of the 
server. The Xml is just like the previous assignment where the reader is first created and the tags are used to decode the setting and set the setting of 
the server. We also added some helper functions in the networkController like serverawaitingclientloop and acceptnewclient which is the callback function.
We also setup some basic of the servercontroller like the servercontroller constructer and the parameters that will go into the servercontroller. I have set 
up a stack for the list of inactive socket if the player choose to disconnect and also a list of players. The list of players is a dictionary that will use the 
first come first serve as id. This will also be the socket id.

11/25/2019
We met again and worked on the recievedCommand function and update function the receivecommand function will get the stringbuffer from the socket and try to 
set the player action based on the command. The update will also include an extra feature delegate as the parameter. For this server assignment
we set up the extra feature as the moving star. Which will move if we set the setting xml file in the resource folder to true. So in the setting file 
the option of moving star has to be set to true inorder for the extra game mode to be unlocked. The update contains function on checking if the ship is turning 
left right thrust and firing. The server dictionary of ships will also need to be set to that action. The collision listener are also in update. The way we 
used to check on collision is to make sure that the ship size - projectile location are not less than ship location. The other helper function are withinbound
which we use to make sure if the ship are out of bound they appear on the other side and if projectile are out of bound they are set to dead.

12/01/2019
We have worked on it over the week and we added several methods to the servercontroller. This includes recieveName and createPlayer the recieveName will 
parse the name according to the regex and add a socket and make the connections. The createPlayer will put the player into the dictionary of players and 
set hp and location. The location will be spawned at a random location and will not collide with other ships and be at the frames. So today we finished the
assignment and set our extra features as the moving star. To activate this setting you have to go to the setting xml file in the resource folder. Change the 
movingstar to true to turn on the extra feature. We finished the assignment today.

12/03/2019
Today we did unit testing for the model of the program. This includes testing the world, the ship, the star, projectile and explosion. Only the model
can be tested as the server would need to startup the server to test which is only possible through video testing.

The extra option is the moving star which can be activated by going into the resource folder and open the  setting xml file. This can be done with a notepad
once in the movingstar need to be changed from false to true.