using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayFadeIn());
    }

    IEnumerator PlayFadeIn()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = true;
        for (float i = 1f; i >= 0f; i -= 0.005f)
        {
            renderer.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
