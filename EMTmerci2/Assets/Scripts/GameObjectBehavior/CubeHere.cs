using UnityEngine;
using System.Collections;

public class CubeHere : TouchObject {
	
	public Transform TouchReceiver;
	
	// Use this for initialization
	public override void OnTap (GestureObject g) {
	
		TouchReceiver.SendMessage("CubeHere", this.transform);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
