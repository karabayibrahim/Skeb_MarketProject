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
    private bool _lefthand = false;
    private bool _righthand = false;
    void Start()
    {
        _anim = GetComponent<Animator>();
        ObjectSelect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_lefthand)
        {
            _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
            _anim.SetIKPosition(AvatarIKGoal.LeftHand, _selectObject.transform.position);
        }
        if (_righthand)
        {
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, _selectObject.transform.position);
        }

    }
    private void ObjectSelect()
    {
        if (GameManager.Instance.CurrentLevel.ProductManager.Products.Count>0)
        {
            _anim.CrossFade("Take", 0.01f);
        }
        _selectObject = GameManager.Instance.CurrentLevel.ProductManager.Products[0].gameObject;
        GameManager.Instance.CurrentLevel.ProductManager.Products.Remove(GameManager.Instance.CurrentLevel.ProductManager.Products[0]);
        _lefthand = true;
        _selectObject.transform.DOMove(_target.transform.position, 0.5f).OnComplete(() =>
        {
            int rndTarget = Random.Range(0, GameManager.Instance.CurrentLevel.Targets.Count);
            _target2 = GameManager.Instance.CurrentLevel.Targets[rndTarget];
            _lefthand = false;
            _righthand = true;
            _selectObject.transform.DOMove(_target2.transform.position, 0.5f).OnComplete(() =>
            {
                _righthand = false;
                _selectObject.GetComponent<Product>().Move = true;
                ObjectSelect();
            });
        });
        _anim.CrossFade("Take", 0.01f);
    }


}
