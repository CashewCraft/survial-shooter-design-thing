using UnityEngine;

public class PlayerShootingPlasma: MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float Range = 100f;

    float timer;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

	public GameObject projectile;

    void Awake ()
    {
        gunParticles = GetComponent<ParticleSystem> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && PlayerHealth.isAlive)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.FromToRotation(transform.position, transform.position + transform.forward), null);
        bullet.GetComponent<Projectile>().Damage = damagePerShot;
		bullet.GetComponent<Projectile>().Range = Range;
		bullet.GetComponent<Projectile>().Player = transform;
        bullet.transform.rotation = transform.rotation;
	}
}
