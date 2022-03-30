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
    public Casier Casier;
    public GameStatus GameStatus;
    public static Action FailAction;
    public static Action WinAction;
    public LevelData LevelData;
    [SerializeField] private int _levelIndex;

    private float scrollSpeed = 0.1f;
    void Awake()
    {
        CreateLevel();
    }

    public int LevelIndex
    {
        get
        {
            return _levelIndex;
        }
        set
        {
            if (LevelIndex==value)
            {
                return;
            }
            _levelIndex = value;
            CreateLevel();
        }
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

    private void CreateLevel()
    {
        //Instantiate(LevelData.Levels[LevelIndex]);
        Bag = FindObjectOfType<Bag>();
        CurrentLevel = FindObjectOfType<Level>();
    }
}
