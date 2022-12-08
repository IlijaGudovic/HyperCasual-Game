using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{

    public Vector3 direction;
    public float speed = 2;
    public Item item;

    private void OnCollisionEnter(Collision collision)
    {
        transform.forward = - direction;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.velocity = - transform.forward * speed;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            if (target.gameObject.GetComponent<Collection>().list.Count == 0)
            {
                target.gameObject.GetComponent<Collection>().add(item);
                Destroy(gameObject);
            }
        }
    }

   

}
