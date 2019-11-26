using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingArea : MonoBehaviour
{
    public GameObject seating_area;
    public GameObject planter;
    System.Random rand = new System.Random();
    public List<GameObject> seats = new List<GameObject>();
    public List<int> seats_available = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        CreateEatingArea();
        CreatePlanters();
    }

    private void CreatePlanters()
    {
        Vector3 position;
        int num_planters = rand.Next(4) + 2;
        for (int i = 0; i < num_planters; i++)
        {
            position = FindNextPlanterPos();
            CreatePlanter(position);
        }

    }

    private void CreatePlanter(Vector3 position)
    {
        GameObject temp = Instantiate(planter, position, Quaternion.identity);
        temp.transform.localScale += new Vector3((rand.Next(10)/7), 1, (rand.Next(10) / 7) + 1);
        temp.transform.parent = transform.GetChild(0);
    }

    private Vector3 FindNextPlanterPos()
    {
        Collider[] hitColliders;
        Vector3 pos = new Vector3(rand.Next(200) - 100, -3, rand.Next(70) - 35);
        hitColliders = Physics.OverlapSphere(pos, 30);
        int max = 0;
        while (hitColliders.Length != 1 && max<100)
        {
            pos = new Vector3(rand.Next(200) - 100, -3, rand.Next(70) - 35);
            hitColliders = Physics.OverlapSphere(pos,30);
            max++;
        }
        return pos;
    }

    private void CreateEatingArea()
    {
        Vector3 position;
        int num_seating_areas = rand.Next(2)+3;
        
        for(int i = 0; i < num_seating_areas; i++)
        {
         position = FindNextSeatingPos();
         CreateSeatingArea(position);
        }
       

    }

    private Vector3 FindNextSeatingPos()
    {
        Collider[] hitColliders;
        Vector3 pos = new Vector3(rand.Next(150) - 75, 1, rand.Next(70) - 35);
        hitColliders = Physics.OverlapSphere(pos,40);
        int max = 0;
        while (hitColliders.Length != 1 && max < 100)
        {
            pos = new Vector3(rand.Next(150) - 75, 1, rand.Next(70) - 35);
            hitColliders = Physics.OverlapSphere(pos, 40);
            max++;
        }
        return pos;
    }

    private void CreateSeatingArea(Vector3 position)
    {
       
        int num_chairs = rand.Next(5) + 4;
        int index = rand.Next(8);
        int chairs_placed = 0;
        GameObject chair;
        GameObject area = Instantiate(seating_area,position, Quaternion.identity);
        GameObject chairs = area.transform.GetChild(1).gameObject;

        area.transform.parent = transform.GetChild(1);
        while (chairs_placed < num_chairs)
        {
            index = rand.Next(8);
            chair = chairs.transform.GetChild(index).gameObject;
            if (!chair.activeSelf)
            {
                seats.Add(chair);
                seats_available.Add(0);
                chair.SetActive(true);
                chairs_placed += 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
