# Nonogram Puzzle

### Contributors
* Athea DeLing
* Bia Jaitrong
* Quin Asselin

### Description
This application allows a user to create a nonogram puzzle. The user is able to select the size of a grid. Once the size has been selected the user is then given a grid of white squares. The user can then select the individual squares to change the color to black to create an image of their choice. Once the image is created the user is able to save the image. It is saved to the database, the user is then able to bring this back up.

### Technologies Used
* C#
* .NET 6 SDK
* SQL
* Entity Framework
* HTML
* CSS

### Setup Instructions
#### Database Install
1. To run this program you will need to install MySQL, to do so please follow this [link](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c/installing-and-configuring-mysql)
2. Once MySql is installed you can create your database to do so follow these [instructions](https://www.learnhowtoprogram.com/c-and-net-part-time/database-basics/introduction-to-mysql-workbench-creating-a-database) to create the initial database
3. You should name your database firstname_lastname with your first and last name

#### Running the program
1. Fork the repository to your own GitHub
2. Clone the newly forked repository on to your own personal computer
3. Once cloned open the file and open up your terminal
4. In the terminal navigate to Factory once the run **dotnet restore** this should add all need packages
5. You will then in the main folder and a new file called **appsettings.json**
6. Once added you will add the following code
``` json
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=firstname_lastname;uid=[uid];pwd=[pwd];"
  }
}
```
7. You will replace the firstname and lastname with your first and last name and the [uid] and [pwd] including the brackets you will replace with your user name and password for your SQL Workbench
8. Once that file is add in the terminal in Factory you will run **dotnet ef migrations add Setup** and **then dotnet ef database update** this will setup your database that you will use
9. Once you have completed all of the previous steps run **dotnet watch run**
10. Once the application is running to be able to add and access information, you will have to create an account and log in once you have done that you can use the application

### Known Bugs
There are no known bugs but there are unfinished pieces
* The grid that comes up after the information has been saved is a larger value then the original one created this is done so that in the future numbers can be added so a different user can guess the image od the nonogram.

### License
[GNU GPL 3.0](https://choosealicense.com/licenses/gpl-3.0/) Copyright (c) 01/27/2023 Athea DeLing, Bia Jaitrong, Quin Asselin 