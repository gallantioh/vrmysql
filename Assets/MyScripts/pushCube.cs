using UnityEngine;
using System.Collections;
using System;


public class pushCube : MonoBehaviour
{

    float boxW = 60f;
    float boxH = 25f;
    public GameObject targetObject;
    public GameObject [] targetObjects;
    public database ct;
    public string tName;

    public bool ShowGUI = false;
    public string TagToUse = "Table";
    public float radius = 1.5f;

    // Use this for initialization
    void Start()
    {
          targetObject = GameObject.FindGameObjectWithTag("Table");
          ct = FindObjectOfType(typeof(database)) as database;
       
    }

    // Update is called once per frame
    void Update()
    {

      //   for (int i = 0; i < targetObject.Length; i++)
      //    {
        if (!targetObject) { targetObject = null; }
        else if (targetObject)
        {
            Vector3 toTarget = (targetObject.transform.position - transform.position).normalized;
            if (Vector3.Dot(toTarget, transform.forward) > 0) { ShowGUI = true; }
            else { ShowGUI = false; }

            float dist = Vector3.Distance(transform.position, targetObject.transform.position);
            if (dist > radius) { targetObject = null; ShowGUI = false; }
        }

        Collider[] colls = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colls)
        {
            if (hit.tag == TagToUse && !targetObject)
            {
                targetObject = hit.gameObject;
                ShowGUI = true;
            }

        }

    }
  //}

    void OnGUI()
    {
        if (ShowGUI)
        {
              targetObjects = GameObject.FindGameObjectsWithTag("Table");
              GameObject[] sortedTOs = new GameObject[targetObjects.Length];
              int[] nt = ct.nOfRecs("a");

            for (int j = 0, l = 0; j < nt[0]; j++)
            {
               
                for (int k = 0; k < nt[1]; k++, l++)
                {
                sortedTOs[l] = GameObject.Find(k + "-" + j);
                }
                targetObjects = sortedTOs;
            }

            string [] sm = ct.getTableContent("a");
          
              for (int i = 0; i <  targetObjects.Length; i++)
              {
            if (targetObjects[i])
            {
               
                Vector2 TextLocation = Camera.main.WorldToScreenPoint(targetObjects[i].transform.position);

                TextLocation.y = Screen.height - TextLocation.y;
                TextLocation.x -= boxW * 0.5f;
                TextLocation.y -= boxH * 0.5f;
                    try { GUI.Box(new Rect(TextLocation.x, TextLocation.y, boxW, boxH), sm[i]); }
                    catch (Exception ) {; }//IndexOutOfRangeException

                }
        }
    }
}
}