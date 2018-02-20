using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float Speed = 6;
	public float Damage = 30;
    
	List<Collider> ignore = new List<Collider>();
	
	void FixedUpdate () {
        transform.Translate((transform.position+transform.forward) * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.forward);
	}

	void OnTriggerEnter(Collider other)
	{
		if (!ignore.Contains(other) && other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
		{
			other.GetComponent<EnemyHealth>().TakeDamage(30,other.ClosestPoint(transform.position));
			ignore.Add(other);
		}
	}
}
