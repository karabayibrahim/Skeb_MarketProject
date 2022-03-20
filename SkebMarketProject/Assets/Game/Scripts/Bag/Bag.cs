using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<Product> MyProducts = new List<Product>();
    [SerializeField] private Material myMaterial;
    [SerializeField] private int _productCount;

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
            myMaterial.color = new Color(myMaterial.color.r, myMaterial.color.g - 0.05f, myMaterial.color.b-0.05f,myMaterial.color.a);
            if (ProductCount > 15f)
            {
                foreach (var item in MyProducts)
                {
                    item.GetComponent<Rigidbody>().mass = 10f;
                }
            }
        }
    }

    void Start()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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
                ProductCount++;
                other.gameObject.GetComponent<Product>().Fall = false;
            }
            
        }
    }
}
