using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance;
    public List<Transform> allyPossibleTargetList;
    public List<Transform> enemyPossibleTargetList;
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private GameObject allyParent;
    private void Awake()
    {
        Instance = this;
        enemyParent = FindObjectOfType<EnemyParent>().gameObject;
        allyParent = FindObjectOfType<AllyParent>().gameObject;
        AddAllAllyCharactersToTheList();
        AddAllEnemyCharactersToTheList();
    }
    private void AddAllAllyCharactersToTheList()
    {
        foreach (Transform child in allyParent.GetComponentInChildren<Transform>())
        {
            allyPossibleTargetList.Add(child);
        }
    }
    private void AddAllEnemyCharactersToTheList()
    {
        foreach (Transform child in enemyParent.GetComponentInChildren<Transform>())
        {
            enemyPossibleTargetList.Add(child);
        }
    }


    /*private void EventWithParametersExample()
    {
        private void Start()
        {
            if (EventManager.AddAllyCharacterToTheList == null)
            {
                EventManager.AddAllyCharacterToTheList = new MyAllyCharacterEvent();
            }
            EventManager.AddAllyCharacterToTheList.AddListener(AddTarget);
        }
        private void Update()
        {
            foreach (var child in allyTargetList)
            {
                if (child == null)
                {
                    allyTargetList.Remove(child);
                }
            }
        }
        private void AddTarget(Transform target)
        {
            allyTargetList.Add(target);
        }
    }*/
}
