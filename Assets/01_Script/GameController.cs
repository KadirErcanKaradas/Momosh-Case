using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public event EventHandler<OnGameStageChangedEventArgs> OnGameStageChanged;

    public static GameController Instance { get; private set; }
    public GameStage GameStage { get; private set; }

    public List<GameObject> handcuff = new List<GameObject>();
    public List<GameObject> guilty = new List<GameObject>();
    public bool isWalk = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SetGameStage(GameStage.Loaded);
    }

    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;

        OnGameStageChanged?.Invoke(this, new OnGameStageChangedEventArgs { gameStage = gameStage });

    }

    public class OnGameStageChangedEventArgs : EventArgs
    {
        public GameStage gameStage;
    }

    public void RemoveHandcuff(Transform targetPos)
    {
        GameObject obj = handcuff[handcuff.Count - 1];
        obj.transform.DOMoveY(5f, 0.5f).OnComplete(() =>
        {
            obj.transform.parent = targetPos;
            obj.transform.DOLocalMove(targetPos.GetChild(0).transform.localPosition, 0.5f);
            obj.transform.DOScale(Vector3.one / 100, 0.5f).OnComplete(() =>
            {
                handcuff.Remove(obj);
                obj.SetActive(false);
                obj.transform.parent = null;
                obj.transform.localScale = Vector3.one/2;
            });
        });
    }
}

public enum GameStage { NotLoaded, Loaded, Started, Win, Fail }
