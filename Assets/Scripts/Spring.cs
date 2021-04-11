using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    public int leftForce;
    public int rightForce;
    public int upForce;
    public int downForce;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject; ;

        if (obj.name == "Body")
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Rigidbody2D characterBody = obj.GetComponent<Rigidbody2D>();
            characterBody.velocity = Vector2.up * 0;
            characterBody.velocity += Vector2.left * leftForce
                + Vector2.right * rightForce
                + Vector2.up * upForce
                + Vector2.down * downForce;
        }
    }
}
