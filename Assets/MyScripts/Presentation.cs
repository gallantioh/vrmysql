using UnityEngine;
using System.Collections;
//using MySql.Data.MySqlClient;
using System;

public class Presentation : MonoBehaviour {

    public database db;
    public GameObject go, em;
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
            getTables(tb[i], tb[i].name);
                ///////////////////////////////////////////////////////////////
            i++;
            //}
        }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void genTables()
    {
        /*GameObject*/ em = new GameObject();
        em.transform.position = new Vector3(1f, 1f, 2f);
        em.name = "empty";

        int n = db.nOfTables("rmd");
        int i = 0;
        float xp = 3.3f, yp = 1.2f, zp = 5f;
        string[] tn = db.tableNames("rmd");

        while (i < n) { 
        go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        go.transform.SetParent(em.transform);
        go.AddComponent<database>();
        go.AddComponent<pushCube>();
        go.name = tn[i];// "Table-" + i;
        go.tag = "nTable";
        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        go.transform.position = new Vector3(xp++, yp, zp);
        i++;
         
    }
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
      public void roTables(GameObject o/*, float fx, float fz*/)
      {
        em.transform.Rotate(new Vector3(0f, 0.2f, 0f), 0.2f);
        /* try {
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
         }*/
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void getTables(GameObject go, string tn)// GO IS TABLESPHERE
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
                    int[] recs = db.nOfRecs(tn);
                    GameObject[] cubes = new GameObject[recs[0] * recs[1]];
                    cubes = createTable(xp, yp, zp, recs[0], recs[1]);
                    rowSelect(cubes, 0);
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
    public GameObject [] createTable(float xp, float yp, float zp, int rows, int cols)
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

        GameObject[] cubes = new GameObject[(rows * cols) + rows];

        for (int x = 0, i = 0; x < cols; x++, xp += 0.3f)
        {
            //row.tag = "Table";
            yp = go.transform.position.y;

          /*  GameObject col = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            col.name = "Col-" + x;
            col.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            col.transform.position = new Vector3(xp, yp + 1.3f, zp);*/

            for (int y = 0; y < rows; y++)
            {
                if (x == 0) {
                    GameObject row = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    row.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    row.transform.position = new Vector3(xp - 0.3f, yp + 1, zp);
                    row.name = "Row-" + y;
                    cubes[i++] = row;
                }
                
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.AddComponent<db>();
                cube.AddComponent<pushCube>();
                cube.name = x + "-" + y;
                cube.tag = "Table";
                cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                cube.transform.position = new Vector3(xp, yp + 1, zp);
                cubes[i++] = cube;
                yp -= 0.3f;
            }
        }
        //GameObject[] cubes = GameObject.FindGameObjectsWithTag("Table");
        return cubes;
    }
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*
    public void rowSelect(GameObject [] table, int recNum)
    {
        //recNum = +1;
        GameObject sel = null;
        for(int i = 0; i < table.Length; i++)
        {
            if (table[i].name.ToLower() == "row-" + recNum)
            {
                sel = table[i];
            }else if (i == table.Length - 1)
            {
                print(" The record your looking for doesn't exist, sorry!");
            }
        }

        if (sel != null)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].name.EndsWith(recNum + ""))
                {
                    table[i].transform.SetParent(sel.transform);
                }
            }
        }
    }
}
