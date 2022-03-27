using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
public class Bag : MonoBehaviour
{
    public List<Product> MyProducts = new List<Product>();
    [SerializeField] private Material myMaterial;
    [SerializeField] private Material deformMaterial;
    [SerializeField] private int _productCount;
    [SerializeField] private float materialValue;
    [SerializeField] private int deformationValue;
    [SerializeField] private GameObject objMain;
    [SerializeField] private GameObject objLeft;
    [SerializeField] private GameObject objRight;


    public Vector3 MyPosition;

    public int ProductCount
    {
        get
        {
            return _productCount;
        }
        set
        {
            if (ProductCount == value)
            {
                return;
            }
            _productCount= value;
            materialValue= Mathf.InverseLerp(0, deformationValue, ProductCount);
            myMaterial.Lerp(myMaterial, deformMaterial, materialValue);
            //myMaterial.color = new Color(myMaterial.color.r, myMaterial.color.g - 0.5f, myMaterial.color.b-0.5f,myMaterial.color.a);
            if (ProductCount > deformationValue)
            {
                objMain.GetComponent<SkinnedMeshRenderer>().enabled = false;
                objLeft.SetActive(true);
                objRight.SetActive(true);
                foreach (var item in MyProducts)
                {
                    item.GetComponent<ObiRigidbody>().kinematicForParticles = true;
                }
                GameManager.FailAction?.Invoke();
                //foreach (var item in MyProducts)
                //{
                //    item.GetComponent<Rigidbody>().mass = 10f;
                //}
            }
        }
    }

    void Start()
    {
        //transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        MyPosition = transform.position;
        GameManager.Instance.Bag = GetComponent<Bag>();
        myMaterial = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Product>() != null)
        {
            other.gameObject.GetComponent<Product>().InBag = true;
            if (other.gameObject.GetComponent<Product>().Fall)
            {
                other.gameObject.transform.SetParent(gameObject.transform);
                MyProducts.Add(other.gameObject.GetComponent<Product>());
                GameManager.Instance.PlayerController.MyBagCount += other.gameObject.GetComponent<Product>().MyAmount;
                ProductCount++;
                other.gameObject.GetComponent<Product>().Fall = false;
            }
            
        }
    }

    public void ShakeEffect()
    {
        StartCoroutine(ShakeTimer());
    }
    private IEnumerator ShakeTimer()
    {
        Debug.Log("Shake");
        GetComponent<ObiSolver>().gravity = new Vector3(0, -10, 0);
        //GetComponentInChildren<ObiSoftbody>().plasticCreep = 1f;
        //GetComponentInChildren<ObiSoftbody>().plasticRecovery = 1f;
        //GetComponentInChildren<ObiSoftbody>().plasticYield = 1f;
        yield return new WaitForSeconds(0.5f);
        GetComponent<ObiSolver>().gravity = new Vector3(0,0, 0);
        //GetComponent<ObiSolver>().parameters.damping = 1f;
        //GetComponentInChildren<ObiSoftbody>().plasticCreep = 0;
        //GetComponentInChildren<ObiSoftbody>().plasticRecovery = 0;
        //GetComponentInChildren<ObiSoftbody>().plasticYield = 0f;
        //yield return new WaitForSeconds(0.1f);
        //GetComponent<ObiSolver>().parameters.damping = 0.01f;
        yield break;
    }
}
