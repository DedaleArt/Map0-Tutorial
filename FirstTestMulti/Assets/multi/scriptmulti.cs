using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class scriptmulti : NetworkBehaviour
{
	/*public Canvas gameover;
	public List<Text> Heart;

	private int count;
	public Text countText; */
	
	public Transform nord;
	public Transform sud;
	public Transform est;
	public Transform ouest;
	public float distacediago;
	private string lastposition = null;
	
	public float jumpPower = 1;
	private int jumppossible = 0;
	
	public Transform rightGunBone;
	public Transform leftGunBone;
	public Arsenal[] arsenal;
	public float speed;

	private float lasttimeit;
	
	int pv;
	private Animator animator;

	const int countOfDamageAnimations = 3;
	int lastDamageAnimation = -1;
	

	public int m_PlayerNumber = 1
		; // Used to identify which tank belongs to which player.  This is set by this tank's manager.

	public float m_Speed = 12f; // How fast the tank moves forward and back.


	private int isdead;
		 // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.

	
	


	private string m_MovementAxisName; // The name of the input axis for moving forward and back.
	private string m_TurnAxisName; // The name of the input axis for turning.
	private Rigidbody m_Rigidbody; // Reference used to move the tank.
	private float mouv_droite; // The current value of the movement input.
	private float mouv_profon; // The current value of the turn input.
	private float m_OriginalPitch; // The pitch of the audio source at the start of the scene.


	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		//gameover.enabled = false;
	}


	private void OnEnable()
	{
		// When the tank is turned on, make sure it's not kinematic.
		m_Rigidbody.isKinematic = false;

		// Also reset the input values.
		mouv_droite = 0f;
		mouv_profon = 0f;
	}


	private void OnDisable()
	{
		// When the tank is turned off, set it to kinematic so it stops moving.
		m_Rigidbody.isKinematic = true;
	}


	private void Start()
	{
		
		// The axes names are based on player number.
		m_MovementAxisName = "Vertical";
		m_TurnAxisName = "Horizontal" ;
		pv = 4;
		isdead = 1;
		//count = 0;
		lasttimeit = 0;

		//SetCountText ();

		
	}


	private void Update()
	{
		if (!isLocalPlayer)
			return;
		// Store the value of both input axes.
		mouv_droite = Input.GetAxis(m_MovementAxisName);
		mouv_profon = Input.GetAxis(m_TurnAxisName);
		if (Input.GetButtonDown("Jump") ) 
			Jump();
		Move();
		if (! Isalive())
		{
			pv = 1;
			Death();
			//gameover.enabled = true;
			isdead = 0;
		}

		lasttimeit += Time.deltaTime;

	}


	


	private void FixedUpdate()
	{
		// Adjust the rigidbodies position and orientation in FixedUpdate.
		
		
		
		
		lastposition = where();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!isLocalPlayer)
			return;
		if (other.gameObject.CompareTag("Ground"))
			jumppossible = 0;
	}


	private void Move()
	{
		Vector3 movement = new Vector3(0,0,0);
		switch (where())
		{
				case "nord" : 
					movement = new Vector3(-mouv_profon,0,-mouv_droite);
					break;
				case "est" : 
					movement = new Vector3(-mouv_droite,0,  mouv_profon);
					break;
				case "sud" : 
					movement = new Vector3(mouv_profon,0, mouv_droite);
					break;
				case "ouest" : 
					movement = new Vector3(mouv_droite,0,- mouv_profon);
					break;
		}	
		
		
		
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement * m_Speed * Time.deltaTime*isdead);
		transform.LookAt(m_Rigidbody.position + movement * m_Speed * Time.deltaTime * isdead);
		if (movement*isdead != new Vector3(0,0,0))
			Walk();
		else if (isdead == 1)
			Stay();
	}


	private void Turn()
	{
		/*// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = mouv_profon * m_TurnSpeed * Time.deltaTime;

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);*/
		Vector3 movement = new Vector3(0,0,0)* mouv_droite * m_Speed * Time.deltaTime;
		
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
		
	}

	
	[System.Serializable]
	public struct Arsenal
	{
		public string name;
		public GameObject rightGun;
		public GameObject leftGun;
		public RuntimeAnimatorController controller;
	}
	public void SetArsenal(string name) {
       		foreach (Arsenal hand in arsenal) {
       			if (hand.name == name) {
       				if (rightGunBone.childCount > 0)
       					Destroy(rightGunBone.GetChild(0).gameObject);
       				if (leftGunBone.childCount > 0)
       					Destroy(leftGunBone.GetChild(0).gameObject);
       				if (hand.rightGun != null) {
       					GameObject newRightGun = (GameObject) Instantiate(hand.rightGun);
       					newRightGun.transform.parent = rightGunBone;
       					newRightGun.transform.localPosition = Vector3.zero;
       					newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
       					}
       				if (hand.leftGun != null) {
       					GameObject newLeftGun = (GameObject) Instantiate(hand.leftGun);
       					newLeftGun.transform.parent = leftGunBone;
       					newLeftGun.transform.localPosition = Vector3.zero;
       					newLeftGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
       				}
       				animator.runtimeAnimatorController = hand.controller;
       				return;
       				}
       		}
       	}
	public void Jump()
    	{
		   	if (isdead ==1 )
				if (jumppossible< 2)
				{
					jumppossible++;
					Jumpanim();
					m_Rigidbody.AddForce(0, jumpPower,0);
				}
    	}
	
	
	public void Stay () {
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 0f);
	}

	

	public void Walk () {
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 0.5f);
	}

	public void Run () {
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 1f);
	}

	public void Attack () {
		Aiming ();
		animator.SetTrigger ("Attack");
	}

	public void Death () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
			animator.Play("Idle", 0);
		else
			animator.SetTrigger ("Death");
	}

	public void Damage () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) return;
		int id = Random.Range(0, countOfDamageAnimations);
		if (countOfDamageAnimations > 1)
			while (id == lastDamageAnimation)
				id = Random.Range(0, countOfDamageAnimations);
		lastDamageAnimation = id;
		animator.SetInteger ("DamageID", id);
		animator.SetTrigger ("Damage");
	}

	public void Jumpanim () {
		animator.SetBool ("Squat", false);
		animator.SetFloat ("Speed", 0f);
		animator.SetBool("Aiming", false);
		animator.SetTrigger ("Jump");
	}

	public void Aiming () {
		animator.SetBool ("Squat", false);
		animator.SetFloat ("Speed", 0f);
		animator.SetBool("Aiming", true);
	}

	public void Sitting () {
		animator.SetBool ("Squat", !animator.GetBool("Squat"));
		animator.SetBool("Aiming", false);
	}
	
	
	
	
	
	public string where()
	{
		float no = 0;
		float ou = 0;
		float su = 0;
		float es = 0;
		
		switch (lastposition)
		{
			case  "nord" :
				no = distacediago;
				break;
			case  "ouest" :
				ou = distacediago;
				break;
			case  "sud" :
				su = distacediago;
				break;
			case  "est" :
				es = distacediago;
				break;
					
		}
		
		string azymut;
		
		if ((this.transform.position - nord.position).magnitude -no  < (this.transform.position - sud.position).magnitude -su &&
		    (this.transform.position - nord.position).magnitude-no < (this.transform.position - est.position).magnitude - es
		    && (this.transform.position - nord.position).magnitude-no < (this.transform.position - ouest.position).magnitude - ou)
			azymut = "nord";
		else if ((this.transform.position - ouest.position).magnitude - ou < (this.transform.position - sud.position).magnitude -su &&
		         (this.transform.position - ouest.position).magnitude - ou < (this.transform.position - est.position).magnitude- es)
			azymut = "ouest";
		else if ((this.transform.position - est.position).magnitude- es < (this.transform.position - sud.position).magnitude -su)
			azymut = "est";
		else
			azymut = "sud";

		return azymut;
	}

	void OnTriggerEnter(Collider other)
	{
		 /*if (other.gameObject.CompareTag ("Pick up"))
		{
			other.gameObject.SetActive (false);
			count += 1;
			SetCountText ();
		}
		if (other.gameObject.CompareTag ("Endup") && count == 1)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
		} */
		if (other.gameObject.CompareTag ("underground"))
		{
			pv = 0;
		}
		if (other.gameObject.CompareTag ("mob"))
		{
			if (lasttimeit < 2.0f)
				return;
			
			
			Damage();
			pv--;
			//Life();
			lasttimeit = 0;
		}

	}
	/*
	public void Life()
	{
		Heart[pv].enabled = false;
	}*/

	public bool Isalive ()
	{
		return pv != 0;

	}

	/*
	void SetCountText ()
	{

		countText.text = "Loot: " + count;
	}*/
}