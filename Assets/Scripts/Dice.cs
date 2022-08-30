using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Dice : MonoBehaviour
{
    [SerializeField] private DiceValue[] _diceValues;
    [SerializeField] private int _diceValue;
    [SerializeField] private GameObject allyPrefab;
    [SerializeField] private GameObject allyParent;
    private bool _landed = false;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _diceValues = GetComponentsInChildren<DiceValue>();
        allyParent = FindObjectOfType<AllyParent>().gameObject;
        _rigidbody.AddTorque(new Vector3(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500)), ForceMode.Force);
    }
    private void Update()
    {
        CheckForLanded();
        if (_landed)
        {
            InstantiateAllyPrefab();
            Destroy(gameObject);
        }
    }

    private void CheckForLanded()
    {
        if (_rigidbody.velocity == Vector3.zero)
        {
            CheckDiceSide();
            _landed = true;
        }
    }

    private void InstantiateAllyPrefab()
    {
        for (int i = 1; i <= _diceValue; i++)
        {
            Instantiate(allyPrefab, transform.position, Quaternion.identity, allyParent.transform);
        }
    }
    private void CheckDiceSide()
    {
        foreach (var side in _diceValues)
        {
            if (side.ReturnGrounded())
            {
                _diceValue = side.DicesesValue;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyCharacter enemyCharacter))
        {
            Destroy(collision.gameObject);
        }
    }
}
