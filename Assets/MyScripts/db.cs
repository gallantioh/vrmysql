using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

public class db : MonoBehaviour {

    IDbConnection dbcon;// = new MySqlConnection(cstring);

    // Use this for initialization
    void Start()
    {

        string cstring = "Server=localhost;Database=rmd;User ID=root;Password=123;Pooling=false;";
        dbcon = new MySqlConnection(cstring);
        dbcon.Open();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("I pushed");
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if (didHit)
            {
               // dbconn();
            }
            else
            {
                Debug.Log("jack");
            }
        }
    }

    public void dbMain()
    {
        /* string cstring = "Server=localhost;Database=rmd;User ID=root;Password=123;Pooling=false;";
         IDbConnection dbconn = new MySqlConnection(cstring);
         dbconn.Open();*/


        IDbCommand dbcmd = dbcon.CreateCommand();
        string sqlst = "SELECT * FROM a";
        dbcmd.CommandText = sqlst;

        IDataReader rd = dbcmd.ExecuteReader();
        while (rd.Read())
        {
            int num = (int) rd["num"];
            string fname = (string) rd["fname"];
            Debug.Log(num + " " + fname);
        }
        rd.Close();
        rd = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;

    }


}
