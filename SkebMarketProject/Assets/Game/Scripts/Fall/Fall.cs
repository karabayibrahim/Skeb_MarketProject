using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Product>()!=null)
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Collider>().enabled = true;
            other.GetComponent<Product>().enabled = false;
            if (other.gameObject.GetComponent<Product>().InBag==false&& other.gameObject.GetComponent<Product>().InTake==false)
            {
                Destroy(other.gameObject, 2f);
            }
            
        }
    }
}
