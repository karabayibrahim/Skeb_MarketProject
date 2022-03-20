using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public bool Move = false;
    public bool Takeable = true;
    public bool InTake = false;
    public bool InBag = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            transform.Translate(Time.deltaTime * -0.5f, 0, 0);
        }
    }
}
