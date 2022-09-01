using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDiceLauncher : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private GameObject cooldownIndicator;
    private float forceMultiplier = 1;
    private bool canLaunch = false;
    private float cooldown = 2f;
    private float curCooldown = 0;

    private void Update()
    {
        if (canLaunch) LaunchDice();
        if (!canLaunch) 
        {
            StartCooldown();
            CooldownIndicator();
        } 
    }

    private void LaunchDice()
    {
        Vector3 direction = new Vector3(Random.Range(-200,200), Random.Range(400,1500));
        Launch(direction);
        canLaunch = false;
    }
    private void Launch(Vector3 force)
    {
        var dice = InstantiateDice();
        dice.GetComponent<Rigidbody>().AddForce(new Vector3(force.x, force.y / 3f, -force.y / 3f) * forceMultiplier);
    }

    private GameObject InstantiateDice()
    {
        return Instantiate(dicePrefab, transform.position + Vector3.up, Quaternion.identity);
    }
    private void StartCooldown()
    {
        curCooldown += Time.deltaTime;
        if (curCooldown >= cooldown)
        {
            canLaunch = true;
            curCooldown = 0f;
        }
    }
    private void CooldownIndicator()
    {
        float calcCooldown = curCooldown / cooldown;
        cooldownIndicator.transform.localScale = new Vector3(Mathf.Clamp(calcCooldown, 0, 1), Mathf.Clamp(calcCooldown, 0, 1), cooldownIndicator.transform.localScale.z);
    }
}
