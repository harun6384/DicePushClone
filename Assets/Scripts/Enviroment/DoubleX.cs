using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleX : MultiplierBase
{
    public override void DestroyAfterAWhile()
    {
        Destroy(gameObject);
    }
}
