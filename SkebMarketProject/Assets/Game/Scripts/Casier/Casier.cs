using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SA;
public class Casier : MonoBehaviour
{
    //private Animator _anim;
    [SerializeField] private GameObject _selectObject;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _target2;
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject LeftElbow;
    [SerializeField] private GameObject RightElbow;
    [SerializeField] private GameObject Head;
    private bool _lefthand = false;
    private bool _righthand = false;
    [SerializeField] Transform rightPos;
    [SerializeField] Transform leftPos;
    Tween left;
    Tween right;

    void Start()
    {
        LeftHand.transform.eulerAngles = new Vector3(0, -45, 30);
        RightHand.transform.eulerAngles = new Vector3(0, 90, -30);
        //_anim = GetComponent<Animator>();
        ObjectSelect();
    }

    // Update is called once per frame
    void Update()
    {
        //Head.transform.position = _selectObject.transform.position;
        if (_selectObject!=null)
        {
            Head.transform.LookAt(_selectObject.transform);
        }
        if (_lefthand)
        {
            //LeftHand.transform.position = _selectObject.transform.position;
            left = LeftHand.transform.DOMove(new Vector3(_selectObject.transform.position.x, _selectObject.transform.position.y + 0.1f, _selectObject.transform.position.z), 0.1f);
            //LeftElbow.transform.DOMove(new Vector3(_selectObject.transform.position.x, _selectObject.transform.position.y + 0.1f, _selectObject.transform.position.z), 0.1f);
        }
        else
        {
            LeftHand.transform.localPosition = leftPos.localPosition;
        }
        if (_righthand)
        {
            //RightHand.transform.position = _selectObject.transform.position;
            right = RightHand.transform.DOMove(new Vector3(_selectObject.transform.position.x, _selectObject.transform.position.y+0.1f, _selectObject.transform.position.z), 0.1f);
            //RightElbow.transform.DOMove(new Vector3(_selectObject.transform.position.x, _selectObject.transform.position.y + 0.1f, _selectObject.transform.position.z), 0.1f);
        }
        else
        {
            RightHand.transform.localPosition = rightPos.localPosition;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //if (_lefthand)
        //{
        //    _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
        //    _anim.SetIKPosition(AvatarIKGoal.LeftHand, _selectObject.transform.position);
        //}
        //if (_righthand)
        //{
        //    _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
        //    _anim.SetIKPosition(AvatarIKGoal.RightHand, _selectObject.transform.position);
        //}

    }
    private void ObjectSelect()
    {
        if (GameManager.Instance.CurrentLevel.ProductManager.Products.Count>0)
        {
            _selectObject = GameManager.Instance.CurrentLevel.ProductManager.Products[0].gameObject;
            GameManager.Instance.CurrentLevel.ProductManager.Products.Remove(GameManager.Instance.CurrentLevel.ProductManager.Products[0]);
            _lefthand = true;
            _selectObject.GetComponent<Product>().TypeRotation();
            //LeftHand.transform.rotation = new Quaternion(0, -90f, 0, 0);
            _selectObject.transform.DOMove(_target.transform.position, 1f).OnComplete(() =>
            {
                int rndTarget = Random.Range(0, GameManager.Instance.CurrentLevel.Targets.Count);
                _target2 = GameManager.Instance.CurrentLevel.Targets[rndTarget];
                _lefthand = false;
                _selectObject.GetComponent<Product>().BuyMethod();
                left.Kill();
                LeftHand.transform.DOLocalMove(leftPos.localPosition, 1f);
                _righthand = true;
                //RightHand.transform.eulerAngles = new Vector3(0, -90f, 0);
                _selectObject.transform.DOMove(_target2.transform.position, 1f).OnComplete(() =>
                {
                    _righthand = false;
                    right.Kill();
                    //RightHand.transform.localPosition = rightPos.localPosition;
                    RightHand.transform.DOLocalMove(rightPos.localPosition, 1f);
                    _selectObject.GetComponent<Product>().Move = true;
                    _selectObject.GetComponent<Rigidbody>().isKinematic = false;
                    _selectObject.GetComponent<Product>().Takeable = true;
                    if (GameManager.Instance.CurrentLevel.ProductManager.Products.Count > 0)
                    {
                        ObjectSelect();
                    }
                    else
                    {
                        ResetHandPos();
                        GetComponent<FullBodyIKBehaviour>().fullBodyIK.bodyEffectors.hips.rotationEnabled = false;
                    }
                });
            });
            //_anim.CrossFade("Take", 0.01f);
        }
        //else
        //{
        //    ResetHandPos();
        //}
        
    }
    private void ResetHandPos()
    {
        Debug.Log("bitti");
        left.Kill();
        right.Kill();
        _righthand = false;
        _lefthand = false;
        RightHand.transform.localPosition = rightPos.localPosition;
        LeftHand.transform.localPosition = leftPos.localPosition;
    }


}
