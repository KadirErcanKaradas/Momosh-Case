using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private Rigidbody rb;
    public Animator anim;
    private FloatingJoystick joystick;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveForce, rb.velocity.y, joystick.Vertical * moveForce);


        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            anim.SetBool("walk", true);
            GameController.Instance.isWalk = true;
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            anim.SetBool("walk", false);
            GameController.Instance.isWalk = false;
        }
    }
}
