using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class IInteractable : MonoBehaviour
{
    [SerializeField]
    protected bool isFocus;

    public virtual string GetName()
    {
        return "";
    }
    public virtual void Focus(GameObject interacter)
    {
        LogHelper.GetInstance().Log(interacter.name + " try to focus " + this.name);
        isFocus = true;
    }
    public virtual void Defocus(GameObject interacter)
    {
        LogHelper.GetInstance().Log(interacter.name + " defocus " + this.name);
        isFocus = false;
    }

    public virtual bool Interact(GameObject interacter)
    {
        LogHelper.GetInstance().Log(interacter.name + " try to interact with " + this.name);
        return isFocus;
    }

    public bool IsFocus()
    {
        return isFocus;
    }

    public virtual string GetKindOfInteraction()
    {
        return "";
    }
}
