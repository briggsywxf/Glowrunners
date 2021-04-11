using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{

    public int speedBoost;

     private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject obj = other.gameObject;;
       
        if (obj.name == "Lower Left Leg" || obj.name == "Lower Right Leg"){
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Rigidbody2D rb = obj.transform.parent.gameObject.transform.Find("Body").GetComponent<Rigidbody2D>();
            rb.velocity += Vector2.right * speedBoost;
        }
    }
}
