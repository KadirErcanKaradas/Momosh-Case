using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guilty : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    private CapsuleCollider cc;
    public Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (GameController.Instance.isWalk==true)
        {
            anim.SetBool("walk",true);
        }
        else
        {
            anim.SetBool("walk",false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameController.Instance.handcuff.Count>0)
        {
            if (GameController.Instance.guilty.Count == 0)
            {
                GameController.Instance.guilty.Add(gameObject);
                gameObject.transform.parent = target.transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                GameController.Instance.RemoveHandcuff(transform);
                Destroy(rb);
                cc.isTrigger = false;
                anim.enabled = true;
            }
            else
            {
                gameObject.transform.parent = target.transform;
                gameObject.transform.localPosition = new Vector3(0, 0, GameController.Instance.guilty[GameController.Instance.guilty.Count-1].transform.localPosition.z-1f);
                GameController.Instance.RemoveHandcuff(transform);
                gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                GameController.Instance.guilty.Add(gameObject);
                Destroy(rb);
                cc.isTrigger = false;
                anim.enabled = true;
            }
        }
    }
}
