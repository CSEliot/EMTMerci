using UnityEngine;
using System.Collections;

public class CubeHere : TouchObject {
	

	public Transform TouchReceiver;

	
	// Use this for initialization
	public override void OnTap (GestureObject g) {
	
		TouchReceiver.SendMessage("CubeHere", this.transform);
		Camera.main.SendMessage("ResetCam");
		Camera.main.SendMessage("Norm");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
