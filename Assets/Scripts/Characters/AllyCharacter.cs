using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCharacter : CharactersBase
{
    private Rigidbody _rigidbody;
    private Transform _target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float viewDistance;
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private Transform pushable;
    [SerializeField] private List<Transform> enemyCharacters;
    private float _distanceBetweenEnemyCharacter;
    private float _distanceToPushable;
    private bool _whatToAct;
    private float _startTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        enemyParent = FindObjectOfType<EnemyParent>().gameObject;
        pushable = FindObjectOfType<Pushable>().transform;
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
            if (enemyCharacters[i] == null) return;
            _distanceBetweenEnemyCharacter = Mathf.Abs(Vector3.Distance(transform.position, enemyCharacters[i].position));
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

    public override void Push()
    {
        //_target = pushable;
        _rigidbody.velocity = Vector3.forward * moveSpeed;
        
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
        _distanceToPushable = Mathf.Abs(Vector3.Distance(pushable.position, transform.position));
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
        }
        else
        {
            _whatToAct = false;
        }
    }
}
