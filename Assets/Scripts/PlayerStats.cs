using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private int normalSpeed = 30;
    [SerializeField] public Vector2 walkLeftVector;
    [SerializeField] public Vector2 walkRightVector;

    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
        healthBar.SetMaxHealth(maxHealth);
       ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        transform.Find("Body").transform.Find("Blood Source").GetComponent<ParticleSystem>().Play();

        // Death
        if (currentHealth <= 0)
        {
            if (playerNumber == 1)
            {
                GameObject.Find("Game Manager").GetComponent<GameManager>().Win(2, false);
            }
            else
            {
                GameObject.Find("Game Manager").GetComponent<GameManager>().Win(1, false);
            }
        }

    }

    public void ChangeSpeed(int speed, int seconds)
    {
        walkLeftVector = Vector2.left * speed;
        walkRightVector = Vector2.right * speed;
        StartCoroutine(ChangeSpeedCountdown(seconds));
    }

    private IEnumerator ChangeSpeedCountdown(int seconds)
    {
        while (seconds > 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;
        }
        
        ResetSpeed();
    }

    private void ResetSpeed()
    {
        walkLeftVector = Vector2.left * normalSpeed;
        walkRightVector = Vector2.right * normalSpeed;
    }
}
