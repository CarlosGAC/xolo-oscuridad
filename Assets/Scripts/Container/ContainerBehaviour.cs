using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContainerBehaviour : MonoBehaviour
{

    /*
     * Posiciones de los items
     * 0: Flores
     * 1: Joyas
     * 2: Papel Picado
     * 3: Maiz
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

    public void ResetItemContainers()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void AddItemToContainer(int position)
    {
        transform.GetChild(position).gameObject.SetActive(true);
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
            ResetItemContainers();
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
