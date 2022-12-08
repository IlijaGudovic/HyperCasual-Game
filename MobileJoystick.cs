using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
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

        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            joystick.transform.position = touch.position;
            pivot.transform.position = touch.position;

            joystick.gameObject.SetActive(true);
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            pivot.transform.position = touch.position;

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
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            joystick.gameObject.SetActive(false);

            playerScript.direction = Vector3.zero;
        }

    }

    private void LateUpdate()
    {

        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            if ((pivot.transform.position - joystick.transform.position).magnitude > range)
            {
                joystick.transform.position = Vector3.Lerp(joystick.position, pivot.position, speed);
            }
        }
    }
}
