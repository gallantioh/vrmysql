using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
//using System.Exception;

public class database : MonoBehaviour {

    IDbConnection dbconn;
    string cString = "";
   // string userN = "";
   // string passW = "";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void connDb(bool open)
    {
        cString = "Server=localhost;Database=rmd;User ID=root;Password=123;Pooling=false;";
        //try {
            if (open == true)
            {
              dbconn = new MySqlConnection(cString);
              dbconn.Open();
            }
            else
            {
              dbconn.Close();
              dbconn = null;
            }
       /* }
        catch (Exception e) { }*/
    }
    //********************************************************************************************************************************
    //                                              TEMPORARY FOR TESTNG PURPOSES
    //********************************************************************************************************************************
  
}
