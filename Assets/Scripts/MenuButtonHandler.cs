using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonHandler : MonoBehaviour
{
    public void ReturnToMenu()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().ReturnToMenu();
    }
}
