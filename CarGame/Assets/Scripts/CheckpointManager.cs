using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint[] checkpoints;

    public Checkpoint[] Checkpoints
    {
        get
        {
            return checkpoints;
        }
    }
    private int i = 0;
    private bool gameWon = false;

    public bool GameWon
    {
        get
        {
            return gameWon;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Checkpoint[] checkpointsTemporary = FindObjectsOfType<Checkpoint>();
        checkpoints = new Checkpoint[checkpointsTemporary.Length];
        

        for (int i = 0; i < checkpointsTemporary.Length; i++)
            for (int j = 0; j < checkpointsTemporary.Length; j++) {
                {
                    if (checkpointsTemporary[j].name.EndsWith(i.ToString()))
                    {
                        checkpoints[i] = checkpointsTemporary[j];
                    }
                }
            }
        checkpoints[i].isActive = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void NextCheckpoint()
    {
       i++;
       if (i >= checkpoints.Length)
       {
            gameWon = true;
       } else
       {
           checkpoints[i].isActive = true;
       }
    }
}
