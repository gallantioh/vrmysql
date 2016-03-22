using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System;
//using System.Exception;

public class database : MonoBehaviour {

    IDbConnection dbconn;
    string cString = "";  // connection string
    string dbName = "rmd"; // database name
    string userID = "root";
    string passW = "123";

    string[] tNames; // name of tables in the db

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-* 
    public string openConnDb() //ERRORS?
    {
        cString = "Server=localhost;Database=" + dbName + ";User ID=" + userID +
            ";Password=" + passW + ";Pooling=false;";

        try {
              dbconn = new MySqlConnection(cString);
              dbconn.Open();
              return dbconn.State.ToString();
        }
        catch (Exception e) { print("Connection failed and gave this Error: \"" + e + 
                                    "\"\nDatabase perhaps not started?");
        }

        return dbconn.State.ToString();
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-* 
    public string closeConnDb() //ERRORS?
    {
        try
        {
            dbconn.Close();
            //dbconn = null;
            return dbconn.State.ToString();
        }
        catch (Exception e)
        {
            print("Unable to Connection and  this Error was given: \"" + e +
                  "\"\nConnection perhaps not Opened yet?");
        }

        return dbconn.State.ToString();
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public int nOfTables(string dbName) // returns number of tables in the db
    {
        string sqlcmd = "show tables from " + dbName + ";";
        IDbCommand dbcmd;
        IDataReader read;

        if (openConnDb().ToLower() == "open")
        {
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlcmd;
            read = dbcmd.ExecuteReader();
            int n = 0;
             while (read.Read())
           {
                n++;
            }

            // int n = rd.GetInt32(0); // NOT SURE WHY CAUSES ERRORS!!

            dbcmd.Dispose();
            read.Close();
            closeConnDb();

            return n;
        }
        else
        {
            return -1;
        }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public string [] tableNames(string dbName)
    {
        string sqlcmd = "show tables from " + dbName + ";";
        IDbCommand dbcmd;
        IDataReader read;
        int n = nOfTables(dbName);

        if (openConnDb().ToLower() == "open")
        {
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlcmd;
            read = dbcmd.ExecuteReader();

            string[] tableNames = new string [n];
            int i = 0;

            while (read.Read())
            {
                tableNames[i] = (string)read["Tables_in_" + dbName];
                //print("table number "+i+" is named: "+tableNames[i]);
                i++;
            }
            return tableNames;
        }
        else
        {
            return null;
        }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void nOfRecs(string tName) // return recods number its void now change it
    {
        openConnDb();
            IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlcmd = "SELECT COUNT(*) FROM " + tName + ";" +
            "SELECT COUNT(*) FROM information_schema.columns where table_name = '" + tName + "';";
 
        try
        {
            if (openConnDb().ToLower() == "open")
            {
                dbcmd.CommandText = sqlcmd;
                IDataReader read = dbcmd.ExecuteReader();
                read.Read();

                int rows = read.GetInt32(0);
                read.NextResult();
                read.Read();
                int cols = read.GetInt32(0);

                read.Close();
                dbcmd.Dispose();

                print("rows: " + rows + "colomns: " + cols);
                closeConnDb();
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public string[] getTableContent(string tName)
    {
        openConnDb();
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlst = /*"SELECT COUNT(*) FROM information_schema.columns where table_name = '" + tName + "';"
            + */"SELECT COLUMN_NAME FROM information_schema.columns where table_name = '" + tName + "';"
            + "SELECT * FROM " + tName + ";" + "SELECT * FROM " + tName + ";";

        dbcmd.CommandText = sqlst;
        IDataReader read = dbcmd.ExecuteReader();

        // read.Read();
        int cols = 0;//, cols = (int) read[0];
        while (read.Read()) { cols++; }
       // string[] colNames = new string[cols];
        
        read.NextResult();
        //read.Read();
        int recs = 0;//= read.GetInt32(0);
        while (read.Read()) { recs++; }
        recs = recs * cols + 1; // 2 is the number of cells in a record
       
        read.NextResult();
        string [] dTable = new string[recs];
        int i = 0;

        while (read.Read())
        {
            for (int j = 0; j < cols; j++)
            {
                dTable[i] = read[j].ToString();
               if (dTable[i].Length > 8)
               {
                   dTable[i] = dTable[i].Substring(0, 6) + "..";
               } 
                i++;
            }
        }
        dTable[i] = cols + "";

        read.Close();
        dbcmd.Dispose();
        closeConnDb();

        return dTable; // return an array of records
    }
    //********************************************************************************************************************************
    //                                              TEMPORARY FOR TESTNG PURPOSES
    //********************************************************************************************************************************

}
