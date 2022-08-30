using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValue : MonoBehaviour
{
    [SerializeField] private int diceValue;
    public int DicesesValue => diceValue;
    private bool _onGround = false;
    public bool OnGround => _onGround;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ground ground))
        {
            _onGround = true;
        }
    }
    public bool ReturnGrounded()
    {
        return _onGround;
    }
}
