using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SmokeActivation : MonoBehaviour
{

    PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.Health < playerHealth.smokeStart)
        {
            this.GetComponent<ParticleSystem>().Play();
        }
        if(playerHealth.Health >= playerHealth.smokeStart)
        {
            this.GetComponent<ParticleSystem>().Stop();
        }
    }
}
