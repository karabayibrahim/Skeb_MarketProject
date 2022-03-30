using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;
public class Product : MonoBehaviour
{
    public bool Move = false;
    public bool Takeable = true;
    public bool InTake = false;
    public bool InBag = false;
    public bool Fall = false;
    public bool LastObject = false;
    public bool CounterControl = false;
    private float rnd;
    [SerializeField] private ProductType _myProductType;
    public float MyAmount;
    private Rigidbody _rb;
    void Start()
    {
        //_zValue=Random.Range(-0.1f, 0.1f);
        rnd= Random.Range(-5,5);
        _rb = GetComponent<Rigidbody>();
        LastObjectControl();
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            //transform.DOLocalRotate(new Vector3(0, -90, 0), 0.2f);
            //if (_myProductType == ProductType.CANDY)
            //{
            //    //transform.position+=new Vector3(Time.deltaTime * -0.3f, 0,0);
            //}
            //else
            //{
            //    transform.Translate(Time.deltaTime * -0.3f, 0, Time.deltaTime * _zValue);
            //}
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity=new Vector3(Time.deltaTime * -20f,0, Time.deltaTime *rnd);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bag" && InTake)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            GameManager.Instance.Bag.ShakeEffect();
            InTake = false;
            //Debug.Log("posetteyim");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Product"&&!CounterControl)
        {
            if (InBag==true)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                
            }
            else
            {
                if (other.gameObject.GetComponent<Product>().InBag==true||other.gameObject.GetComponent<Product>().CounterControl==true)
                {
                    GameManager.Instance.Bag.ProductCount++;
                    GameManager.Instance.Bag.MyProducts.Add(this);
                    CounterControl = true;
                    if (PlayerPrefs.GetInt("onOrOffVibration") == 1)
                        TapticManager.Impact(ImpactFeedback.Light);
                }
                
            }
        }
    }

    public void BuyMethod()
    {
        GameManager.Instance.CaseSystem.TextWrite(MyAmount);
    }

    public void LastObjectControl()
    {
        if (GameManager.Instance.CurrentLevel!=null)
        {
            if (gameObject == GameManager.Instance.CurrentLevel.ProductManager.Products[GameManager.Instance.CurrentLevel.ProductManager.Products.Count - 1])
            {
                LastObject = true;
            }
        }
        
    }

    public void TypeRotation()
    {
        switch (_myProductType)
        {
            case ProductType.CANDY:
                break;
            case ProductType.SALMON:
                gameObject.transform.eulerAngles = new Vector3(-5,72,-30);
                break;
            default:
                break;
        }
    }

    public void FallRotation()
    {
        switch (_myProductType)
        {
            case ProductType.CANDY:
                break;
            case ProductType.SALMON:
                gameObject.transform.eulerAngles = new Vector3(-90, 0,0);
                break;
            case ProductType.SALAMI:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            default:
                break;
        }
    }
}
