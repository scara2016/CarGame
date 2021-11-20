using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody playerRigidbody;
    private Vector3 oldOffset;
    private Vector3 newOffset;
    public float lerpPositionStrength = 0.2f;
    public float lerpForwardStrength = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        setPosition();
    }

    private void setPosition()
    {
        oldOffset = player.transform.position;
        newOffset += (oldOffset + (-playerRigidbody.velocity * 1f) - newOffset) * Time.fixedDeltaTime / 10f; 
        Vector3 offset = Vector3.Lerp(oldOffset, newOffset, lerpPositionStrength);
        Vector3 targetVector = Vector3.Lerp(transform.forward, player.transform.forward, lerpForwardStrength);
        targetVector.y = 0;
        transform.forward = targetVector;

        transform.position = offset;       
    }
  
}
