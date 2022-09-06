using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyAllyCharacterEvent : UnityEvent<Transform>
{
}

[System.Serializable]
public class MyEnemyCharacterEvent : UnityEvent<Transform>
{
}

public static class EventManager
{
    public static MyAllyCharacterEvent AddAllyCharacterToTheList;
    public static MyEnemyCharacterEvent AddEnemyCharacterToTheList;


    public static event UnityAction AddTarget;
    public static void OnAddTarget() => AddTarget?.Invoke();
}
