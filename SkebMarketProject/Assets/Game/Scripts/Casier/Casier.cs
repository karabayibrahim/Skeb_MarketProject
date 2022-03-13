using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Casier : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _obje;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _target2;
    private bool _lefthand = true;
    private bool _righthand = false;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _obje.transform.DOMove(_target.transform.position, 0.5f).OnComplete(() =>
        {
            _lefthand = false;
            _righthand = true;
            _obje.transform.DOMove(_target2.transform.position, 0.5f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_lefthand)
        {
            _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            _anim.SetIKPosition(AvatarIKGoal.LeftHand, _obje.transform.position);
        }
        if (_righthand)
        {
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, _obje.transform.position);
        }
        
    }
}
