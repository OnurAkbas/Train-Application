# Train-Application
Uni Project

Evaluation Pros and Cons
This section will be a discussion between programming the software which will include pros and cons during the implementation of the application.
Pros
•	MySQL was hosted locally on the pc allowing me to manage my SQL with a lot of freedom during the implementation of adding, editing and deleting.
•	Having experience in coding in C# and having SQ*L knowledge allowed me to easily implement simple features and filling in tables for user implementation.
•	The Course User Interface Design gave me the advantage to create a user-friendly design with validation for each section, which is bug-free.
•	I had the ability to create small functions to keep the program tidy with methods to connect to the MYSQL, and a good way to have less lines on the software.
•	Implementing the algorithm, itself was easy once the data was sent over, with if statements adding up each distance one by one and making it work.

Cons
•	Web Service was not implemented but was attempted using the examples given and lecture notes. The quiz also helped us improve our knowledge on soap servers and how to call SQL lines using components.
•	Algorithm Implementation was a difficult section as having to send all the data using an array and looping all the information one by one and then extracting the information inside the component was one of the hardest sections. As trying different types of arrays, which didn’t support looping the data inside as the adapter didn’t support sending all at one go but looping inside was the only option. And then also its used to display the information. (used for Loop)
•	A difficult section was where the try and catch will close the connections, the connections was colliding together which was unable me to display another table. A lot of research after Finding out about Finally a section which is attached to try and catch and then its finally a good section to close your connections also catch a good way to tell the users there is a connection problem.
•	Foreign keys were a section which didn’t really work properly, but I had the idea to use it another way which worked perfect, the foreign keys didn’t assign as easy it would be on access database, I used Navicat Premium a software to manage your SQL data easily which didn’t support foreign keys.
•	SQL Query’s were very long and was meant to be tested slowly so the outcome of the query, I had to write the query and some mistakes will occur which I couldn’t notice so it was a difficult section to check one by one to make sure the queries had no mistakes.





Algorithm Implementation
This section will be a critical discussion of Shortest distance and fastest distance algorithm which is implemented into the software.
Shortest Distance 
The Shortest distance was implemented into the program, which will calculate the routes from one station to another by passing each one by one adding the distance together and then returns a value, the component checks for all the distance database and checks for the current connections, the method needs 3 arrays which is station1 which is the from and then station2 which is the To station and then the distance array, all of these are combined of one row of a SQL Line,  and then basic string for what location from and then to.
The loop repeats until it finds the starting point of the station and then the second station is saved and then continues to sum up until it reaches the station they want to go and once it arrives it returns the total distance added up and returns the value to user.
Having more time, would have allowed me to make through different lines and the shortest distance would have allowed me to make a shorter distance from the current.

Fastest Distance
The Fastest Route has not been implemented into the software the reason being is time, but I had an idea of how to do it. 
I would have made a loop which loops for ever until it returns all possible outcomes, and then the fastest route which delays are added up through changing the lines included with an estimated time given for users from all possible outcomes. And would give instructions how to go through all.

Conclusion
The Course Overall is a good way to improve how manage data, and implement different types of algorithms, this could be useful on the real world as Google maps use similar functionalities to calculate the fastest and shortest routes. And I recommend students to take this course to improve their coding skills and learn how to make complex systems.
