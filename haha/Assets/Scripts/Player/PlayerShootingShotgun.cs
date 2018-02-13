﻿using UnityEngine;

public class PlayerShootingShotgun : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public float inaccurracy = 0.6f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer[] gunLines;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLines = GetComponents <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
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
            i.enabled = false;
            i.SetPosition(0, transform.position);
        }

        foreach (LineRenderer i in gunLines)
        {

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward+new Vector3(Random.Range(-inaccurracy,inaccurracy), Random.Range(-inaccurracy, inaccurracy), 0);

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
                i.SetPosition(1, shootHit.point);
            }
            else
            {
                i.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}
