using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

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
            if (other.gameObject.GetComponent<Product>().Fall == false)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Collider>().enabled = false;
                other.GetComponent<ObiCollider>().enabled = false;
                other.GetComponent<Product>().enabled = false;
                other.gameObject.GetComponent<Product>().Takeable = false;
            }
            if (other.gameObject.GetComponent<Product>().InBag==false&& other.gameObject.GetComponent<Product>().InTake==false)
            {
                Destroy(other.gameObject, 2f);
            }
            if (other.gameObject.GetComponent<Product>().LastObject)
            {
                GameManager.FailAction?.Invoke();
            }
            
        }
    }
}
