using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedXScript : MonoBehaviour
{
    public static RedXScript Instance;

    //Dit script koppelt het rode kruis aan de schatkist

    public Transform TreasureChestTransform;
    public float yPosition;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (TreasureChestTransform != null)
        {
            GetComponent<Renderer>().enabled = true;
            transform.position = new Vector3(TreasureChestTransform.position.x, yPosition, TreasureChestTransform.position.z);
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
