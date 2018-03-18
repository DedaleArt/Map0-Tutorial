using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class warriormouv : MonoBehaviour {

    
    Animator anim;
    
    NavMeshAgent nav;
    
    
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    
    
   
    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
	    anim = GetComponent<Animator>();
		agent.autoBraking = false;
    
    		GotoNextPoint();

    }
	
	// Update is called once per frame
	void Update ()
    {
        /* float h = Input.GetAxisRaw("Horizontal");
         float v = Input.GetAxisRaw("Vertical");
         Animating(h, v);
         */
        
       if (!agent.pathPending && agent.remainingDistance < 0.5f)
           			GotoNextPoint();
       Animating();
    }
	
	void GotoNextPoint()
	{
    		// Returns if no points have been set up
    		if (points.Length == 0)
    			return;
    
    		// Set the agent to go to the currently selected destination.
    		agent.destination = points[destPoint].position;
    
    		// Choose the next point in the array as the destination,
    		// cycling to the start if necessary.
    		destPoint = (destPoint + 1) % points.Length;
    }
   

    void Animating()
    {
        
        
        
        // Create a boolean that is true if either of the input axes is non-zero.
	    bool moving = points.Length != 0;


        // Tell the animator whether or not the player is walking.
        anim.SetBool("forward", moving); 
	    
       
    }
	
	public void Attack () 
	{
		anim.SetTrigger ("attack");
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Joueur"))
		{
			agent.Stop();
			Attack();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		agent.Resume();
	}
}
