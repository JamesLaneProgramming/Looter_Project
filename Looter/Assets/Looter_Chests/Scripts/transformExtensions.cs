using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LooterChests
{
    public static Transform FindRootParent(this Transform _transform)
    {
        Transform currentTransform = _transform;
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }
        return currentTransform;
    }
}
