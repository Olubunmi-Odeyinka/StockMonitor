(A)Steps to Create and Initialize Database
There are 2 data schema i.e Asp.net User security which schema is dbo and the tables i created with schema sm.
For asp.net all that is required is to try to create a user and the database will be initialize for you
While for those tables i created please use the following steps
(1)Open package manager console and make sure the project selected is StockExchage.Data
(2) type into the console  =>  add-migration initial
(3) type into the console => update-database -verbose
(4) Run the script to insert data into database. The script is in source folder.
Note: please check connection string and make sure is pointing to your mssql server database. Presently is pointing to default database.
Also: Note that database connection are also on the Web and Service Projects.

(B)Steps to prepare the source code to build properly
Using Visual Studio 2015 or any visual studio from Visual Studio 2012 and above open the solution.
By default the start up projects should be StockExchage.Web and StockExchage.Service 
Just run and it should be good.
Please not incase any of the nugget package is not there please restore it.

(C)Any assumptions made and missing requirements that are not covered in the requirements
I couldn't not attempt the nice to have.

(C)I couldn't not attempt the nice to have because i ran out of time