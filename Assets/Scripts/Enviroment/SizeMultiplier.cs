using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeMultiplier : MultiplierBase
{
    public override void DestroyAfterAWhile()
    {
        Destroy(gameObject);
    }
}
