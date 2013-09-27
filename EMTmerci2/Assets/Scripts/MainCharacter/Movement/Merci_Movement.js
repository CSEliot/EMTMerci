#pragma strict

var Dead : boolean;
var target : Transform;
var HasPatient : boolean;
var AtTarget : boolean;
var TakingCover : boolean;
var AnimatingBones : Transform;

var RunSpeed : float = 5;
//var animator : Animator;

//visibility items variables
var GunOut : Transform;
var GunBack : Transform;
var LazerOut : Transform;
var Lazer2Out : Transform;
var SawOut : Transform;

enum merciState{IdleStill, RunningTo, Hiding, Operating, isDead
}
var state : merciState;

InvokeRepeating("StateLogic", 0, 0.05);

function Start () {

	yield StateMachine();

}

function StateLogic(){

		if(Dead == true){
			
			state = merciState.isDead;
			
		} else
		
		if((HasPatient == true) && (AtTarget == true)){
			
			state = merciState.Operating;
			
			
		}else if(TakingCover == true){ 
		
			state = merciState.Hiding;
			
			} else if(HasPatient == false){
				
					state = merciState.IdleStill;
				
				} else{
					
					state = merciState.RunningTo;
				}
				
//			Debug.Log(state);
	

}


function StateMachine(){
		while(true){
			switch(state){
				
				case merciState.isDead:
				isDead();
				break;
				
				case merciState.Operating:
				Operate();
				break;
				
				case merciState.Hiding:
				Hide();
				break;
				
				case merciState.IdleStill:
				Idle();
				break;
				
				case merciState.RunningTo:
				Running();
				break;
					}
				yield;
	
			}
			
				
			
			
			
			

}
	function isDead(){
			
			
		}
		
		
	function Operate(){
			
			AnimatingBones.SendMessage("Operate");
			GunBack.renderer.enabled = false;
			GunOut.renderer.enabled = true;
			LazerOut.renderer.enabled = true;
			Lazer2Out.renderer.enabled = true;
			SawOut.renderer.enabled = true;
			
		}
		
		
	function Hide(){
			
			
		}
		
		
	function Idle(){
			
			AnimatingBones.SendMessage("Idle");
			
		}
		
	function Running(){
		
		var controller : CharacterController = GetComponent(CharacterController);
		var RotationSpeed : float = 10;
		var NeededRotation = Quaternion.LookRotation(target.position - transform.position);
		
		
			transform.rotation = Quaternion.Slerp(transform.rotation, NeededRotation, Time.deltaTime * RotationSpeed);
			transform.rotation.x = 0;
			transform.rotation.z = 0;
			AnimatingBones.SendMessage("Run");
			GunBack.renderer.enabled = true;
			GunOut.renderer.enabled = false;
			LazerOut.renderer.enabled = false;
			Lazer2Out.renderer.enabled = false;
			SawOut.renderer.enabled = false;
			
			controller.SimpleMove(transform.TransformDirection(Vector3.forward * RunSpeed));
			

}


//Behavorial Interactions Below//

	function OnTriggerEnter(hit : Collider){
	
		if(hit.gameObject.transform == target){
		
		Camera.main.SendMessage("Operate");
		AtTarget = true;
		hit.gameObject.collider.enabled = false;
		
		
		}
	
	}


	function CubeHere(NewTarget){
	
		if(NewTarget != target){
			target.SendMessage("OnTriggerEnter");
			target.gameObject.collider.enabled = true;
			target = NewTarget;
			AtTarget = false;
			HasPatient = true;
		}
	}
	
	function Slow(){
	
		RunSpeed -= 10;
	}
	
	function Fast(){
	
		RunSpeed += 10;
	}
@script RequireComponent(Collider)