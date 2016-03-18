using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class Presentation : MonoBehaviour {

    public database db;
    public GameObject go;
    float iniX, iniZ, fCurX, fCurZ,lCurX, lCurZ, finX, finZ;
    //Vector3 tPos;
    // Use this for initialization
    void Start () {

        db = FindObjectOfType(typeof(database)) as database;
        genTables();

        /**/
        iniX = 3.3f;//o.transform.position.x;
        iniZ = 5f;//o.transform.position.z;
        fCurX = iniX;
        fCurZ = iniZ;
        finX = 6.6f;//iniX + 5;
        finZ = 7f;//iniZ + 2;
        lCurX = finX;
        lCurZ = finZ;
       // tPos = new Vector3(finX, 0, iniZ);
      /* */
    }

    // Update is called once per frame
    void Update () {

        go = GameObject.Find("Table-0");
        roTables(go);
       
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void genTables()
    {
        int n = db.nOfTables("rmd");
        int i = 0;
        float xp = 3.3f, yp = 1.2f, zp = 5f;

        while (i < n) { 
        go = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        go.AddComponent<database>();
        go.AddComponent<pushCube>();
        go.name = "Table-" + i;
        //t1.tag = "Table";
        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        go.transform.position = new Vector3(xp++, yp, zp);
            i++;
    }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
      public void roTables(GameObject o)
      {

    if (fCurX < finX)
        {
            o.transform.Translate(Vector3.right * 0.01f);
            fCurX += 0.01f;
        }
       else if (fCurZ < finZ)
        {
            o.transform.Translate(Vector3.forward * 0.01f);
            fCurZ += 0.01f;
        } 
        else if (fCurX > iniX && !(lCurX < iniX))
        {
            o.transform.Translate(Vector3.left * 0.01f);
            lCurX -= 0.01f;
        }
        else if (fCurZ > iniZ && !(lCurZ < iniZ))
        {
            o.transform.Translate(Vector3.back * 0.01f);
            lCurZ -= 0.01f;
            if (lCurZ <= iniZ) { fCurZ = iniZ; lCurZ = finZ; fCurX = iniX; lCurX = finX; }
        }

      }/**/
}
