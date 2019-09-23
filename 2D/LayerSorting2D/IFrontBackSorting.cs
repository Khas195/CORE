using System;
using UnityEngine;

public abstract class IFrontBackSorting : MonoBehaviour
{
    [SerializeField]
    protected Transform host;
    [SerializeField]
    protected bool useSelfAsHost;
    public virtual Vector3 HostPosition()
    {
        return host.position;
    }
    public virtual void SetHostPosition(Vector3 pos)
    {
        host.position = pos;
    }

    public abstract bool IsAboveCharacter(Vector3 characterPos, Vector3 hostPos);

    public abstract bool IsBelowCharacter(Vector3 characterPos, Vector3 hostPos);
}
