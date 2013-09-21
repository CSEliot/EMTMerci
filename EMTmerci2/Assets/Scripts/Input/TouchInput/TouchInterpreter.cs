using UnityEngine;
using System.Collections;

public class TouchInterpreter : MonoBehaviour {

	
	public GameObject[] notifiedObjects;
	public int numNotifiedObjects;
	
	public bool isDragging = false;
	public bool isTouched = false;
	
	private Camera mainCamera;
	private Vector3 touchPos;
	
	public bool gameStarted = false;
	
	// Update is called once per frame
	void Update () {
	
		if(Debug.isDebugBuild){
			
			MouseControls();
			
	}else{
			
			TouchControls();
			
	}
			
	
	void MouseControls(){
			
			if(Input.GetMouseButtonDown=0)){
				
				RaycastHit hit;
				Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
				
				
				if(!Physics.Raycast (ray, out hit, 10000)) return;
				touchPos = mainCamera.ScreenToWorldPoint
			
		}	
		
		
			
}
