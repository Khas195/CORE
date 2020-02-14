using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Item : IInteractable
{
    [SerializeField]
    GameObject itemEntity = null;
    ItemObject itemData = null;
    [SerializeField]
    bool useInventoryUI = false;
    [SerializeField]
    [ShowIf("useInventoryUI")]
    Canvas itemCanvas = null;
    void Awake()
    {
    }
    public override void Defocus(GameObject interacter)
    {
        base.Defocus(interacter);
        if (useInventoryUI)
        {
            itemCanvas.gameObject.SetActive(false);
        }
    }

    public override void Focus(GameObject interacter)
    {
        base.Focus(interacter);
        if (useInventoryUI)
        {
            itemCanvas.gameObject.SetActive(true);
        }
    }

    public override string GetKindOfInteraction()
    {
        return "Pick Up";
    }

    public override string GetName()
    {
        return itemData.name;
    }

    public override bool Interact(GameObject interacter)
    {
        if (base.Interact(interacter) == false) return false;
        InventorySystem.GetInstance().AddItem(this.itemData);
        Destroy(this.itemEntity);
        return true;
    }


}

