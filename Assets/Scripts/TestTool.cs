using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTool : MonoBehaviour
{
    [SerializeField] private GameObject allyPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform allyParent;
    [SerializeField] private Transform enemyBackLine;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform allyBackLine;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(allyPrefab, allyParent.position, Quaternion.identity, allyParent);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(allyPrefab, enemyBackLine.position, Quaternion.identity, allyParent);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(enemyPrefab, enemyParent.position, Quaternion.identity, enemyParent);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(enemyPrefab, allyBackLine.position, Quaternion.identity, enemyParent);
        }
    }
}
