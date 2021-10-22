using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPool : MonoBehaviour
{
    public void ResetAllObjects()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            ContainerBehaviour container = transform.GetChild(i).GetComponent<ContainerBehaviour>();
            container.ResetItems();
            container.ResetItemContainers();
        }
    }
}
