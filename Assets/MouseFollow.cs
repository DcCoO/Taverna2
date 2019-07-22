using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
    Transform t;

    private void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0)) 
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos = new Vector2(Mathf.Floor(pos.x), Mathf.Floor(pos.y));
            
            t.position = pos;
        }	
	}
}
