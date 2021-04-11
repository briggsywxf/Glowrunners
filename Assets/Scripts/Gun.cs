using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float range = 90f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float scanDelay = 0.5f;
    [SerializeField] private float shootDelay = 0.2f;
    [SerializeField] private float reloadDelay = 2f;
    [SerializeField] private int numProjectiles = 10;
    [SerializeField] private int angle;

    private bool scanning = true;
    private Vector2 raySource;

    // Start is called before the first frame update
    void Start()
    {
        raySource = transform.Find("Origin").gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (scanning)
        {
            float rZ = Mathf.SmoothStep(0,range,Mathf.PingPong(Time.time * rotationSpeed,1));
            transform.rotation = Quaternion.Euler(0,0,rZ-angle);

            RaycastHit2D hit = Physics2D.Raycast(raySource,
                -transform.up*700f);
            Debug.DrawRay(raySource, -transform.up*700f, Color.green, 0.5f);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        scanning = false;
        yield return new WaitForSeconds(scanDelay);
        for (int i = 0; i < numProjectiles; i++)
        {
            var newProjectile = Instantiate(projectile, gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().velocity = -transform.up * 300;
            yield return new WaitForSeconds(shootDelay);
        }

        yield return new WaitForSeconds(reloadDelay);
        scanning = true;
    }

}
