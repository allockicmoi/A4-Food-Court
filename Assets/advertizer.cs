using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advertizer : MonoBehaviour
{
    private int state = 0;
    System.Random rand = new System.Random();

    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        setRandomDest();
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 setRandomDest()
    {
        int side = rand.Next(2);
        int x = rand.Next(250) - 125;
        if (side == 1)
        {
            return new Vector3(x, 1, 60);
        }
        else
        {
            return new Vector3(x, 1, -70);
        }
    }
}
