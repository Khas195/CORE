using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FrontBackSorting : IFrontBackSorting
{
    // Start is called before the first frame update
    void Start()
    {
        if (useSelfAsHost)
        {
            host = this.transform;
        }
    }
    public override bool IsAboveCharacter(Vector3 characterPos, Vector3 hostPos)
    {
        return characterPos.y > hostPos.y;
    }

    public override bool IsBelowCharacter(Vector3 characterPos, Vector3 hostPos)
    {
        return characterPos.y < hostPos.y;
    }
}
