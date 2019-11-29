using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class advertizers : MonoBehaviour
{
    public GameObject adv_prefab;
    public int num_adv = 1;
    System.Random rand = new System.Random();
    public GameObject flyers;
    public Text radius_display;
    public int obs_d=40;
    public int sales_d = 20;
    public Text sales_d_display;
    public double flyer_timer;
    public Text adv_rate;
    public float flyer_prob;
    public Text disp_flyer_prob;
    public  Text advs_disp;

    // Start is called before the first frame update
    void Start()
    {
        radius_display.text = "" + obs_d;
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
        if (transform.childCount < num_adv)
        {
            GameObject agent = Instantiate(adv_prefab, transform);
            agent.transform.position = FindPosition();
            advertizer adv = agent.GetComponent<advertizer>();
            adv.flyers = flyers;
            
        }
    }

    public void setRadius(float value)
    {
        obs_d = (int)(value * 100);
        radius_display.text = "" + obs_d;
    }
    public void setSalesD (float value)
    {
        sales_d = (int)(value * 100);
        sales_d_display.text = "" + sales_d;
    }
    public void Adv_rate(float value)
    {
        flyer_timer = value * 100 +1;
        adv_rate.text = "" + flyer_timer;
    }
    public void Flyer_prob(float value)
    {
        flyer_prob = value ;
        disp_flyer_prob.text = "" + (int)(flyer_prob*100)+ "%";
    }
    public void Num_adv(float value)
    {
        num_adv = (int)(value*10);
        advs_disp.text = "" + num_adv;
    }


}
