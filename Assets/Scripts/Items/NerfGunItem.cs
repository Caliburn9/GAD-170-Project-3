using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that when used while held acts as a physics based projectile instantiator
/// </summary>
public class NerfGunItem : InteractiveItem
{
    public GameObject nerfDartPrefab;
    public Transform nerfDartSpawnLocation;
    public float fireRate = 1;    
    public float launchForce = 10;
    protected float fireRateCounter;

    float timer;

protected void Update()
    {
        //Increase the timer's value in relation to the in-game time
        timer += Time.deltaTime;
    }

public override void OnUse()
{
        base.OnUse();
        FireNow();
}

public void FireNow()
{
        //Fire a nerf dart, adding a force to it and playing a sound to indicate this
        if (timer >= fireRate)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            GameObject dart = Instantiate(nerfDartPrefab, nerfDartSpawnLocation.position, Quaternion.identity);
            Rigidbody rb = dart.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * launchForce);

            timer = 0;
        }
    }
}
