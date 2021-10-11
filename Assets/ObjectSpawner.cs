using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectSpawns;

    public List<GameObject> occupiedSpawns;

    public GameObject objectPrefab;

    public GameObject objectPool;

    public int howManyObjects;
    void Start()
    {
        PopulateVendorList();
        PopulateOccupiedSpawnsList();

        for(int i = 0; i < occupiedSpawns.Count; i++)
        {
            Debug.Log("Name: " + objectPool.transform.GetChild(i).name);
            SetVendorPositionToOccupiedSpawn(objectPool.transform.GetChild(i), occupiedSpawns[i].transform.position);
        }
    }

    private void PopulateVendorList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            objectSpawns.Add(transform.GetChild(i).gameObject);
        }
    }

    private void PopulateOccupiedSpawnsList()
    {
        for (int i = 0; i < howManyObjects; i++)
        {
            int vendorSpawnIndex = Random.Range(0, objectSpawns.Count);
            GameObject vendorSpawn = objectSpawns[vendorSpawnIndex];
            objectSpawns.Remove(vendorSpawn);
            occupiedSpawns.Add(vendorSpawn);
        }
    }

    private void SetVendorPositionToOccupiedSpawn(Transform vendor, Vector3 occupiedSpawnPosition)
    {
        vendor.position = occupiedSpawnPosition;
    }
}
