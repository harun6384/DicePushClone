using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CharactersBase : MonoBehaviour
{
    public abstract void Act();
    public abstract void GetTarget();
    public abstract void Push();
}
