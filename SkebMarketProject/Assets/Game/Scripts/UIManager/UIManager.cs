using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Obi;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    public Button ChangeButton;
    public Button RestartButton;
    public Button TapButton;
    public Button NextButton;
    public TMP_Text BagRightText;
    public TMP_Text PlayerMoneyText;
    public GameObject WinPanel;
    public GameObject FailPanel;
    public GameObject GameStartPanel;
    public GameObject InGamePanel;
    void Start()
    {
        BagRightText.text = GameManager.Instance.PlayerController.BagRight.ToString();
        ChangeButton.onClick.AddListener(ChangeBag);
        RestartButton.onClick.AddListener(RestartStatus);
        TapButton.onClick.AddListener(TapStartStatus);
        NextButton.onClick.AddListener(NextStatus);
        GameManager.WinAction += WinStatus;
        GameManager.FailAction += FailStatus;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        ChangeButton.onClick.RemoveListener(ChangeBag);
        RestartButton.onClick.RemoveListener(RestartStatus);
        GameManager.WinAction -= WinStatus;
        GameManager.FailAction -= FailStatus;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBag() 
    {
        GameManager.Instance.Bag.GetComponent<ObiSolver>().gravity = new Vector3(0, -10, 0);
        StartCoroutine(ChangeBagTimer());
        GameManager.Instance.Bag.transform.DOMoveZ(-1.6f, 1f).SetEase(Ease.InOutExpo).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            GameManager.Instance.PlayerController.BagRight--;
            GameManager.Instance.Bag.MyProducts.Clear();
            GameManager.Instance.Bag.ProductCount = 0;
            GameManager.Instance.Bag.GetComponent<ObiSolver>().gravity = new Vector3(0, 0, 0);
        });
    }

    public void FailStatus()
    {
        StartCoroutine(FailTimer());
    }

    private IEnumerator FailTimer()
    {
        yield return new WaitForSeconds(2f);
        FailPanel.SetActive(true);
        GameManager.Instance.PlayerController.enabled = false;
        GameManager.Instance.Casier.enabled = false;
    }

    public void TapStartStatus()
    {
        Time.timeScale = 1;
        GameStartPanel.SetActive(false);
        InGamePanel.SetActive(true);
        
    }

    public void NextStatus()
    {

    }
    

    public void WinStatus()
    {
        WinPanel.SetActive(true);
        GameManager.Instance.PlayerController.enabled = false;
        GameManager.Instance.Casier.enabled = false;
    }

    public void RestartStatus()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ChangeBagTimer()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.Bag.gameObject.SetActive(false);
        GameManager.Instance.Bag.ResetMaterialColor();
        foreach (var item in GameManager.Instance.Bag.MyProducts)
        {
            Destroy(item.gameObject);
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.Bag.gameObject.SetActive(true);
    }
}
