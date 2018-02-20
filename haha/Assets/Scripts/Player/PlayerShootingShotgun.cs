using UnityEngine;

public class PlayerShootingShotgun : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float OptimalRange = 50f;
	public float MaxRange = 100f;
	public float inaccurracy = 0.6f;
	public int pellets = 6;

	float dropOff;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer[] gunLines;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.05f;


    void Awake ()
    {
		dropOff = damagePerShot / (MaxRange - OptimalRange);

		GameObject[] Holders = new GameObject[pellets];
		gunLines = new LineRenderer[pellets];
		for (int i = 0; i < pellets; i++)
		{
			Holders[i] = new GameObject();
			Holders[i].transform.position = transform.position;
			Holders[i].transform.parent = transform;
			Holders[i].AddComponent<LineRenderer>();
			gunLines[i] = Holders[i].GetComponent<LineRenderer>();
			gunLines[i].widthMultiplier = 0.05f;
			gunLines[i].colorGradient = transform.GetComponent<LineRenderer>().colorGradient;
            gunLines[i].material = transform.GetComponent<LineRenderer>().material;
        }

        shootableMask = LayerMask.GetMask ("Shootable");
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

        if(timer >= Mathf.Min(1,timeBetweenBullets) * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        foreach (LineRenderer i in gunLines)
        {
            i.enabled = false;
        }
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        foreach (LineRenderer i in gunLines)
        {
            i.enabled = true;
            i.SetPosition(0, transform.position);
        }

        foreach (LineRenderer i in gunLines)
        {

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward+new Vector3(Random.Range(-inaccurracy,inaccurracy), Random.Range(0, inaccurracy*0.5f), 0);

			if (Physics.Raycast(shootRay, out shootHit, MaxRange, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage((int)(damagePerShot-(dropOff*Mathf.Max(Vector3.Distance(transform.position,shootHit.point)-OptimalRange,0))), shootHit.point);
                }
                i.SetPosition(1, shootHit.point);
            }
            else
            {
                i.SetPosition(1, shootRay.origin + shootRay.direction * MaxRange);
            }
        }
    }
}
