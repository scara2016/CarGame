using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionActivation : MonoBehaviour
{

    PlayerHealth playerHealth;
    bool explosionHasOccured = false;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.Health <= 0 && explosionHasOccured == false)
        {
            explosionHasOccured = true;
            this.GetComponent<ParticleSystem>().Play();
        }
        if (playerHealth.Health >= 0)
        {
            this.GetComponent<ParticleSystem>().Stop();
        }
    }
}
