using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContainerBehaviour : MonoBehaviour
{

    /*
     * Posiciones de los items
     * 0: comida 
     * 1: papel picado
     * 2: cempasuchitl
     * 3: mementos
     */

    public bool[] items;
    public int maxItems;

    public GeneralSounds generalSounds;
    public GameMaster gameMaster;

    public UnityEvent OnContainerCompleted;
    public ObjectSpawner containerSpawner;

    void Start()
    {
        items = new bool[maxItems];
        OnContainerCompleted.AddListener(generalSounds.PlayContainerFinished);
        OnContainerCompleted.AddListener(delegate { gameMaster.IncreaseScore(5); });
    }

    public void CheckForAllItems()
    {
        bool allItems = true;

        foreach(bool item in items)
        {
            allItems &= item;
        }

        if(allItems)
        {
            ResetItems();
            OnContainerCompleted.Invoke();
            containerSpawner.ChangeObjectPositionToNewSpawnPoint(gameObject);
        }
    }

    public void ResetItems()
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = false;
        }
    }
}
