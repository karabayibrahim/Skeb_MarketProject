using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Product _myProduct;
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _productPosition;
    [SerializeField] private GameObject Hand;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        ProductColliderControl();
    }

    private void ProductColliderControl()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x+0.2f, transform.position.y,transform.position.z), transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) *1000, Color.white);
            if (hit.transform.GetComponent<Product>() != null)
            {
                Debug.Log("Did Hit");
                TakeProduct(hit.transform.GetComponent<Product>());
            }
            if (hit.transform.GetComponent<Bag>() != null)
            {
                LeaveProduct();
            }
        }
        else
        {
            Debug.DrawRay(new Vector3(transform.position.x+0.2f, transform.position.y, transform.position.z), transform.TransformDirection(-Vector3.up) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void TakeProduct(Product product)
    {
        if (_myProduct==null)
        {
            _myProduct = product;
            _myProduct.transform.SetParent(Hand.transform);
            _myProduct.transform.position = _productPosition.position;
            _anim.SetBool("Take", true);
            _myProduct.Move = false;
            gameObject.transform.DOMoveY(0.9f, 0.5f).OnComplete(() =>
            {
                gameObject.transform.DOMoveY(1, 0.5f);
            });
        }
        
    }
    private void LeaveProduct()
    {
        if (_myProduct!=null)
        {
            _anim.SetBool("Take", false);
            _myProduct.GetComponent<Rigidbody>().isKinematic = false;
            _myProduct.transform.SetParent(null);
            _myProduct = null;
        }
    }
}
