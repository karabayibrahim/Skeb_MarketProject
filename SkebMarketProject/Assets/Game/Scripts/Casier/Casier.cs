using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Casier : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _selectObject;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _target2;
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject Head;
    private bool _lefthand = false;
    private bool _righthand = false;
    [SerializeField] Transform rightPos;
    [SerializeField] Transform leftPos;
    Tween left;
    Tween right;

    void Start()
    {
        _anim = GetComponent<Animator>();
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
            left = LeftHand.transform.DOMove(_selectObject.transform.position, 0.1f);
        }
        else
        {
            LeftHand.transform.localPosition = leftPos.localPosition;
        }
        if (_righthand)
        {
            //RightHand.transform.position = _selectObject.transform.position;
            right = RightHand.transform.DOMove(_selectObject.transform.position, 0.1f);
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
            _selectObject.transform.DOMove(_target.transform.position, 0.5f).OnComplete(() =>
            {
                int rndTarget = Random.Range(0, GameManager.Instance.CurrentLevel.Targets.Count);
                _target2 = GameManager.Instance.CurrentLevel.Targets[rndTarget];
                _lefthand = false;
                left.Kill();
                LeftHand.transform.DOLocalMove(leftPos.localPosition, 0.5f);
                _righthand = true;
                _selectObject.transform.DOMove(_target2.transform.position, 0.5f).OnComplete(() =>
                {
                    _righthand = false;
                    right.Kill();
                    //RightHand.transform.localPosition = rightPos.localPosition;
                    RightHand.transform.DOLocalMove(rightPos.localPosition, 0.5f);
                    _selectObject.GetComponent<Product>().Move = true;
                    if (GameManager.Instance.CurrentLevel.ProductManager.Products.Count > 0)
                    {
                        ObjectSelect();
                    }
                    else
                    {
                        ResetHandPos();
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
