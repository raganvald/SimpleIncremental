﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyTargeting : MonoBehaviour
{
    public event TargetChangedHandler OnNewTargetAcquired;
    public delegate void TargetChangedHandler();

    public event TargetChangedHandler OnTargetLost;

    public Transform target = null;

    private void OnDisable()
    {
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target == null)
        {
            if (collision.CompareTag("Player"))
            {
                CharacterHealth ch = collision.GetComponent<CharacterHealth>();
                target = ch.transform;
                OnNewTargetAcquired?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target == collision.transform)
        {
            target = null;
            OnTargetLost?.Invoke();
        }
    }

    public void PlayerDied()
    {
        target = null;
        OnTargetLost?.Invoke();
    }
}
