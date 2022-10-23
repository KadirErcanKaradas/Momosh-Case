using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PoliceStation : MonoBehaviour
{
    public List<GameObject> handcuffObj = new List<GameObject>();
    [SerializeField] private Transform targetJail;
    private Transform firstEntry;
    private GameObject target;
    public bool isCompt = false;

    private void Awake()
    {
        target = GameObject.Find("handcuffTarget");
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < GameController.Instance.guilty.Count; i++)
            {
                GameObject obj = GameController.Instance.guilty[i];
                obj.transform.parent = targetJail;
            }
            StartCoroutine(GuiltyGoTarget());
        }
    }
    public IEnumerator GuiltyGoTarget()
    {
        for (int i = 0; i < GameController.Instance.guilty.Count; i++)
        {
            GameObject obj = GameController.Instance.guilty[i];
            obj.transform.DOMove(targetJail.position, 2f).OnComplete(() =>
            {
                HandcuffBack();
            });
            obj.transform.parent = targetJail;
            yield return new WaitForSeconds(1f);
        }
        GameController.Instance.guilty.Clear();
    }

    public void HandcuffBack()
    {
        GameObject obj = handcuffObj[0];
        if (GameController.Instance.handcuff.Count==0)
        {
            obj.transform.DOJump(target.transform.position, 5,1,0.1f).OnComplete((() =>
            {
                obj.transform.position = target.transform.position;
                obj.transform.parent = target.transform;
                handcuffObj.RemoveAt(0);
                GameController.Instance.handcuff.Add(obj);
            }));
        }
        else
        {
            GameObject targetPos = GameController.Instance.handcuff[GameController.Instance.handcuff.Count - 1];
            Transform followHandcuff = targetPos.transform;
            obj.transform.DOJump(followHandcuff.position,10, 1,0.1f).OnComplete((() =>
            {
                obj.transform.position= followHandcuff.transform.position + new Vector3(0, 0.5f, 0);
                obj.transform.parent = target.transform;
                handcuffObj.RemoveAt(0);
                GameController.Instance.handcuff.Add(obj);
            }));
        }
    }
}
