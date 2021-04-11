using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>()
                .Win(other.gameObject.transform.parent.GetComponent<PlayerStats>().GetPlayerNumber(), true);
        }
    }
}
