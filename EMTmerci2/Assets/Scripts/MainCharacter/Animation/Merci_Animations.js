#pragma strict

var animator : Animator;


function Run () {

	animator.SetBool("isRunning", true);
	animator.SetBool("isOperating", false);

}

function Idle(){

	animator.SetBool("isRunning", false);
}

function Operate () {

	animator.SetBool("isOperating", true);

}