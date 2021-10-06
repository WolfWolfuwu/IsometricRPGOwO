﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{
    public abstract void CallMethod(GameObject Source);
}
