using UnityEngine;
using System.Collections;



public class AnimationHandler : MonoBehaviour {

	public bool isSewed = false;
	public Animator animatorComp;
	
	// Update is called once per frame
	void OnSew () {
	
		isSewed = true;
		animatorComp.SetBool("isStitched", true);
	}
}
