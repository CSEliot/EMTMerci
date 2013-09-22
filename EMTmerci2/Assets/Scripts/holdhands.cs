using UnityEngine;
using System.Collections;

public class holdhands : MonoBehaviour {
	
public bool WristSnap = false;
public Transform WristHeld;
public Transform Parent;

	// Use this for initialization
	void WristPosParent () {
	
		WristSnap = true;
		transform.parent= null;
		transform.parent = WristHeld;
	}
	
	void ResetParent () {
	
//		transform.parent = Bones;
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
		if(WristSnap ==true ){
//			transform.position = WristHeld.position;
		}
		
	}
}
