using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class CaseSystem : MonoBehaviour
{
    private float _totalAmount;
    [SerializeField] TMP_Text _caseText;
    [SerializeField] TMP_Text _bigcaseText;
    [SerializeField] Light _buyLight;
    [SerializeField] private GameObject smallScreen;
    [SerializeField] private GameObject bigScreen;
    void Start()
    {
        _buyLight.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextWrite(float productCount)
    {
        StartCoroutine(TimeText(productCount));
        StartCoroutine(LightTime());
    }

    private IEnumerator TimeText(float productCount)
    {
        _caseText.text = "+" + productCount.ToString() + "$";
        smallScreen.transform.DOScale(150f, 0.5f).SetLoops(2,LoopType.Yoyo);
        yield return new WaitForSeconds(1f);
        _totalAmount += productCount;
        _caseText.text = "0$";
        _bigcaseText.text =_totalAmount.ToString() + "$";
        //bigScreen.transform.DOScale(120f, 0.5f).SetLoops(2, LoopType.Yoyo);
        yield break;
    }
    private IEnumerator LightTime()
    {
        _buyLight.color = new Color(1, 0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        _buyLight.color = new Color(0, 0, 0, 0);
    }
}
