﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLegSize : MonoBehaviour
{

    public void ChangeSize(Vector3 scaling)
    {
        gameObject.transform.localScale = scaling;
    }
}
