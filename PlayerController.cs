using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private RectTransform joystick;
    private RectTransform pivot;

    [SerializeField]
    public float speed = 0.2f;

    [SerializeField]
    public float range = 40f;

    [SerializeField]
    private float pivotLimit = 20f;

    private PlayerMovment playerScript;

    private GameObject pausePanel;

    private void Awake()
    {
        playerScript = gameObject.GetComponent<PlayerMovment>();

        joystick = playerScript.joystick;
        pivot = joystick.GetChild(0).GetComponent<RectTransform>();

        pausePanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (pausePanel.activeInHierarchy)
        {
            playerScript.direction = Vector3.zero;
            joystick.gameObject.SetActive(false);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            joystick.transform.position = Input.mousePosition;
            pivot.transform.position = Input.mousePosition;

            joystick.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButton(0)) 
        {
            pivot.transform.position = Input.mousePosition;

            Vector3 dir = (pivot.position - joystick.position);

            if (dir.magnitude > pivotLimit)
            {
                dir.Normalize();
                playerScript.direction.x = dir.x;
                playerScript.direction.z = dir.y;
            }
            else
            {
                playerScript.direction = Vector3.zero;
                //pivot.position = joystick.position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            joystick.gameObject.SetActive(false);

            playerScript.direction = Vector3.zero;
        }

    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if ((pivot.transform.position - joystick.transform.position).magnitude > range)
            {
                joystick.transform.position = Vector3.Lerp(joystick.position, pivot.position, speed);
            }
        }
    }

}
