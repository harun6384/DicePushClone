using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dice : DiceBase
{
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        parent = FindObjectOfType<AllyParent>().gameObject;
        AddSomeTorque();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyCharacter enemyCharacter))
        {
            Destroy(collision.gameObject);
        }
    }
}
