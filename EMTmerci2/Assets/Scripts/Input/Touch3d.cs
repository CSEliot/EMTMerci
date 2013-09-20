using UnityEngine;
using System.Collections;

public class Touch3d : TouchLogic {

void OnTouchEnded3D () {
	
		SendMessage("TouchEnded");
	}

}