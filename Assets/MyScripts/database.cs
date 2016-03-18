using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System;
//using System.Exception;

public class database : MonoBehaviour {

    IDbConnection dbconn;
    string cString = "";  // connection string
    //string dbName = "rmd"; // database name
    // string userN = "";
    // string passW = "";

    string[] tNames; // name of tables in the db

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-* 
    public string connDb(bool open) //ERRORS
    {
        cString = "Server=localhost;Database=rmd;User ID=root;Password=123;Pooling=false;";

        try {
            if (open == true)
            {
              dbconn = new MySqlConnection(cString);
              dbconn.Open();
                return dbconn.State.ToString();
            }
            else
            {
              dbconn.Close();
              //dbconn = null;
                return dbconn.State.ToString();
            }
        }
        catch (Exception e) { print(e.Message); }

        return dbconn.State.ToString();
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public int nOfTables(string dbName) // returns number of tables in the db
    {
        string lTables = "show tables from " + dbName + ";";
        IDbCommand nt;
        IDataReader rd;

        if (connDb(true).ToLower() == "open")
        {
            nt = dbconn.CreateCommand();
            nt.CommandText = lTables;
            rd = nt.ExecuteReader();
            int n = 0;
            while (rd.Read())
            {
                n++;
            }
            //int ntables = rd.GetInt32(0); // NOT SURE WHY CAUSES ERRORS!!
           
            nt.Dispose();
            rd.Close();
           connDb(false); // MIGHT CAUSE ERRORS!!

            return n;

        }
        else
        {
            return -1;
        }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*


    //********************************************************************************************************************************
    //                                              TEMPORARY FOR TESTNG PURPOSES
    //********************************************************************************************************************************

}
