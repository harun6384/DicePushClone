using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiplierBase : MonoBehaviour
{
    [SerializeField] private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            DestroyAfterAWhile();
        }
    }
    public abstract void DestroyAfterAWhile();
}
