using UnityEngine;
using System.Collections;



public class TouchObject : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void OnTap(GestureObject g)
    {
		
    }

    public virtual void OnDrag(GestureObject g)
    {

    }

    public virtual void OnPull(GestureObject g)
    {

    }

    public virtual void OnPinch(GestureObject g)
    {

    }
	
	//Paul
	public virtual void OnScreenDrag(GestureObject g)
    {
		
    }
}
