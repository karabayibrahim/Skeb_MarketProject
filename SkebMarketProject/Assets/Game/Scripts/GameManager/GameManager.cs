using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoSingleton<GameManager>
{
    public Level CurrentLevel;
    public Bag Bag;
    public CaseSystem CaseSystem;
    public GameObject Band;
    public UIManager UIManager;
    public PlayerController PlayerController;
    public static Action FailAction;
    public static Action WinAction;

    private float scrollSpeed = 0.1f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        BandMove();
    }

    private void BandMove()
    {
        float offset = 0f;
        offset -= Time.time * scrollSpeed;
        Band.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
