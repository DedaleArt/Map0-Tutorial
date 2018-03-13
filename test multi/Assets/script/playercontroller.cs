using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playercontroller : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;


        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cmdfire();
        }


    }
    [Command]
    void Cmdfire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }
}
