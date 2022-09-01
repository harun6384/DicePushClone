using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDice : MonoBehaviour
{
    [SerializeField] private DiceValue[] _diceValues;
    [SerializeField] private int _diceValue;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyParent;
    private bool _landed = false;
    private Rigidbody _rigidbody;
    private float _timer = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _diceValues = GetComponentsInChildren<DiceValue>();
        enemyParent = FindObjectOfType<EnemyParent>().gameObject;
        _rigidbody.AddTorque(new Vector3(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500)), ForceMode.Force);
    }
    private void Update()
    {
        CheckForLanded();
        _timer += Time.deltaTime;
        if (_landed)
        {
            InstantiateEnemyPrefab();
            Destroy(gameObject);
        }
    }

    private void CheckForLanded()
    {
        if (_timer >= 1 && _rigidbody.velocity.magnitude <= .5)
        {
            CheckDiceSide();
            _landed = true;
        }
    }

    private void InstantiateEnemyPrefab()
    {
        for (int i = 1; i <= _diceValue; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyParent.transform);
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
        if (collision.gameObject.TryGetComponent(out AllyCharacter allyCharacter))
        {
            Destroy(collision.gameObject);
        }
    }
}
