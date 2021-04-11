using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseVolume());
    }
    
    public void Play()
    {
        StartCoroutine(PlayTransition());
    }

    IEnumerator PlayTransition()
    {
        var overlay = GameObject.Find("Overlay Fade");
        for (float i = 0f; i <= 1f; i += 0.005f)
        {
            overlay.GetComponent<Image>().color = new Color(0, 0, 0, i);
            GetComponentInChildren<AudioSource>().volume -= i/200f;
            yield return new WaitForSeconds(0.005f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
