#pragma strict

var Dead : boolean;
var target : Transform;
var HasPatient : boolean;
var AtTarget : boolean;
var TakingCover : boolean;
var AnimatingBones : Transform;

var RunSpeed : float = 5;
//var animator : Animator;



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
			
		}
		
		
	function Hide(){
			
			
		}
		
		
	function Idle(){
			
			AnimatingBones.SendMessage("Idle");
			
		}
		
	function Running(){
		
		var controller : CharacterController = GetComponent(CharacterController);
		var RotationSpeed : float = 5;
		var NeededRotation = Quaternion.LookRotation(target.position - transform.position);
		
		
			transform.rotation = Quaternion.Slerp(transform.rotation, NeededRotation, Time.deltaTime * RotationSpeed);
			transform.rotation.x = 0;
			transform.rotation.z = 0;
			AnimatingBones.SendMessage("Run");
			
			controller.SimpleMove(transform.TransformDirection(Vector3.forward * RunSpeed));
			

}


//Behavorial Interactions Below//

	function OnTriggerEnter(hit : Collider){
	
		if(hit.gameObject.transform == target){
		
		AtTarget = true;
		
		
		}
	
	}


	function CubeHere(NewTarget){
	
		if(NewTarget != target){
			target = NewTarget;
			AtTarget = false;
			HasPatient = true;
		}
	}
@script RequireComponent(Collider)