using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorBehaviour : MonoBehaviour
{
    public VendorItem item;
    public SpriteRenderer itemHolderSpriteRenderer;
    void Start()
    {
        item = GetComponent<VendorItem>();
        itemHolderSpriteRenderer.sprite = item.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
