using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{

    [SerializeField]
    private Transform[] places = null;

    private bool ready = true;

    [SerializeField]
    private MoneyBank moneyBank = null;

    private void OnCollisionEnter(Collision target)
    {

        if (!ready)
        {
            return;
        }

        if (target.gameObject.CompareTag("Player"))
        {
            if (!target.gameObject.GetComponent<Collection>().ready)
            {
                ready = false;
                return;
            }

            float minDist = float.MaxValue;

            Transform order = null;

            foreach (var place in places)
            {
                if ((target.transform.position - place.position).magnitude < minDist)
                {
                    minDist = (target.transform.position - place.position).magnitude;

                    order = place;
                }
            }

            int check = order.GetComponent<Order>().checkList(target.gameObject.GetComponent<Collection>());

            if (check > 0)
            {
                target.gameObject.GetComponent<Collection>().emptyList();
                moneyBank.add(check);
                ready = false;
            }
        }
    }

    private void OnCollisionExit(Collision target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            ready = true;
        }
    }

}
