using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CaseSystem : MonoBehaviour
{
    private float _totalAmount;
    [SerializeField] TMP_Text _caseText;
    [SerializeField] Light _buyLight;
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
        yield return new WaitForSeconds(1f);
        _totalAmount += productCount;
        _caseText.text =_totalAmount.ToString() + "$";
        yield break;
    }
    private IEnumerator LightTime()
    {
        _buyLight.color = new Color(1, 0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        _buyLight.color = new Color(0, 0, 0, 0);
    }
}
