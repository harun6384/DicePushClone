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
    private int _diceMultiplier = 1;
    private bool _isScaleIncreased = false;
    private bool _landed = false;
    private Rigidbody _rigidbody;
    private float _timer = 0;

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
        _timer += Time.deltaTime;
        if (_landed)
        {
            InstantiateAllyPrefab();
            Destroy(gameObject);
        }
    }

    private void CheckForLanded()
    {
        if (_timer >= 1 &&_rigidbody.velocity.magnitude <= .5)
        {
            CheckDiceSide();
            _landed = true;
        }
    }

    private void InstantiateAllyPrefab()
    {
        _diceValue *= _diceMultiplier;
        for (int i = 1; i <= _diceValue; i++)
        {
            var go = Instantiate(allyPrefab, transform.position, Quaternion.identity, allyParent.transform);
            if(_isScaleIncreased) go.transform.localScale *= 2;
        }
    }
    private void CheckDiceSide()
    {
        foreach (var side in _diceValues)
        {
            if (side.OnGround)
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out DoubleX doubleX))
        {
            _diceMultiplier = 2;
            _isScaleIncreased = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out DoubleX doubleX))
        {
            _diceMultiplier = 1;
            _isScaleIncreased = false;
        }
    }
}
