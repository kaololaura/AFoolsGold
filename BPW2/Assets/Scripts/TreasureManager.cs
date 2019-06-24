using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    //Dit script zorgt voor het spawnen van de schatkisten en slaat de locaties op

    public static TreasureManager Instance;

    public GameObject TreasurePrefab;
    public List<Transform> TreasureLocations;

    private Transform lastLocation;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        GetTreasureLocations();
    }

    void GetTreasureLocations()
    {
        foreach (Transform child in transform)
        {
            TreasureLocations.Add(child);
        }
    }

    public void SpawnTreasure()
    {
        List<Transform> availableLocations = new List<Transform>();
        foreach (var location in TreasureLocations)
        {
            if(location != lastLocation)
            {
                availableLocations.Add(location);
            }
        }

        lastLocation = TreasureLocations[Random.Range(0, TreasureLocations.Count)];

        GameObject treasure = Instantiate(TreasurePrefab, lastLocation.position + new Vector3(0, 1f,0), lastLocation.rotation);
        treasure = treasure.transform.GetChild(0).gameObject;

        ShovelScript.Instance.TreasureChest = treasure;
        RedXScript.Instance.TreasureChestTransform = treasure.transform;
    }
}
