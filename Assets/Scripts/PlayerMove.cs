using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour{
	
	public GameObject bulletPrefab;
	void Update(){
		if (!isLocalPlayer) return;
		if (Input.GetKey ("up")) {
			transform.Translate(0, 0, 5);
			Debug.Log ("Move");
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
	[Command]
	void CmdFire()
	{
		// create the bullet object from the bullet prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			transform.position - transform.forward,
			Quaternion.identity);

		// make the bullet move away in front of the player
		bullet.GetComponent<Rigidbody>().velocity = -transform.forward*4;
		NetworkServer.Spawn(bullet);

		// make bullet disappear after 2 seconds
		Destroy(bullet, 2.0f);        
	}
}
