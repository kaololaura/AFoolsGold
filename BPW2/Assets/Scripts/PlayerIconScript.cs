using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconScript : MonoBehaviour
{
    public Transform PlayerTransform;
    public float yPosition;
   
    void Update()
    {
        transform.position =  new Vector3(PlayerTransform.position.x, yPosition, PlayerTransform.position.z);
    }
}