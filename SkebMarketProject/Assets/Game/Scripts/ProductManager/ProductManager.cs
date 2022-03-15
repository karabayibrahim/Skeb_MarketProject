using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public List<GameObject> Products = new List<GameObject>();
    void Awake()
    {
        ObjectAssigment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ObjectAssigment()
    {
        foreach (Transform item in GetComponentInChildren<Transform>())
        {
            if (item.GetComponent<Product>()!=null)
            {
                Products.Add(item.gameObject);
            }
        }
    }
}
