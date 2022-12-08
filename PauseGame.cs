using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{

    [SerializeField]
    private GameObject optionsPaenl = null;

    private void Start()
    {
        Button pauseButton = GameObject.Find("Pause Button").GetComponent<Button>();

        if (!optionsPaenl.activeSelf)
        {
            pauseButton.GetComponentInChildren<Text>().text = "Debug";
        }
        else
        {
            pauseButton.GetComponentInChildren<Text>().text = "Close";
        }
    }

    public void pauseGame()
    {

        Button pauseButton = GameObject.Find("Pause Button").GetComponent<Button>();

        if (optionsPaenl.activeSelf)
        {

            optionsPaenl.SetActive(false);
            pauseButton.GetComponentInChildren<Text>().text = "Debug";
        }
        else
        {
            optionsPaenl.SetActive(true);
            pauseButton.GetComponentInChildren<Text>().text = "Close";
        }

        transform.GetComponent<DebugCameraOptions>().saveData();

    }




}
