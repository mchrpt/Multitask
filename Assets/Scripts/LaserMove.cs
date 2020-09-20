using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public float engineAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.left * .25f);
        transform.Translate(Vector3.right * (engineAmount / 10), Space.World);
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock")
        {
            Destroy(gameObject, 0.05f);
        }
    }
}
