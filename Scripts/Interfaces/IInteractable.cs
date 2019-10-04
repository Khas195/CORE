using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractable : MonoBehaviour {
    [SerializeField]
    protected bool isFocus;

    // Update is called once per frame
    void Update () {
		
	}
    public virtual string GetName()
    {
        return "";
    }
    public virtual void Focus(GameObject interacter) 
    {
         Definition.InteractableDebug(interacter.name + " try to focus " + this.name);
        
        isFocus = true;
    }
    public virtual void Defocus(GameObject interacter)
    {
        isFocus = false;
    }

    public virtual bool Interact(GameObject interacter)
    {
        Definition.InteractableDebug(interacter.name + " try to interact with " + this.name);
        return isFocus;
    }

    public virtual GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
