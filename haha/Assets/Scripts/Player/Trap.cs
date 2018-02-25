using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	public int Uses = 4;
	public int Damage = 10;
	public PlayerShootingTrap parent;
	List<Transform> hit;

	void Start()
	{
		hit = new List<Transform>();

		foreach (Transform i in transform)
		{
			i.GetComponent<Renderer>().material.color = Random.ColorHSV();
			Vector3 dir = Random.onUnitSphere; dir.y = 0;
			i.GetComponent<Rigidbody>().AddForce(dir * 100f);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Shootable") && !hit.Contains(other.transform))
		{
			other.GetComponent<EnemyHealth>().TakeDamage(Damage, other.ClosestPoint(transform.position));
			if (!other.transform.GetComponent<EnemyMovement>().Slowed)
			{
				other.transform.GetComponent<EnemyMovement>().Trap();
            }
			Uses -= 1;
			if (Uses <= 0)
			{
				parent.Expired();
				Destroy(gameObject);
			}
		}
	}
}