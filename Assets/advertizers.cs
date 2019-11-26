using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advertizers : MonoBehaviour
{
    public GameObject adv_prefab;
    public int num_adv = 1;
    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        CreateAdvs();
    }

    private void CreateAdvs()
    {
        GameObject agent;
        for (int i = 0; i < num_adv; i++)
        {
            agent = Instantiate(adv_prefab, transform);
            agent.transform.position = FindPosition();
        }
    }

    private Vector3 FindPosition()
    {
        int side = rand.Next(2);
        int x = rand.Next(250) - 125;
        if(side == 1)
        {
            return new Vector3(x, 1, 60);
        }
        else
        {
            return new Vector3(x, 1, -70);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
