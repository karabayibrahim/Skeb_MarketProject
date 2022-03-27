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
    [SerializeField] private float myBagCount;
    [SerializeField] private float targetAmount;
    [SerializeField] private int bagRight = 3;
    public float HorizontalSpeed;
    public float VerticalSpeed;
    [SerializeField] private float _movementClampNegative;
    [SerializeField] private float _movementClampPositive;
    [SerializeField] private float _movementClampNegativeVer;
    [SerializeField] private float _movementClampPositiveVer;
    void Start()
    {
        GameManager.Instance.UIManager.PlayerMoneyText.text = MyBagCount.ToString() + "$";
    }

    public float MyBagCount
    {
        get
        {
            return myBagCount;
        }
        set
        {
            if (MyBagCount==value)
            {
                return;
            }
            myBagCount = value;
            GameManager.Instance.UIManager.PlayerMoneyText.text = MyBagCount.ToString() + "$";
            if (MyBagCount==targetAmount)
            {
                GameManager.WinAction?.Invoke();
            }
        }
    }

    public int BagRight
    {
        get
        {
            return bagRight;
        }
        set
        {
            if (BagRight==value)
            {
                return;
            }
            bagRight = value;
            GameManager.Instance.UIManager.BagRightText.text = BagRight.ToString();
            if (BagRight <= 0)
            {
                GameManager.Instance.UIManager.ChangeButton.enabled = false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
    }
    private void FixedUpdate()
    {
        ProductColliderControl();
        Debug.DrawRay(new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z), transform.TransformDirection(-Vector3.up) * 1000, Color.white);
    }

    private void ProductColliderControl()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x+0.4f, transform.position.y,transform.position.z), transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) *1000, Color.white);
            if (hit.transform.GetComponent<Product>() != null&&hit.transform.GetComponent<Product>().Takeable)
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
            Debug.Log("Did not Hit");
        }
    }

    private void TakeProduct(Product product)
    {
        if (_myProduct==null)
        {
            Hand.transform.DOLocalRotate(new Vector3(45, 90, 0f), 0.5f);
            _myProduct = product;
            _myProduct.GetComponent<Rigidbody>().isKinematic = true;
            _myProduct.InTake = true;
            _myProduct.transform.SetParent(Hand.transform);
            _myProduct.GetComponent<Collider>().enabled = false;
            _myProduct.transform.position = _productPosition.position;
            _anim.SetBool("Take", true);
            _myProduct.Move = false;
            gameObject.transform.DOMoveY(0.9f, 0.2f).OnComplete(() =>
            {
                gameObject.transform.DOMoveY(1, 0.2f);
            });
        }
        
    }

    private void HorizontalMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch _theTouch = Input.GetTouch(0);

            if (_theTouch.phase == TouchPhase.Moved)
            {
                Vector2 touchPos = _theTouch.deltaPosition;
                if (touchPos != Vector2.zero)
                {
                    transform.position += new Vector3(0, 0, touchPos.x * (HorizontalSpeed / 100) * Time.deltaTime);
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, _movementClampNegative, _movementClampPositive));
                }
            }
        }
    }

    private void VerticalMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch _theTouch = Input.GetTouch(0);

            if (_theTouch.phase == TouchPhase.Moved)
            {
                Vector2 touchPos = _theTouch.deltaPosition;
                if (touchPos != Vector2.zero)
                {
                    transform.position += new Vector3(touchPos.y * (VerticalSpeed / 100) * Time.deltaTime, 0, 0);
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, _movementClampNegativeVer, _movementClampPositiveVer), transform.position.y, transform.position.z);
                }
            }
        }
    }
    private void LeaveProduct()
    {
        if (_myProduct!=null)
        {
            Hand.transform.DOLocalRotate(new Vector3(90, 90, 0f), 0.1f);
            _anim.SetBool("Take", false);
            _myProduct.Takeable = false;
            _myProduct.Fall = true;
            _myProduct.GetComponent<Rigidbody>().isKinematic = false;
            _myProduct.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            _myProduct.GetComponent<Collider>().enabled = true;
            _myProduct.GetComponent<Product>().enabled = false;
            _myProduct.transform.SetParent(null);
            _myProduct.transform.SetParent(GameManager.Instance.Bag.transform);
            _myProduct = null;
        }
    }
}
