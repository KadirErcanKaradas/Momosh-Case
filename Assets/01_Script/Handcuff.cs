using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Handcuff : MonoBehaviour
{
    private GameObject target;

    private void Awake()
    {
        target = GameObject.Find("handcuffTarget");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameController.Instance.handcuff.Count == 0)
            {
                transform.DOLocalJump(target.transform.localPosition, 5, 1, 0.5f)
                    .OnComplete(() => gameObject.transform.position = target.transform.position);
                GameController.Instance.handcuff.Add(gameObject);
                gameObject.transform.parent = target.transform;
            }
            else
            {
                GameObject obj = GameController.Instance.handcuff[GameController.Instance.handcuff.Count - 1];
                Transform followHandcuff = obj.transform;
                transform.DOLocalJump(target.transform.localPosition, 10, 1, 0.5f)
                    .OnComplete(() => gameObject.transform.position = followHandcuff.transform.position+new Vector3(0,0.5f,0));
                GameController.Instance.handcuff.Add(gameObject);
                gameObject.transform.parent = target.transform;
            }
        }
    }
}
