using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class advertizer : MonoBehaviour
{
    public int flyer_timer = 5;
    public double flyer_prob = 0.5;
    private int state = 0;
    DateTime last_flyer = new DateTime();
    System.Random rand = new System.Random();
    public Vector3 direction = new Vector3();
    public Vector3 destination = new Vector3();
    public SteeringForcesNavigator nav = new SteeringForcesNavigator();
    public GameObject flyers;
    public GameObject flyer_prefab;
   
    public int shopper_sphere_radius=40;
    shopper target;
    private DateTime timer;
    public int chase_time = 5;
    public int pause_time = 1;
    public DateTime pause_start;
    public int score = 0;
    private float target_range;

    // Start is called before the first frame update
    void Start()
    {
       setRandomDest();
    }

  

    // Update is called once per frame
    void Update()
    {
        set_obs_distance();
        if(state == 0) {
            shopper shopper = FindFlyered();
            if (shopper != null)
            {
                ChaseShopper(shopper);
            }
        else if (isInRange())
        {
            setRandomDest();
        }
        else
        {
        transform.position+=nav.ComputeDisplacement(this) / (float)1.5;
        }
        if ((transform.position.z > 40 || transform.position.z < -40) && (DateTime.Now - last_flyer).TotalSeconds > 100/flyer_timer  ){
            if(rand.Next(10) / 10.0 < flyer_prob) { 
            GameObject flyer = Instantiate(flyer_prefab, flyers.transform);
            flyer.transform.position = transform.position;}
            last_flyer = DateTime.Now;
        }
        }
        else if( state ==1)
        {
            destination = target.transform.position;
            transform.position += nav.ComputeDisplacement(this)/(float)1.5;
            if (isInTargetRange())
            {
                state++;
                timer = DateTime.Now;
            }

        }
        else if(state == 2)
        {
            if (isInTargetRange())
            {
                destination = target.transform.position;
                transform.position += nav.ComputeDisplacement(this) /(float) 1.2;
                if((DateTime.Now-timer).TotalSeconds> chase_time)
                {
                    StartPause();
                }
            }
            else
            {state = 0;
                
            }
        }
        else if(state == 3)
        {
            if ((DateTime.Now - pause_start).TotalSeconds > pause_time)
            {
                ResetAdv();
            }
        }
        
    }

    private void StartPause()
    {
        state++;
        score++;
        pause_start = DateTime.Now;
        
        transform.GetComponent<Renderer>().material.color = Color.green;
    }

    private void ResetAdv()
    {
        if(score == 3)
        {
            Destroy(gameObject);
        }
        else
        {   
            if(score == 1)
                transform.GetComponent<Renderer>().material.color = Color.black;
            if(score == 2)
                transform.GetComponent<Renderer>().material.color = Color.yellow;
            state = 0;
            last_flyer = DateTime.Now;
        }
    }

    private bool isInTargetRange()
    {
        if (Vector3.Distance(transform.position, destination) < target_range)
        {
            return true;
        }
        return false;
    }

    private void ChaseShopper(shopper shopper)
    {
        target = shopper;
        state += 1;
    }

    private shopper FindFlyered()
    {
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(transform.position, shopper_sphere_radius);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].name == "Shopper(Clone)")
            {
                shopper shopper = hitColliders[i].gameObject.GetComponent<shopper>();
                if (shopper.flyered)
                {
                    return shopper;

                }
            }
        }
        return null;
    }

    private bool isInRange()
    {
        if (Vector3.Distance(transform.position, destination) < 3)
        {
            return true;
        }
        return false;
    }

    private void setRandomDest()
    {
        rand = new System.Random();
        int side = rand.Next(2);
        int x = rand.Next(250) - 125;
        if (side == 1)
        {
            destination =  new Vector3(x, 1, 60);
        }
        else
        {
            destination = new Vector3(x, 1, -70);
        }
    }
    public void set_obs_distance()
    {
        GameObject parent = transform.parent.gameObject;
        shopper_sphere_radius = parent.GetComponent<advertizers>().obs_d;
        target_range = parent.GetComponent<advertizers>().sales_d;
        flyer_timer = (int)parent.GetComponent<advertizers>().flyer_timer;
        flyer_prob = (int)parent.GetComponent<advertizers>().flyer_prob;
    }
    
}
