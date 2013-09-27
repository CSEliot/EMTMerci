using UnityEngine;
using System.Collections;

public class CubeHere : TouchObject {
	

	public Transform TouchReceiver;

	
	// Use this for initialization
	public override void OnTap (GestureObject g) {
	
		TouchReceiver.SendMessage("CubeHere", this.transform);
		Camera.main.SendMessage("ResetCam");
		Camera.main.SendMessage("Norm");
		BoxCollider boxcol = GetComponent<BoxCollider>();
        boxcol.size = new Vector3(1.2f, 1.2f, 1.2f);

	}
	
	// Update is called once per frame
	public void OnTriggerEnter() {
		BoxCollider boxcol = GetComponent<BoxCollider>();
		boxcol.size = new Vector3(3.5f, 3.5f, 3.5f);
		
	}
	
}
