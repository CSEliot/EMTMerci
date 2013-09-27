using UnityEngine;
using System.Collections;

public class touchSlow : TouchObject {

	
	public bool slowed;
	// Use this for initialization
	public override void OnTap(GestureObject g) {
	
		if(slowed == false){
		this.SendMessage("Slow");
		slowed = true;
		}else{
		this.SendMessage("Fast");
		slowed = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
