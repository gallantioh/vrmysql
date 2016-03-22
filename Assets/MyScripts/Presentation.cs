using UnityEngine;
using System.Collections;
//using MySql.Data.MySqlClient;
using System;

public class Presentation : MonoBehaviour {

    public database db;
    public GameObject go;
    float iniX, iniZ, fCurX, fCurZ,lCurX, lCurZ, finX, finZ;

    // Use this for initialization
    void Start () {

        db = FindObjectOfType(typeof(database)) as database;
        genTables();

        /**/
        iniX = 3.3f;//o.transform.position.x;
        iniZ = 5f;//o.transform.position.z;
        fCurX = iniX;
        fCurZ = iniZ;
        finX = 10f;//iniX + 5;
        finZ = 9f;//iniZ + 2;
        lCurX = finX;
        lCurZ = finZ;

        //db.nOfRecs("b");
    }

    // Update is called once per frame
    void Update () {

        GameObject [] tb = GameObject.FindGameObjectsWithTag("nTable");
        int i = 0;
        while (i < tb.Length) { 
       // if (go != null) { // need fixing cause it prevents the mouse info once table is gen
            roTables(tb[i]/*, 7f + i, 7f + i*/);
            ///////////////////////////////////////////////////////////////
            getTables(tb[i]);
                ///////////////////////////////////////////////////////////////
                i++;
            //}
        }
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
        go.tag = "nTable";
        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        go.transform.position = new Vector3(xp++, yp, zp);
            i++;
    }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
      public void roTables(GameObject o/*, float fx, float fz*/)
      {

        try {
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
        }
        catch (NullReferenceException)
        {
            //print(ex.Message);
        }
      }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void getTables(GameObject go)// GO IS TABLESPHERE
    {
        float xp, yp, zp;
       

        if (Input.GetMouseButtonDown(0)/* && go != null*/)
        {
            xp = go.transform.position.x;
            yp = go.transform.position.y;
            zp = go.transform.position.z;

            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if (didHit)
            {
                Debug.Log(rhInfo.collider.name + "  " + rhInfo.point);
                //GameObject sph = GameObject.Find("tableSphere");

                // TEMPORARY SOLUTION
                if (rhInfo.collider.name.Equals(go.name))//gives nullPointerEX when accesed again//if (go != null)
                {
                    //go.transform.Translate(new Vector3(0, 0, 3));
                    Destroy(go, 0);
                    //--------------------------------------------------------
                    createTable(xp, yp, zp, 5, 5);
                    //--------------------------------------------------------
                }
                else
                {
                    print("can't turn into a table!");
                }
            }
            else
            {
                Debug.Log("empty");
            }
        }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void createTable(float xp, float yp, float zp, int rows, int cols)
    {

        if (rows > 5)
        {
            //int xRows = rows;
            rows = 5;
        }
        if (cols > 5)
        {
          //  int xCols = cols;
            cols = 5;
        }

        for (int x = 0; x < cols; x++, xp += 0.3f)
        {
            //row.tag = "Table";
            yp = go.transform.position.y;

            GameObject col = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            col.name = "Col-" + x;
            col.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            col.transform.position = new Vector3(xp, yp + 1.3f, zp);

            for (int y = 0; y < rows; y++)
            {
                if (x == 0) {
                    GameObject row = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    row.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    row.transform.position = new Vector3(xp - 0.3f, yp + 1, zp);
                    row.name = "Row-" + y;
                }
                
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.AddComponent<db>();
                cube.AddComponent<pushCube>();
                cube.name = x + "-" + y;
                cube.tag = "Table";
                cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                cube.transform.position = new Vector3(xp, yp + 1, zp);
                yp -= 0.3f;
            }
        }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void rowSelect()
    {

    }
}
