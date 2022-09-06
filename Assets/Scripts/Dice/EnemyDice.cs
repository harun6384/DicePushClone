using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDice : DiceBase
{
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        parent = FindObjectOfType<EnemyParent>().gameObject;
        AddSomeTorque();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out AllyCharacter allyCharacter))
        {
            Destroy(collision.gameObject);
        }
    }
}
