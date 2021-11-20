using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics; 

public class PlayerHealth : MonoBehaviour
{   
    public float totalHealth = 100;
    private float health;
    public float Health
    {
        get
        {
            return health;
        }
    }
    public float restoreHealth = 20;
    public float thresholdForce = 3;
    public float damage = 10;
    public float force;
    public float smokeStart = 20;
    public float timer;
    private GameObject collidedObject;
    public ParticleSystem smokeParticleSystem;
    public ParticleSystem explosionParticleSystem;
    private PlayerMovement playerMovement;
      

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
       AnalyticsEvent.GameStart();
       health = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (health > 100)
        {
            health = 100;
        }

        if (health <= 0)
        {
           
        }

    }

    public void AddHealth()
    {
        health+= restoreHealth;
    }

   public void OnCollisionEnter(Collision collision)
   {
    if (collision.gameObject.tag == "Terrain")
    {
        force = Mathf.Abs((collision.impulse.x + collision.impulse.y + collision.impulse.z)/Time.fixedDeltaTime)/100;
        if (force >= thresholdForce)
        {
            collidedObject = collision.gameObject;
            health-= damage+force;
        }
    }
   }

   private void GameEnd()
   {
        // Analytics.CustomEvent("player death", new Dictionary<string, object>
        //      {
        //         { "time", timer },
        //         { "player position", gameObject.transform.position },
        //         { "object", collidedObject }
        //      });
        Vector3 playerPositionOnDeath = this.transform.position;
        Analytics.CustomEvent("death", new Dictionary<string, object>
        {
            {"Time", Time.deltaTime },
            {"Player Position", transform.position },
            {"object", collidedObject.name }
        }
        );

        AnalyticsEvent.GameOver();
        playerMovement.GameEnded = true;
   }

}
