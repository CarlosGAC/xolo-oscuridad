using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectSpawns;

    public List<GameObject> occupiedSpawns;

    public GameObject objectPool;

    public int howManyObjects;
    void Start()
    {
        PopulateObjectList();
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        PopulateOccupiedSpawnsList();
        for (int i = 0; i < occupiedSpawns.Count; i++)
        {
            SetVendorPositionToOccupiedSpawn(objectPool.transform.GetChild(i), occupiedSpawns[i].transform.position);
            occupiedSpawns[i].GetComponent<SpawnPoint>().whichObjectSpawnedHere = objectPool.transform.GetChild(i).gameObject;
        }
    }

    public void ChangeObjectPositionToNewSpawnPoint(GameObject whichGameObject)
    {
        int index = 0;

        for(int i = 0; i < occupiedSpawns.Count; i++)
        {
            if(occupiedSpawns[i].GetComponent<SpawnPoint>().whichObjectSpawnedHere == whichGameObject)
            {
                index = i;
                break;
            }
        }

        GameObject oldSpawnPointObject = occupiedSpawns[index];
        occupiedSpawns.RemoveAt(index);
        objectSpawns.Add(oldSpawnPointObject);

        GameObject newSpawnPointObject = SetRandomObjectSpawnToOccupied();
        SetVendorPositionToOccupiedSpawn(whichGameObject.transform, newSpawnPointObject.transform.position);
        newSpawnPointObject.GetComponent<SpawnPoint>().whichObjectSpawnedHere = whichGameObject;
    }

    private void PopulateObjectList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            objectSpawns.Add(transform.GetChild(i).gameObject);
        }
    }

    private GameObject SetRandomObjectSpawnToOccupied()
    {
        int vendorSpawnIndex = Random.Range(0, objectSpawns.Count);
        GameObject vendorSpawn = objectSpawns[vendorSpawnIndex];
        objectSpawns.Remove(vendorSpawn);
        occupiedSpawns.Add(vendorSpawn);
        return vendorSpawn;
    }

    private void PopulateOccupiedSpawnsList()
    {
        for (int i = 0; i < howManyObjects; i++)
        {
            SetRandomObjectSpawnToOccupied();
        }
    }

    private void SetVendorPositionToOccupiedSpawn(Transform vendor, Vector3 occupiedSpawnPosition)
    {
        
        if(vendor.name == "Player")
        {
            Debug.Log("Position: " + vendor.position);
            Debug.Log("Occupied spawn: " + occupiedSpawnPosition);

            vendor.position = occupiedSpawnPosition;
            Debug.Log("NEW Position: " + vendor.position);
        }
        vendor.position = occupiedSpawnPosition;
    }

    public void ResetSpawnPoints()
    {
        foreach(GameObject spawn in occupiedSpawns)
        {
            objectSpawns.Add(spawn);
        }

        occupiedSpawns.Clear();
    }
}
