using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class camerascript : MonoBehaviour {

	// Use this for initialization

	public Transform player;
	public Transform nord;
	public Transform sud;
	public Transform est;
	public Transform ouest;
	private float distance_camera;
	private float hauteur_cam;
	private Transform initialtrans;
	private string lastposition = null;
	public float distacediago;
	private float time = 0;
	
	void Start ()
	{
		initialtrans = this.gameObject.transform;
		distance_camera = -Mathf.Abs(player.position.z - initialtrans.position.z);
		hauteur_cam = initialtrans.position.y - player.position.y;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//mouvcame();
		
		poscamera();
		lastposition = where();
		
	}


	private void FixedUpdate()
	{
		
	}
	
	
	public void mouvcame()
	{
		if (@where() != lastposition)
			time = 0;
		while (time< 1000)
		{
			gameObject.transform.RotateAround(player.position, new Vector3(0.0f,1.0f,0.0f),90  );
		}
			
			
	}

	
	public void poscamera()
	{
		
		string azymut = where();
		
		
			
			
		
		switch (azymut)
		{
			case "sud" : 
				this.gameObject.transform.position = new Vector3(player.position.x, player.position.y + hauteur_cam, player.position.z + distance_camera);
				 
				break;
			case "est" :
				this.gameObject.transform.position = new Vector3(player.position.x - distance_camera,player.position.y + hauteur_cam, player.position.z);
				
				break;
			case "nord" : 
				this.gameObject.transform.position = new Vector3(player.position.x, player.position.y + hauteur_cam, player.position.z - distance_camera); 
				
				break;
			case "ouest" :
				this.gameObject.transform.position = new Vector3(player.position.x + distance_camera,player.position.y + hauteur_cam, player.position.z); 
				
				break;
			
				
		}
		transform.LookAt(player);
		
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
		
		if ((player.position - nord.position).magnitude -no  < (player.position - sud.position).magnitude -su &&
		    (player.position - nord.position).magnitude-no < (player.position - est.position).magnitude - es
		    && (player.position - nord.position).magnitude-no < (player.position - ouest.position).magnitude - ou)
			azymut = "nord";
		else if ((player.position - ouest.position).magnitude - ou < (player.position - sud.position).magnitude -su &&
		         (player.position - ouest.position).magnitude - ou < (player.position - est.position).magnitude- es)
			azymut = "ouest";
		else if ((player.position - est.position).magnitude- es < (player.position - sud.position).magnitude -su)
			azymut = "est";
		else
			azymut = "sud";

		return azymut;
	}

	
}
/*
 * if (player.transform.position.x < sudest.position.x && player.transform.position.x > sudouest.position.x &&
   		    player.transform.position.z < sudest.position.z)
   			return "sud";
   		if (player.transform.position.x < nordest.position.x && player.transform.position.x > nordouest.position.x &&
   		    player.transform.position.z > nordest.position.z)
   			return "nord";
   		if (player.transform.position.x < nordouest.position.x && player.transform.position.z > sudouest.position.z &&
   		    player.transform.position.z < nordouest.position.z)
   			return "ouest";
   		if (player.transform.position.x > sudest.position.x && player.transform.position.z > sudest.position.z &&
   		    player.transform.position.z < nordest.position.z)
   			return "est";
   		return "intransition";
 */