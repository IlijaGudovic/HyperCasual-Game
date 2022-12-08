using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    
    public List<GameObject> list = new List<GameObject>();

    public Transform startingPoint;
    public Transform height;

    public GameObject itemTemplate;

    [HideInInspector]
    public bool ready = true;

    public AudioManager audioManager;

    private void Start()
    {
        height = new GameObject().GetComponent<Transform>();
        height.SetParent(this.transform);
    }

    public void add(Item item)
    {

        if (!ready)
        {
            return;
        }

        if (list.Count == 0)
        {

            GameObject template = Instantiate(itemTemplate, startingPoint.position, Quaternion.identity);

            template.GetComponent<ItemDisplay>().spawn(item);

            template.transform.SetParent(this.transform);

            list.Add(template);

            height.position = startingPoint.position;
            height.position += Vector3.up * item.height;
        }
        else if (true)
        {

            GameObject template = Instantiate(itemTemplate, height.position, Quaternion.identity);

            template.GetComponent<ItemDisplay>().spawn(item);

            template.transform.SetParent(this.transform);

            height.position += Vector3.up * item.height;

            list.Add(template);

        }

        transform.GetChild(0).GetComponent<Animator>().SetBool("Ice", true);

        audioManager.play("Take");

    }

    public void clearCollection()
    {
        foreach (var item in list)
        {
            Destroy(item);
        }

        list.Clear();
        height.position = startingPoint.position;
    }

    public void emptyList()
    {
        StartCoroutine(eat());
        ready = false;
    }

    private IEnumerator eat()
    {

        if (list.Count == 0)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Ice", false);
            ready = true;
            yield break;
        }

        Destroy(list[list.Count - 1]);
        list.RemoveAt(list.Count - 1);

        audioManager.play("Pufna");

        yield return new WaitForSeconds(0.12f);

        StartCoroutine(eat());

    }

}
