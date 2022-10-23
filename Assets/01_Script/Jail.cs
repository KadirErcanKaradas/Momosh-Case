using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

    public class Jail : MonoBehaviour
    {
        private GameObject target;
        public List<GameObject> handcuffObj = new List<GameObject>();
        private void Awake()
        {
            target = GameObject.Find("handcuffTarget");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Guilty"))
            {
                print("1");
                if (GameController.Instance.handcuff.Count == 0)
                {
                    handcuffObj[0].transform.DOLocalJump(target.transform.localPosition, 5, 1, 0.5f)
                        .OnComplete(() => handcuffObj[0].transform.position = target.transform.position);
                    GameController.Instance.handcuff.Add(handcuffObj[0]);
                    handcuffObj[0].transform.parent = target.transform;
                    handcuffObj.RemoveAt(0);
                    print("2");
                }
                else
                {
                    GameObject obj = GameController.Instance.handcuff[GameController.Instance.handcuff.Count - 1];
                    Transform followHandcuff = obj.transform;
                    handcuffObj[0].transform.DOLocalJump(target.transform.localPosition, 10, 1, 0.5f)
                        .OnComplete(() => handcuffObj[0].transform.position = followHandcuff.transform.position+new Vector3(0,0.5f,0));
                    GameController.Instance.handcuff.Add(handcuffObj[0]);
                    handcuffObj[0].transform.parent = target.transform;
                    handcuffObj.RemoveAt(0);
                    print("3");
                }

            }
        }
    }
