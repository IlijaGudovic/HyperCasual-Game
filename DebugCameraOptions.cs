using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCameraOptions : MonoBehaviour
{
    [SerializeField]
    private Transform cameraSettings = null;


    public Transform player;

    private Transform myCamera;

    public float cameraZoom;
    public float cameraDist;
    public float cameraAngle;
    public float cameraShift;

    private void Start()
    {
        myCamera = Camera.main.transform;

        loadData();

        setValues();
    }

    private void Update()
    {

        Vector3 cameraPos;

        float maxLeft = cameraDist;

        float left = (maxLeft * cameraShift) / 45;

        cameraPos = player.position + Vector3.back * cameraDist + Vector3.up * cameraZoom + Vector3.right * left;

        myCamera.position = cameraPos;

        //Camera Rotations
        //Vector3 newRotation = new Vector3(cameraAngle, player.transform.eulerAngles.y, 0);
        Vector3 newRotation = new Vector3(cameraAngle, - cameraShift, 0);
        myCamera.eulerAngles = newRotation;

    }

    private void setValues()
    {
        cameraSettings.GetChild(0).GetComponent<Scrollbar>().value = cameraZoom / 20;
        cameraSettings.GetChild(1).GetComponent<Scrollbar>().value = cameraDist / 20;
        cameraSettings.GetChild(2).GetComponent<Scrollbar>().value = cameraAngle / 90;
        cameraSettings.GetChild(3).GetComponent<Scrollbar>().value = cameraShift / 45;
        cameraSettings.GetChild(4).GetComponent<Scrollbar>().value = player.gameObject.GetComponent<PlayerMovment>().speed / 500;
    }

    private float inputValue;

    public void slide(Scrollbar slider)
    {
        inputValue = slider.value;
    }

    //Order Trnsition

    public void order(int option)
    {

        Transform scrolbar = cameraSettings.GetChild(option);

        float value = 0;

        switch (option)
        {

            case 0:
                value = convert(20);
                cameraZoom = value;
                break;
            case 1:
                value = convert(20);
                cameraDist = value;
                break;
            case 2:
                value = convert(90);
                cameraAngle = value;
                break;
            case 3:
                value = convert(45);
                cameraShift = value;
                break;
            case 4:
                value = convert(500);
                player.gameObject.GetComponent<PlayerMovment>().speed = value;
                break;
            default:
                break;
        }

        scrolbar.GetChild(2).GetComponent<Text>().text = value.ToString();

    }


    //Convert Data
    public float convert(float maxValue)
    {
        return (int)(inputValue * maxValue);
    }

    //Load Data
    private void loadData()
    {
        cameraZoom = PlayerPrefs.GetFloat("camera_zoom", 17);
        cameraDist = PlayerPrefs.GetFloat("camera_dist", 11);
        cameraAngle = PlayerPrefs.GetFloat("camera_angle", 50);
        cameraShift = PlayerPrefs.GetFloat("camera_shift", 45);

        player.gameObject.GetComponent<PlayerMovment>().speed = PlayerPrefs.GetFloat("player_speed", 300);
    }


    //Save Data
    public void saveData()
    {
        PlayerPrefs.SetFloat("camera_zoom", cameraZoom);
        PlayerPrefs.SetFloat("camera_dist", cameraDist);
        PlayerPrefs.SetFloat("camera_angle", cameraAngle);
        PlayerPrefs.SetFloat("camera_shift", cameraShift);

        PlayerPrefs.SetFloat("player_speed", player.gameObject.GetComponent<PlayerMovment>().speed);
    }

    public void muteAudio()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

}
