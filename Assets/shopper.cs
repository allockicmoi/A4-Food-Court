using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopper : MonoBehaviour 
{
    public DateTime last_update = DateTime.Now;
    public System.Random rand = new System.Random();
    public SteeringForcesNavigator nav = new SteeringForcesNavigator();
    private int seat;
    private GameObject shop_to_visit;
 
    public Vector3 shop_center;
   
    public GameObject shops;
    int status = -1;
    public EatingArea eating_area;
    DateTime entered_shop;
    DateTime sat_down;
    int moving = 1;
    public bool flyered = false;
    DateTime flyered_time = new DateTime();
    int flyered_seconds=2;
    public Vector3 direction = new Vector3();
    public Vector3 destination = new Vector3();
  
    

    // Start is called before the first frame update
    void Start()
    {
        if (rand.Next(10) > 5)
        {
        SetRandomSpawnandDest();
        }
        else
        {
            StartShopping();
        }
       
        last_update = DateTime.Now;
        name = transform.name;

    }

    private void StartShopping()
    {
        status = 0;
        int z_pos = rand.Next(81) - 40;
        transform.position = new Vector3(-150, 1, z_pos);
        GameObject section_to_visit = shops.transform.GetChild(rand.Next(2)).gameObject;
        shop_to_visit = section_to_visit.transform.GetChild(rand.Next(8)).gameObject;
        destination = shop_to_visit.transform.GetChild(4).position;
        shop_center = shop_to_visit.transform.GetChild(3).position;
        
    }

    private void SetRandomSpawnandDest()
    {
        int z_pos = rand.Next(81) - 40;
        int z_dest = rand.Next(81) - 40;
        transform.position = new Vector3(-150, 1, z_pos);
        Debug.Log(transform.position);
        destination = new Vector3(200, 1, z_dest);
        Debug.Log(destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 195)
            {
                Destroy(this.gameObject);
            }
        if (!flyered)
        {
            int isbyflyer = Isbyflyer();


            if (isbyflyer != -1)
            {
                deleteflyer(isbyflyer);
                setFlyered();
            }
            else if (status == 0 && isInRange())
            {
                EnterShop();
            }
            else if (status == 1 && isInRange())
            {
                entered_shop = DateTime.Now;
                moving = 0;
                status += 1;
            }
            else if (status == 2 && (DateTime.Now - entered_shop).TotalSeconds > 1)
            {
                destination = shop_to_visit.transform.GetChild(4).position;
                status += 1;
                moving = 1;
            }
            else if (status == 3 && isInRange())
            {
                FindSeat();
            }
            else if (status == 4 && isAtSeat())
            {
                moving = 0;
                sat_down = DateTime.Now;
                status += 1;
            }
            else if (status == 5 && (DateTime.Now - sat_down).TotalSeconds > 2)
            {
                goDespawn();
            }
            else if (moving == 1)
            {
                transform.position += nav.ComputeDisplacement(this) / 2;
                //  Debug.Log(nav.ComputeDisplacement(transform.position, destination,transform.name));
                last_update = DateTime.Now;
            }
        }
        else
        {
            if ((DateTime.Now - flyered_time).TotalSeconds > flyered_seconds)
            {
                unflyer();
            }
        }
       
    }

    private void unflyer()
    {
        transform.GetComponent<Renderer>().material.color = Color.red;
        flyered = false;
        

    }

    private void setFlyered()
    {
        transform.GetComponent<Renderer>().material.color = Color.magenta;
        flyered = true;
        flyered_time = DateTime.Now;

    }

    private int Isbyflyer()
    {
        Transform parent = transform.parent;
        shoppers shoppers = parent.GetComponent<shoppers>();
        GameObject flyers = shoppers.flyers;
        for(int i = 0; i < flyers.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, flyers.transform.GetChild(i).position) < 3)
            {
                return i;
            }
        }
        return -1;
    }

    private void deleteflyer(int isbyflyer)
    {
        Destroy(transform.parent.GetComponent<shoppers>().flyers.transform.GetChild(isbyflyer).gameObject);
        
    }

    private void goDespawn()
    {
        eating_area.seats_available[seat] = 0;
        moving = 1;
        int z_dest = rand.Next(81) - 40;
        destination = new Vector3(200, 1, z_dest);
        status += 1;
    }

    private void EnterShop()
    {
        destination = shop_center;
        status += 1;
    }

    private void FindSeat()
    {
        int seat_index = rand.Next(eating_area.seats.Count);
        //Debug.Log(seat_index + "   hjhhjhj");
       
        if (eating_area.seats_available[seat_index] == 0)
        {
            moving = 1;
            eating_area.seats_available[seat_index] = 1;
            seat = seat_index;
            destination = eating_area.seats[seat_index].transform.position;
            status +=1;
        }
    }

    private bool isInRange()
    {
        if (Vector3.Magnitude(destination - transform.position)<5)
        {
            return true;
        }
        return false;
    }
    private bool isAtSeat()
    {
        if (Vector3.Magnitude(destination - transform.position) < 1.4)
        {
            return true;
        }
        return false;
    }
}
