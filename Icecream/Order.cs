using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{

    [SerializeField]
    private Item[] store = null;

    [SerializeField]
    private Item[] cone = null;

    [HideInInspector]
    public int check;

    public string[] orderList;

    private float height = 0;

    public Sprite sprite;

    private int size;

    public Color color;

    private void Start()
    {
        newOrder();
    }

    private void newOrder()
    {

        size = Random.Range(2,6);

        orderList = new string[size];

        height = 0;

        //Cone
        int random = Random.Range(0, cone.Length);
        orderList[0] = cone[random].name + "(Clone)";
        Vector3 position = transform.position;
        position.y = 2.4f + height;

        Instantiate(cone[random].prefab, position, cone[random].prefab.transform.rotation).transform.SetParent(transform);

        height += cone[random].height;

        //Ice
        for (int i = 1; i < size; i++)
        {

            int index = Random.Range(0, store.Length);

            orderList[i] = store[index].name + "(Clone)";

            Vector3 spawnPosition = transform.position;
            spawnPosition.y = 2.4f + height;

            Instantiate(store[index].prefab, spawnPosition, store[index].prefab.transform.rotation).transform.SetParent(transform);

            height += store[index].height;

        }

        GameObject claud = new GameObject();
        claud.AddComponent<SpriteRenderer>().sprite = sprite;
        claud.GetComponent<SpriteRenderer>().color = color;

        claud.transform.position = new Vector3(transform.position.x, 2.4f + height / 2 - 0.2f + 0.2f, transform.position.z);
        claud.transform.localScale = new Vector3(1.2f, height + 0.2f, 1.2f);
        claud.transform.SetParent(transform);

        check = size * 5;

    }

    public int checkList(Collection playerCollection)
    {

        if (playerCollection.list.Count != size)
        {
            return 0;
        }

        for (int i = 0; i < size; i++)
        {
            if (playerCollection.list[i].transform.GetChild(0).name != orderList[i])
            {
                Debug.Log(playerCollection.list[i].transform.GetChild(0).name + " != " + orderList[i]);
                return 0;
            }
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Invoke("newOrder", 2f);

        return check;

    }

}
