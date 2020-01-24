﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : ObjectPooling<GameObject>
{
    protected override void ActivateOjbect(GameObject target)
    {
        target.SetActive(true);
    }

    protected override GameObject CreateInstance()
    {
        return GameObject.Instantiate(this.prototype, this.transform);
    }

    protected override void DeactivateObject(GameObject target)
    {
        target.SetActive(false);
    }
}
