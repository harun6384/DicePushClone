using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceBase : MonoBehaviour
{
    [SerializeField] protected DiceValue[] _diceValues;
    [SerializeField] protected int _diceValue;
    [SerializeField] protected GameObject characterPrefab;
    [SerializeField] protected GameObject parent;
    protected int _diceMultiplier = 1;
    protected bool _isScaleIncreased = false;
    protected bool _landed = false;
    protected Rigidbody _rigidbody;
    protected float _timer = 0;


    private void Awake()
    {
        _diceValues = GetComponentsInChildren<DiceValue>();
    }
    private void Update()
    {
        CheckForLanded();
        _timer += Time.deltaTime;
        if (_landed)
        {
            InstantiatePrefab(parent);
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
    private void InstantiatePrefab(GameObject parent)
    {
        _diceValue *= _diceMultiplier;
        for (int i = 1; i <= _diceValue; i++)
        {
            var go = Instantiate(characterPrefab, transform.position, Quaternion.identity, parent.transform);
            if (_isScaleIncreased) go.transform.localScale *= 2;
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
    protected void AddSomeTorque()
    {
        _rigidbody.AddTorque(new Vector3(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500)), ForceMode.Force);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out DoubleX doubleX))
        {
            _diceMultiplier = 2;
        }
        if (other.gameObject.TryGetComponent(out SizeMultiplier sizeMultiplier))
        {
            _isScaleIncreased = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out DoubleX doubleX))
        {
            _diceMultiplier = 1;
        }
        if (other.gameObject.TryGetComponent(out SizeMultiplier sizeMultiplier))
        {
            _isScaleIncreased = false;
        }
    }
}
