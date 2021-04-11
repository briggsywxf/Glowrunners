using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

    public int bounceFactor;

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject obj = other.gameObject; ;

        if (obj.name == "Lower Left Leg" || obj.name == "Lower Right Leg")
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Rigidbody2D rb = obj.transform.parent.gameObject.transform.Find("Body").GetComponent<Rigidbody2D>();
            //rb.velocity = Vector2.up * 0;
            rb.velocity += Vector2.up * bounceFactor;
        }
    }
}
