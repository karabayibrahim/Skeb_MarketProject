using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Product : MonoBehaviour
{
    public bool Move = false;
    public bool Takeable = true;
    public bool InTake = false;
    public bool InBag = false;
    public bool Fall = false;
    private float _zValue;
    [SerializeField] private ProductType _myProductType;
    [SerializeField] private float _myAmount;
    void Start()
    {
        _zValue=Random.Range(-0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            //transform.DORotate(new Vector3(270, -90, 0), 0.5f);
            if (_myProductType == ProductType.CANDY)
            {
                transform.Translate(0, 0, Time.deltaTime * 0.3f);
            }
            else
            {
                transform.Translate(Time.deltaTime * -0.3f, 0, Time.deltaTime * _zValue);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bag" && InTake)
        {
            GameManager.Instance.Bag.ShakeEffect();
            InTake = false;
            //Debug.Log("posetteyim");
        }
    }

    public void BuyMethod()
    {
        GameManager.Instance.CaseSystem.TextWrite(_myAmount);
    }
}
