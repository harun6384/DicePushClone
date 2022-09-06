using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private GameObject[] MultiplierPrefabs;
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;

    private float spawnTimer = 5f;
    private bool canSpawn = true;
    private void Update()
    {
        RandomSpawner();
    }
    private void RandomSpawnSomeThing()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0f, Random.Range(-size.z / 2, size.z / 2));
        var instantiatedPrefab = Random.Range(0, MultiplierPrefabs.Length);
        Instantiate(MultiplierPrefabs[instantiatedPrefab], pos, Quaternion.identity);
    }
    private void RandomSpawner()
    {
        if (canSpawn)
        {
            if (spawnTimer > 0)
            {
                spawnTimer -= Time.deltaTime;
            }
            else
            {
                RandomSpawnSomeThing();
                spawnTimer = 5f;
            }
        }
        else
        {
            return;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
