using UnityEngine;
using System.Collections;

public class CommonFlags : MonoBehaviour {

    public bool IsEnabled
    {
        get;
        set;
    }

	// Use this for initialization
	void Awake () 
    {
        IsEnabled = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
