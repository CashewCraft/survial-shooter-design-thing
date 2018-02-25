using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float Range = 18;

	int Bounds = 20;

	public float Speed = 3;
	public bool Slowed;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent>();
		nav.speed = Speed;
    }


    void Update ()
    {
		if (Vector3.Distance(player.position, transform.position) < Range)
		{
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
			{
				nav.SetDestination(player.position);
			}
			else
			{
				nav.enabled = false;
			}
		}
		else if (nav.remainingDistance == 0 || nav.remainingDistance < Mathf.Infinity) //Must check distance to path, so enemies don't lose interest after they see you
		{
			Patrol();
		}
    }

	void Patrol()
	{
		nav.SetDestination(new Vector3(Random.Range(-Bounds, Bounds), player.position.y, Random.Range(-Bounds, Bounds)));
	}

	public void Trap()
	{
		nav.speed /= 2;
		Slowed = true;
	}
}
