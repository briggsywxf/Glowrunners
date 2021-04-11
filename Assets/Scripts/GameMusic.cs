using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public void Play()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(IncreaseVolume());
    }

    IEnumerator IncreaseVolume()
    {
        for (float i = 0f; i <= 0.5f; i += 0.01f)
        {
            GetComponentInChildren<AudioSource>().volume = i;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
