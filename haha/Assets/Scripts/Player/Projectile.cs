using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float Speed = 6;
	public int Damage = 30;
	public float Range = 10;

	public Transform Player;

	private bool Detonating;

	List<Transform> ignore = new List<Transform>();
	
	void FixedUpdate ()
	{
		transform.position+=transform.forward * Speed * Time.deltaTime; //Just directly manupulating positions before I kill myself
		if (Vector3.Distance(transform.position, Player.position) > Range)
		{
			Destroy(gameObject);
		} 
	}

	void OnTriggerEnter(Collider other)
	{
		if (!ignore.Contains(other.transform) && other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
		{
			other.GetComponent<EnemyHealth>().TakeDamage(Damage,other.ClosestPoint(transform.position));
			ignore.Add(other.transform);
		}
	}
}
