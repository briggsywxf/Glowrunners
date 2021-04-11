using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGoo : MonoBehaviour
{
    [SerializeField] private int damageDealt = 10;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent.GetComponent<PlayerStats>().TakeDamage(damageDealt);
        }
    }
}
