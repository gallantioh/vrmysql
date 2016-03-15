using UnityEngine;
using System.Collections;

public class click : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("an object");
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if (didHit)
            {
                Debug.Log(rhInfo.collider.name + "  " + rhInfo.point);

                //pushCube dest = rhInfo.collider.GetComponent<pushCube>();
                db dbcon = rhInfo.collider.GetComponent<db>(); 

                GameObject cubie = GameObject.FindGameObjectWithTag("Table");
                float x = rhInfo.point.x, y = rhInfo.point.y, z = rhInfo.point.z;

                if (dbcon)
                {
                        cubie.transform.position = new Vector3(x, y, z + 0.1f);
                }
            }
            else
            {
                Debug.Log("empty");
            }
        }
	}
}
