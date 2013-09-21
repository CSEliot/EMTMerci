using UnityEngine;
using System.Collections;

public class TapDestroy2 : MonoBehaviour {
	
	public Transform WoundAnimator;
	
	// Use this for initialization
	void OnMouseDown () {
	
		WoundAnimator.SendMessage("OnSew");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
