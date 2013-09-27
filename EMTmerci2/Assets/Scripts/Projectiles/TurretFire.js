#pragma strict

//var target : Rigidbody;
var projectile : Rigidbody;
var fireSpeed : float = 10;
var fireRate : float = 2;
var projectileLife : float = 1;
var Ammo : float = 1000;
var Firing : boolean = false;
var rootArmature : GameObject;
var delay : float = 1;
var Shotgun : boolean;
var Ak : boolean;

private var nextFire : float;
private var numBulletsFired = 0;

function Start () {

	rootArmature = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
	rootArmature.SendMessage("GunAdder", this.transform);
	if(Shotgun == true){
	rootArmature.SendMessage("Shotgun");
	}
	if(Ak == true){
	rootArmature.SendMessage("AK");
	}

}


function ShootThisGuy(){

	Firing = true;

}

function CeaseFire(){

	Firing = false;

}

function NoFireTime(){

	Firing = false;

}

function YesFireTime(){

	Firing = true;

}


function FixedUpdate () {

//if(target == null){

//	target = GameObject.FindGameObjectWithTag("Enemy").transform;
	
//	}

}

function AllSet(){

	Firing = true;
	numBulletsFired = 0;

}

function Update () {

if(Firing == true){

if((numBulletsFired < Ammo) && (Time.time > nextFire)){

		nextFire = Time.time + fireRate;
		
		
		var bulletFired : Rigidbody = Instantiate(projectile, transform.position, transform.rotation);


		bulletFired.velocity = transform.TransformDirection(Vector3.forward * fireSpeed);
		Destroy(bulletFired.gameObject, 1);
		
		numBulletsFired++;
		}
//		else
//		
//		Debug.Log(numBulletsFired);
//		rootArmature.SendMessage("ReloadShotgun");
//		Firing = false;		
//		numBulletsFired = 0;
//	}

if(numBulletsFired >= Ammo){

	rootArmature.SendMessage("Reload");
	Firing = false;
//	if(Time.time > delay){
//	numBulletsFired = 0;
	
}
}

}