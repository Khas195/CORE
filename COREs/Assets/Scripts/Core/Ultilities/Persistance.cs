﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistance : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this);
    }
}
