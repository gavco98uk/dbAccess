##Project: dbAccess
##Author: Gavin Coates
##License: MIT

Class library providing simple SQL query aibility for Access databases

dbAccess is a Class library I created many years ago to make it easier to deal with Access databases from C# applications. The library allows you to easily open a database and query it using SQL, returning the results as a well structured DataTable object.

This project is probably now redundant thanks to LINQ.

I have decided to release this project under the MIT license. I hope it is of use to someone. If you do find it useful, please let me know :)

##Example

    // Create a new instance, open and connect to the database
    var database = new DBAccess("database_name.mdb");

    // perform an SQL SELECT query, and store the results
    var result = database.performSELECT("SELECT * FROM Customers WHERE Balance > 1000");

    // this returns a DataTable object, with the columns and data types set as per the database
    // so now we can access them by name:
    foreach(var row in result)
    {
         Console.WriteLine(row["Name"] + " - " + row["Balance"])  ;
    }

    // for insert, update or delete commands, we use performNonSELECT
   result.performNonSELECT("INSERT INTO Customers (Name,Balance) VALUES ('Me',1000000)");

    // this returns the number of records affected, so you can check if it worked or not
    var count = result.performNonSELECT("DELETE FROM Customers WHERE Name = 'Dave'");

    if(count == 0)
       Console.WriteLine("Couldn't delete Dave");

