//using UnityEngine;
//using System.Collections;
//
//
//enum aiState{IdleStill, RunningTo, Hiding, Operating, Dead
//}
//
//
//
//
//public class Merci_Movement : MonoBehaviour {
//	
////InvokeRepeating("StateLogic", 0.0, 0.05);
//	
//	
//	
//public aiState state;
//
//	void Awake(){
//	
//		animator = GetComponent<Animator;
//	
//}
//	
//	
//public Transform target;
//public bool HasPatient;
//public bool AtTarget;
//public bool TakingCover;
//
////RunningTo variables
//public float RunSpeed = 1;
//
//
//private Animator animator;
//	
//	
//	
//	// Use this for initialization
////	void Start () {
////	
////		yield StateMachine();
////		
////	}
////	
////	void StateLogic(){
////		
////		if(Dead == true){
////			
////			state = aiState.Dead;
////			
////		} else
////		
////		if((HasPatient == true) && (AtTarget == true)){
////			
////			state = aiState.Operating;
////			
////			
////		}else if(TakingCover == true){ 
////		
////			state = aiState.Hiding;
////			
////			} else if(AtTarget == true){
////				
////					state = aiState.IdleStill;
////				
////				} else if(HasPatient = true){
////					
////					state = aiState.RunningTo;
////				}
////				
////			Debug.Log(state);
////		
////			}
////	
////	void StateMachine(){
////		while(true){
////			switch(state){
////				
//////				case aiState.Dead;
//////				Dead();
//////				break;
//////				
//////				case aiState.Operating;
//////				Operating();
//////				break;
//////				
//////				case aiState.Hiding;
//////				Hiding();
//////				break;
//////				
//////				case aiState.IdleStill;
//////				Idle();
//////				break;
//////				
//////				case aiState.RunningTo;
//////		
//////		
//////				}
//////			yield
//////			}
//////	
//////
//////		
//////	void Dead(){
////			
////			
////		}
////		
////		
////	void Operating(){
////			
////			
////		}
////		
////		
////	void Hiding(){
////			
////			
////		}
////		
////		
////	void IdleStill(){
////			
////			
////		}
////		
////	void RunningTo(){
////		
////			
////			public CharacterController controller = GetComponent(CharacterController);
////			
////			controller.SimpleMove(transform.TransformDirection(Vector3.forward * RunSpeed));
////			
//
//}