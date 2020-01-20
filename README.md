# Application-Domain-Project
Website to manage payments and transactions


To get the DB running, you may need to run the command "Update-Database" in the package manager console.
To add a new table to the DB, create a model class in the Models folder. Then, create a folder for that table in the Pages folder, right click and select "Add" > "New Scaffolded Item." Choose "Razor Pages" and select the "Razor Pages using Entity Framework (CRUD)." Click add, then select the class you just created in the Model Class dropdown. After that is done, run the command "Add-Migration [name]", where [name] is whatever name you want to give the update. Then run the "Update-DB" command. 
To add data to the DB, navigate to [default url]/[name of the folder containing the scaffolded pages]
