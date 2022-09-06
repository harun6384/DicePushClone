using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCharacter : CharactersBase
{
    private Rigidbody _rigidbody;
    private Transform _target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pushable;
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private List<Transform> enemyCharacters;
    [SerializeField] private float maxSpeed;
    private float _distanceBetweenEnemyCharacter;
    private float _distanceToPushable;
    private bool _whatToAct;
    private float _startTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        pushable = FindObjectOfType<Pushable>().transform;
        enemyParent = FindObjectOfType<EnemyParent>().gameObject;
        AddAllEnemyCharsOnList();
        GetDistanceToPushable();
        GetTarget();
        _startTime = Time.time;
    }
    private void OnDisable()
    {
        RemoveAllEnemyCharsOnList();
    }
    private void FixedUpdate()
    {
        if (_target == null)
        {
            GetTarget();
        }
        Act();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyCharacter enemyCharacter))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    public override void Act()
    {
        if (_whatToAct)
        {
            MoveToTheTarget();
        }
        else
        {
            Push();
        }
    }

    public override void GetTarget()
    {
        RemoveAllEnemyCharsOnList();
        AddAllEnemyCharsOnList();
        var count = enemyCharacters.Count;

        for (var i = 0; i < count; i++)
        {
            GetDistanceToPushable();
            if (enemyCharacters[i] == null) return;
            //_distanceBetweenEnemyCharacter = Vector3.Distance(enemyCharacters[i].position, transform.position);
            GetDistanceBetweenEnemyCharacter(i);
            if (_distanceBetweenEnemyCharacter < _distanceToPushable)
            {
                _target = enemyCharacters[i];
                _whatToAct = true;
            }
            else
            {
                _whatToAct = false;
            }
        }
    }
    private void GetDistanceBetweenEnemyCharacter(int i)
    {
        _distanceBetweenEnemyCharacter = enemyCharacters[i].position.z - transform.position.z;
    }

    public override void Push()
    {
        MoveWithAddForce();
    }

    private void MoveWithAddForce()
    {
        _rigidbody.AddForce(Vector3.forward * moveSpeed, ForceMode.VelocityChange);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
    }

    private void AddAllEnemyCharsOnList()
    {
        foreach (Transform child in enemyParent.GetComponentInChildren<Transform>())
        {
            enemyCharacters.Add(child);
        }
    }
    private void RemoveAllEnemyCharsOnList()
    {
        enemyCharacters.Clear();
    }
    private void GetDistanceToPushable()
    {
        //_distanceToPushable = Vector3.Distance(pushable.position, transform.position);
        _distanceToPushable = pushable.position.z - transform.position.z;
    }
    private void MoveToTheTarget()
    {
        if (_target != null)
        {
            var distanceBetweenTarget = Vector3.Distance(transform.position, _target.position);
            float distanceCovered = Time.time - _startTime;
            float fractionOfJourney = distanceCovered / distanceBetweenTarget;
            var curTargetPos = _target.position;
            transform.position = Vector3.Lerp(transform.position, curTargetPos, fractionOfJourney);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed / 10);
        }
        else
        {
            _whatToAct = false;
        }
    }
}
