#pragma strict

var animator : Animator;


function Run () {

	animator.SetBool("isRunning", true);

}

function Idle(){

	animator.SetBool("isRunning", false);
}

function Operate () {

}