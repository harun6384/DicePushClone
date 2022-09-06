using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : CharactersBase
{
    private Rigidbody _rigidbody;
    private Transform _target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject allyParent;
    [SerializeField] private Transform pushable;
    [SerializeField] private List<Transform> allyCharacters;
    [SerializeField] private float maxSpeed;
    private float _distanceBetweenAllyCharacter;
    private float _distanceToPushable;
    private bool _whatToAct;
    private float _startTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        allyParent = FindObjectOfType<AllyParent>().gameObject;
        pushable = FindObjectOfType<Pushable>().transform;
        AddAllAllyCharsOnList();
        GetDistanceToPushable();
        GetTarget();
        _startTime = Time.time;
    }
    private void OnDisable()
    {
        RemoveAllAllyCharsOnList();
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
        if (collision.gameObject.TryGetComponent(out AllyCharacter allyCharacter))
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
        RemoveAllAllyCharsOnList();
        AddAllAllyCharsOnList();
        var count = allyCharacters.Count;

        for (var i = 0; i < count; i++)
        {
            GetDistanceToPushable();
            if (allyCharacters[i] == null) return;
            //_distanceBetweenAllyCharacter = Vector3.Distance(transform.position, allyCharacters[i].position);
            GetDistanceBetweenAllyCharacter(i);
            if (_distanceBetweenAllyCharacter < _distanceToPushable)
            {
                _target = allyCharacters[i];
                _whatToAct = true;
            }
            else
            {
                _whatToAct = false;
            }
        }
    }
    private void GetDistanceBetweenAllyCharacter(int i)
    {
        _distanceBetweenAllyCharacter = transform.position.z - allyCharacters[i].position.z;
    }

    public override void Push()
    {
        MoveWithAddForce();
    }

    private void MoveWithAddForce()
    {
        _rigidbody.AddForce(Vector3.back * moveSpeed, ForceMode.VelocityChange);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
    }

    private void AddAllAllyCharsOnList()
    {
        foreach (Transform child in allyParent.GetComponentInChildren<Transform>())
        {
            allyCharacters.Add(child);
        }
    }
    private void RemoveAllAllyCharsOnList()
    {
        allyCharacters.Clear();
    }
    private void GetDistanceToPushable()
    {
        //_distanceToPushable = Vector3.Distance(transform.position, pushable.position);
        _distanceToPushable = transform.position.z - pushable.position.z;
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
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed * 1.5f);
        }
        else
        {
            _whatToAct = false;
        }
    }
}
