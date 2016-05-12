/*
Copyright (c) 2016 Gavin Coates  

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;


namespace dbAccess
{
    public class DBAccess
    {

        private string database;
        private OleDbConnection dbConnection;
        private string accessString;

        public bool error;
        public string lastError;

        public DBAccess(string database)
        {
            //
            // TODO: Add constructor logic here
            // //
            this.database = database;
            this.dbConnection = new OleDbConnection();
            connect();

            error = false;
            lastError = "";
        }

        private void connect()
        {
            // build the connection string
            accessString =  "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database + ";User Id=admin;Password=;";

            Console.WriteLine(accessString);


            // connect to the specified database
            
            dbConnection.ConnectionString = accessString;
            dbConnection.Open();

            
            

        }

        public DataTable performSELECT(string SQL)
        {
           
            // create a dbCommand based on the SQL
            OleDbCommand accessQuery = new OleDbCommand(SQL, this.dbConnection);
           


            DataTable resultTable = new DataTable("resultTable");

            try
            {


                OleDbDataReader reader = accessQuery.ExecuteReader();
               
                // create a table to hold the data


                // get all the info we need
                int noOfColumns = reader.FieldCount;

                // add the columns to the datatable
                for (int i = 0; i < noOfColumns; i++)
                {
                    DataColumn column = new DataColumn(reader.GetName(i),
                        reader.GetFieldType(i));
                    resultTable.Columns.Add(column);
                }

                // now that we have created the table, lets add each row
                while (reader.Read())
                {
                    DataRow row = resultTable.NewRow();
                    for (int i = 0; i < noOfColumns; i++)
                    {
                        row[i] = reader.GetValue(i);
                    }

                    // add the row to the DB
                    resultTable.Rows.Add(row);
                }

                // close the reader, we are done
                reader.Close();
                error = false;
                lastError = "";
            }
            catch (OleDbException e)
            {
                error = true;
                lastError = "You have an error in your SQL";
            }



            return resultTable;

        }


        public int performNonSELECT(string SQL)
        {
           
            // create a dbCommand based on the SQL
            OleDbCommand accessQuery = new OleDbCommand(SQL, dbConnection);
            int noOfRows = 0;

            try
            {


                noOfRows = accessQuery.ExecuteNonQuery();
                error = false;
                lastError = "";
            }
            catch (OleDbException e)
            {
                error = true;
                lastError = "You have an error in your SQL";
            }

            return noOfRows;
        }

        public void disconnect(OleDbConnection dbConnection)
        {
            
            dbConnection.Close();
        }

        public string getConnectionState()
        {
            ConnectionState state = dbConnection.State;
            return state.ToString();
        }

    }
}
