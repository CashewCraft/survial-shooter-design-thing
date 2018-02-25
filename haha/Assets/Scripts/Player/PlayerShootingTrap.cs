using UnityEngine;

public class PlayerShootingTrap : MonoBehaviour
{
	public int Damage = 10;
    public int Limit = 4;
	int Deployed = 0;
	public int durability = 3;
    public float timeBetweenBullets = 0.15f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

	public GameObject projectile;

	void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && PlayerHealth.isAlive && Deployed <= Limit )
        {
            Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
	}


    void Shoot ()
    {
        timer = 0f;

		GameObject bullet = Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0), null);
		bullet.GetComponent<Rigidbody>().AddForce(transform.forward);
		bullet.GetComponent<Trap>().Uses = durability;
		bullet.GetComponent<Trap>().Damage = Damage;
		bullet.GetComponent<Trap>().parent = this;
        Deployed++;
	}

	public void Expired()
	{
		Deployed -= 1;
	}
}
