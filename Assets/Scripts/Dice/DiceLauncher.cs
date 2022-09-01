using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceLauncher : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private GameObject cooldownIndicator;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float forceMultiplier = 1;
    private bool canLaunch = true;
    private float cooldown = 2f;
    private float curCooldown = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (canLaunch)
            {
                EndDrag();
            }
        }
        if (!canLaunch)
        {
            StartCooldown();
            CooldownIndicator();
        }
    }
    private void StartDrag()
    {
        startPosition = Input.mousePosition;
    }

    private void ContinueDrag()
    {
        endPosition = Input.mousePosition;
    }
    private void EndDrag()
    {
        Vector3 direction =  startPosition - endPosition;
        Launch(direction);
        canLaunch = false;
        curCooldown = 0f;
    }
    private void Launch(Vector3 force)
    {
        var dice = InstantiateDice();
        dice.GetComponent<Rigidbody>().AddForce(new Vector3(force.x / 3f, force.y / 3f, force.y / 3f) * forceMultiplier);
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
        }
    }
    private void CooldownIndicator()
    {
        float calcCooldown = curCooldown / cooldown;
        cooldownIndicator.transform.localScale = new Vector3(Mathf.Clamp(calcCooldown, 0, 1), Mathf.Clamp(calcCooldown, 0, 1), cooldownIndicator.transform.localScale.z);
    }
}
