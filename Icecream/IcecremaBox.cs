using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecremaBox : MonoBehaviour
{

    private bool ready = true;

    public Item item;

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            if (ready)
            {
                ready = false;

                target.gameObject.GetComponent<Collection>().add(item);
            }
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            ready = true;
        }
    }

}
