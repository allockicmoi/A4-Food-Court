using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shoppers : MonoBehaviour

{
    public Text rate;
    public GameObject shopper_prefab;
    DateTime lastspawn;
    public EatingArea eating_area;

    public double spawnSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        lastspawn = DateTime.Now;
        rate.text = "X" + (int)spawnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (( DateTime.Now- lastspawn).TotalSeconds > (2 / spawnSpeed))
        {
            GameObject agent = Instantiate(shopper_prefab, transform);
            shopper shopper = agent.GetComponent<shopper>();
            shopper.eating_area = eating_area;
            lastspawn = DateTime.Now;
        }
    }

    public void SetSpeed(float value)
    {
        spawnSpeed = value * 10;
        rate.text = "X" +(int)spawnSpeed;
    }
}
