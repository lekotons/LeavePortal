# LeavePortal
A leave portal to capture leave request and approve

To be able to compile the solution you will need to create the database. Run the script located on the root folder db script.sql file.

The connection string uses the default sql and you can modify the web.config in located in the LeavePortal.Web folder to point to the 
correct instance of your database. If you are using the local sql instance, you will only add the username and password of sql instance, or
alternative not modifying the connection string will allow the application to use the default sql instance with windows credentials.

The following users have been loaded.

1. username: user1@test.com, password: Password@1, Role: Normal user
2. username: user2@test.com, password: Password@2, Role: Normal user
3. username: Manager1@test.com, password: Password@3, Role: Normal user
