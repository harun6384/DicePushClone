using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceLauncher : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
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
        if (!canLaunch) StartCooldown();
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
    }
    private void Launch(Vector3 force)
    {
        var dice = InstantiateDice();
        dice.GetComponent<Rigidbody>().AddForce(new Vector3(force.x, force.y / 10f, force.y) * forceMultiplier);
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
}
