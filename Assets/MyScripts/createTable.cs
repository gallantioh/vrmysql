using UnityEngine;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections;

public class createTable : MonoBehaviour
{

    IDbConnection dbconn;

    // Use this for initialization
    void Start()
    {
        string cstring = "Server=localhost;Database=rmd;User ID=root;Password=123;Pooling=false;";
        dbconn = new MySqlConnection(cstring);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if (didHit)
            {
                Debug.Log(rhInfo.collider.name + "  " + rhInfo.point);
                GameObject sph = GameObject.Find("tableSphere");

                if (rhInfo.collider.name.Equals("tableSphere"))
                {
                    //sph = GameObject.Find("tableSphere");
                    Destroy(sph, 0);
                    cTable();
                }
                else {
                    print("not a table!");
                }       
            }
            else
            {
                Debug.Log("empty");
            }
        }

    }

    void cTable()
    {
        //dbconn.Open();
        IDbCommand dbcmd1 = dbconn.CreateCommand();
        IDbCommand dbcmd2 = dbconn.CreateCommand();

        string sqlst = "SELECT COUNT(*) FROM a";
        string sqlcol = "SELECT COUNT(*) FROM information_schema.columns where table_name = 'a';";

        dbcmd1.CommandText = sqlst;
        dbcmd2.CommandText = sqlcol;

        IDataReader rd = dbcmd1.ExecuteReader();
        rd.Read();

        int rows = rd.GetInt32(0);
        rd.Close();
        // dbcmd.CommandText = sqlcol;
        rd = dbcmd2.ExecuteReader();
        rd.Read();
        int cols = rd.GetInt32(0);//rd.FieldCount;

        float xp, yp, zp;
        xp = GameObject.Find("tableSphere").transform.position.x;
        yp = GameObject.Find("tableSphere").transform.position.y;
        zp = GameObject.Find("tableSphere").transform.position.z;
        //string tx = 

        print("Number of Records: " + rows + ", Number of Columns: " + cols);

        for (var y = 0; y < cols; y++)
        {
            xp += 0.3f;
            yp = GameObject.Find("tableSphere").transform.position.y;
            for (var x = 0; x < rows; x++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                cube.AddComponent<db>();
                cube.AddComponent<pushCube>();
                cube.tag = "nTable";
                cube.transform.localScale = GameObject.Find("Cube_1-1").transform.localScale;
                cube.transform.position = new Vector3(xp, yp, zp);

                yp += 0.3f;
            }
        }

       

        rd.Close(); populate();
        rd = null;
        dbcmd1.Dispose();
        dbcmd1 = null;
        dbconn.Close();
        dbconn = null;

    }

    public string [] populate()
    {

        // 
        string cstring = "Server=localhost;Database=rmd;User ID=root;Password=123;Pooling=false;";
        dbconn = new MySqlConnection(cstring);
dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlst = "SELECT * FROM a";

        dbcmd.CommandText = sqlst;

        IDataReader rd = dbcmd.ExecuteReader();
        rd.Read();
        
        int rows = rd.GetInt32(0);
        rows = rows * 2; // 2 is the number of cells in a record
        string[] dtable = new string [rows];
        
        while (rd.Read())
        {
            int i = 0;
            int num = (int)rd["num"];
            dtable[i] = num.ToString();

            string fname = (string)rd["fname"];
            dtable[i+1] = fname;
           // print(dtable[i]+"   "+dtable[i + 1]);
            i++;
        }
       // print(dtable.Length);
        rd.Close();
        dbcmd.Dispose();
       // dbconn.Close();

        return dtable; // return an array of records
    }

}