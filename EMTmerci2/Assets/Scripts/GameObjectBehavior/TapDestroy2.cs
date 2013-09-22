using UnityEngine;
using System.Collections;

public class TapDestroy2 : TouchObject {
	
	public Transform WoundAnimator;
	
	// Use this for initialization
	public override void OnTap(GestureObject g)
    {
        print("ON TAP");
		WoundAnimator.SendMessage("OnSew");
	}
	
}
