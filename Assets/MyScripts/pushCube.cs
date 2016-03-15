using UnityEngine;
using System.Collections;

public class pushCube : MonoBehaviour
{

    float boxW = 60f;
    float boxH = 20f;
    public GameObject targetObject;
    public GameObject [] targetObjects;
    public createTable ct;

    public bool ShowGUI = false;
    public string TagToUse = "Table";
    public float radius = 1.5f;

    // Use this for initialization
    void Start()
    {
          targetObject = GameObject.FindGameObjectWithTag("Table");
        ct = FindObjectOfType(typeof(createTable)) as createTable;
        
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
              targetObjects = GameObject.FindGameObjectsWithTag("nTable");
              string [] sm = ct.populate();
            
           

              for (int i = 0, j = 0; j < sm.Length || i <  targetObjects.Length; i++, j++)
              {
            if (targetObjects[i])
            {
               
                Vector2 TextLocation = Camera.main.WorldToScreenPoint(targetObjects[i].transform.position);

                TextLocation.y = Screen.height - TextLocation.y;
                TextLocation.x -= boxW * 0.5f;
                TextLocation.y -= boxH * 0.5f;
                GUI.Box(new Rect(TextLocation.x, TextLocation.y, boxW, boxH), sm[j]);
            }
        }
    }
}
}