using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTool : MonoBehaviour
{

    [SerializeField]
    private Transform spawnPosition = null;

    [SerializeField]
    private Transform endPosition = null;

    [SerializeField]
    private List<Item> items = null;

    private int index;

    [SerializeField]
    private float cooldwon = 2f;

    [SerializeField]
    private float itemSpeed = 4f;

    private void Start()
    {
        index = 0;
        StartCoroutine(spawnItem());
    }

    private IEnumerator spawnItem()
    {

        //Logic

        GameObject newItem = Instantiate(items[index].prefab, spawnPosition.position, Quaternion.identity);

        newItem.AddComponent<BoxCollider>();
        newItem.AddComponent<Rigidbody>();

        BoxCollider trigger = newItem.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size *= 3f;

        MovingItem script = newItem.AddComponent<MovingItem>();

        Vector3 dir = endPosition.position - spawnPosition.position;

        script.direction = dir;
        script.speed = itemSpeed;

        script.item = items[index];

        index++;

        if (index >=  items.Count)
        {
            index = 0;
        }

        yield return new WaitForSeconds(cooldwon);

        StartCoroutine(spawnItem());

    }

}
