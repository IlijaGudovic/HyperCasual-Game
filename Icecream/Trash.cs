using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            target.transform.GetChild(0).GetComponent<Animator>().SetBool("Ice", false);
            target.gameObject.GetComponent<Collection>().clearCollection();
        }
    }

}
