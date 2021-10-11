using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        items = new bool[maxItems];
    }

    // Update is called once per frame
    void Update()
    {
        
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
            generalSounds.PlayContainerFinished();
            Destroy(gameObject);
        }
    }
}
