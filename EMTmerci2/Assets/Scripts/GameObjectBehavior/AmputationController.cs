﻿using UnityEngine;
using System.Collections;

public class AmputationController : TouchObject {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override void OnPull(GestureObject g)
    {
        print(g.StartPosition + " " + g.EndPosition);
        Vector3 tempStart = new Vector3(g.StartPosition.x, g.StartPosition.y, Camera.main.nearClipPlane + 5.0f);
        Vector3 tempEnd = new Vector3(g.StartPosition.x, g.StartPosition.y, Camera.main.nearClipPlane);
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(tempStart);
        Vector3 worldEnd = Camera.main.ScreenToWorldPoint(tempEnd);
        print(worldStart + " " + worldEnd);
        GetComponent<Sliceable>().SliceMesh(worldStart, worldEnd);
    }
}
