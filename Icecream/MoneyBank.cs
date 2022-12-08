using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBank : MonoBehaviour
{
    public int bank;

    [SerializeField]
    private Text score = null;

    private Vector3 position;

    [SerializeField]
    private Vector3 tableSize = Vector3.zero;

    [SerializeField]
    private Vector3 moneySize = Vector3.zero;

    [SerializeField]
    private GameObject prefab = null;

    private Vector3 offset;

    private bool ready = true;
    private List<GameObject> moneyList = new List<GameObject>();

    private AudioManager audioManager;
    private bool sound;

    private void Start()
    {
        offset = new Vector3(transform.position.x - tableSize.x / 2 + moneySize.x / 2, transform.position.y + tableSize.y / 2 + moneySize.y / 2, transform.position.z - tableSize.z / 2 + moneySize.z / 2 -0.1f);

        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();

        add(160);

    }

    public void add(int money)
    {

        checkPosition();

        moneyList.Add(Instantiate(prefab, position + offset, Quaternion.identity));
        moneyList[moneyList.Count - 1].transform.SetParent(this.transform);

        position.x += moneySize.x;

        if (money > 5)
        {
            add(money - 5);
        }

    }

    private void checkPosition()
    {

        if (position.x >= tableSize.x)
        {
            position.x = 0;
            position.z += moneySize.z;
        }

        if (position.z >= tableSize.z)
        {
            position.z = 0;
            position.y += moneySize.y;
        }

    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            if (ready)
            {
                StartCoroutine(collectMoney());
                ready = false;
            }
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            ready = true;

            //Reset Position
            if (moneyList.Count > 0)
            {
                position = moneyList[moneyList.Count - 1].transform.position - offset;
                position.x += moneySize.x;
            }
        }
    }

    private IEnumerator collectMoney()
    {

        if (moneyList.Count == 0)
        {
            position = Vector3.zero;
            yield break;
        }

        Destroy(moneyList[moneyList.Count - 1]);
        moneyList.RemoveAt(moneyList.Count - 1);

        bank += 5;
        score.text = bank.ToString();

        if (sound)
        {
            audioManager.play("Coin");
            sound = false;
        }
        else
        {
            sound = true;
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(collectMoney());

    }

    public bool take()
    {

        if (bank >= 5)
        {
            bank -= 5;
            score.text = bank.ToString();
            return true;
        }

        return false;

    }

}
