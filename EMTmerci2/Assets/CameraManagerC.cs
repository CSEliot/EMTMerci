using UnityEngine;
using System.Collections;

public class CameraManagerC : TouchObject {

	public SmoothLookAt LookAtScript;
	public Transform LookAtTarget;
	public float TargetNum = 2;
		
	public Transform camT1;
	public Transform camT2;
	public Transform camT3;
	public Transform camT4;
//	public Transform camT5;
//	public Transform camT6;
	
	
	public Vector3 SwipeVector;
	public bool SwipeOnce;
	public float SwipeMagnitude;
	
	public override void OnScreenDrag(GestureObject g){

		
		if(LookAtScript.target != LookAtTarget){
				LookAtScript.target = LookAtTarget;
				SwipeOnce = false;
				return;
		} else {
			
		
		SwipeVector = g.StartPosition - g.EndPosition;
		Debug.Log(SwipeVector.x);
		SwipeMagnitude = SwipeVector.x;
		if((SwipeMagnitude >= 100) && (TargetNum != 4) && (SwipeOnce == false)){
			
			TargetNum ++;
			SwipeOnce = true;
		} else if ((SwipeMagnitude >= 100) && (TargetNum == 4) && (SwipeOnce == false)){
			
			TargetNum = 1;
			SwipeOnce = true;
	
			}
		if((SwipeMagnitude <= -100) && (TargetNum != 1) && (SwipeOnce == false)){
			
			TargetNum --;
			SwipeOnce = true;
		} else if ((SwipeMagnitude <= -100) && (TargetNum == 1) && (SwipeOnce == false)){
			
			TargetNum = 4;
			SwipeOnce = true;
	
			}
		
		
		if(TargetNum == 1){
			
			LookAtTarget = camT1;
		}
		if(TargetNum == 2){
			
			LookAtTarget = camT2;
		}
		if(TargetNum == 3){
			
			LookAtTarget = camT3;
		}
		if(TargetNum == 4){
			
			LookAtTarget = camT4;
		}
//		if(TargetNum == 5){
//			
//			LookAtTarget = camT5;
//		}
//		if(TargetNum == 6){
//			
//			LookAtTarget = camT6;
//		}
		
		}
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
