using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    public RectTransform joystick;

    [HideInInspector]
    public Vector3 direction;

    private Rigidbody rb;

    [SerializeField]
    public float speed = 260f;

    [SerializeField]
    private float angularVelocty = 0.14f;

    private Quaternion rotation;

    [SerializeField]
    private Animator anim = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (direction != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(direction, Vector3.up);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, angularVelocty);
        }
    }

}
