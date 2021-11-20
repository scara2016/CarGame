using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    Light checkpointLight;
    Material checkpointMaterial;
    public bool isActive = false;
    float gameTimer = 0;
    float hitCheckpointTimer = 0f;
    public PlayerHealth playerHealth;
    bool checkPointReached = false;
    public bool CheckPointReached
    {
        get
        {
            return checkPointReached;
        }
    }

    public float HitCheckpointTimer
    {
        get
        {
            return hitCheckpointTimer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        checkpointLight = GetComponentInChildren<Light>();
        checkpointLight.gameObject.SetActive(false);
        checkpointManager = FindObjectOfType<CheckpointManager>();
     
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        if (isActive == false)
        {
  
            //checkpointLight.intensity = 0;
        }
        else if(isActive == true)
        {
            checkpointLight.gameObject.SetActive(true);
            //   checkpointLight.intensity = 1;
          
        }
     

    }


    void OnTriggerEnter(Collider collider)
    {
        if (isActive == true)
        {
            Analytics.CustomEvent("checkpoint", new Dictionary<string, object>
            {
               { "time", gameTimer },
               { "player health", playerHealth.Health }
            });

            playerHealth.AddHealth();
            checkpointLight.intensity = 0;
            hitCheckpointTimer = gameTimer;
            checkpointManager.NextCheckpoint();
            checkPointReached = true;
            Debug.Log("Boo");
        }
        isActive = false;
    }
}
