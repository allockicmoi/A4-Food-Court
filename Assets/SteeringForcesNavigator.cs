using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class SteeringForcesNavigator
    {
        public float speed = (float)1;
        public Vector3 ComputeDisplacement( shopper shopper)
        {
            
            Vector3 normalized_direction = Vector3.Normalize(shopper.destination - shopper.transform.position);
            
            Vector3 sum_forces = new Vector3(0, 0, 0);
            
            sum_forces += shopper.direction;
            sum_forces += SeekBehaviour(shopper);
           
            sum_forces += ObstacleAvoidance(shopper,normalized_direction);
            sum_forces += separation_forces(shopper);

            shopper.direction = sum_forces/3 ;
            if (Vector3.Magnitude(shopper.direction) > 1)
            {
                shopper.direction = Vector3.Normalize(shopper.direction);
            }
            return shopper.direction * speed;
        }

        private Vector3 separation_forces(shopper agent)
        {
            Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(agent.transform.position, 2);
            Vector3 separation_forces = new Vector3(0, 0, 0);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if ((hitColliders[i].name == "Shopper(Clone)"|| hitColliders[i].name =="Advertiser(Clone)" )&& Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position)>0.1)
                {



                   
                        separation_forces += (agent.transform.position - hitColliders[i].transform.position) / (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position)/2 );
                    


                }
            }
            return Vector3.Normalize(separation_forces);
        }

        private static Vector3 ObstacleAvoidance( shopper agent, Vector3 normalized_direction)
        {Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(agent.transform.position, 10);
            Vector3 avoidance_forces = new Vector3(0, 0, 0);
            Vector3 avoidance_vector = new Vector3(0, 0, 0);
            Vector3 normal_vector = new Vector3(0, 0, 0);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].name != "Floor" && hitColliders[i].name != agent.name && hitColliders[i].name != "Shopper(Clone)" && hitColliders[i].name != "Advertizer(Clone)")
                {


                    avoidance_vector = hitColliders[i].transform.position - agent.transform.position;
                    

                    if (Vector3.SignedAngle(hitColliders[i].transform.position - agent.transform.position,agent.direction,Vector3.up)>0)
                    {
                        avoidance_forces += Quaternion.AngleAxis(100, Vector3.up) * normalized_direction/ (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) / 8);
                    }
                    else
                    {
                        avoidance_forces+=Quaternion.AngleAxis(-100, Vector3.up) * normalized_direction/ (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) /8);
                    }

                }
            }

            return Vector3.Normalize(avoidance_forces) ;
        }

        private Vector3 SeekBehaviour(shopper agent)
        {
            Vector3 seek_vector = Vector3.Normalize(agent.destination - agent.transform.position);
            return seek_vector;
        }
        public Vector3 ComputeDisplacement(advertizer shopper)
        {

            Vector3 normalized_direction = Vector3.Normalize(shopper.destination - shopper.transform.position);

            Vector3 sum_forces = new Vector3(0, 0, 0);

            sum_forces += shopper.direction;
            sum_forces += SeekBehaviour(shopper);

            sum_forces += ObstacleAvoidance(shopper);
            sum_forces += separation_forces(shopper);

            shopper.direction = Vector3.Normalize(sum_forces);
            return shopper.direction * speed *(float)0.7;
        }

        private Vector3 separation_forces(advertizer agent)
        {
            Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(agent.transform.position, 3);
            Vector3 separation_forces = new Vector3(0, 0, 0);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].name == "Shopper(Clone)" && Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) > 0.3)
                {



                    
                        separation_forces+= (agent.transform.position - hitColliders[i].transform.position) / (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position));

                    }

            }
            return separation_forces;
        }

        private static Vector3 ObstacleAvoidance(advertizer agent)
        {
            Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(agent.transform.position, 15);
            Vector3 avoidance_forces = new Vector3(0, 0, 0);
            Vector3 avoidance_vector = new Vector3(0, 0, 0);
            Vector3 normal_vector = new Vector3(0, 0, 0);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].name != "Floor" && hitColliders[i].name != agent.name && hitColliders[i].name != "Shopper(Clone)" && hitColliders[i].name != "Advertizer(Clone)")
                {


                    avoidance_vector = hitColliders[i].transform.position - agent.transform.position;


                    if (Vector3.SignedAngle(hitColliders[i].transform.position - agent.transform.position, agent.direction, Vector3.up) > 0)
                    {
                        avoidance_forces += Quaternion.AngleAxis(100, Vector3.up) * agent.direction / (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) / 15);
                    }
                    else
                    {
                        avoidance_forces += Quaternion.AngleAxis(-100, Vector3.up) * agent.direction / (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) / 15);
                    }

                }
            }

            return avoidance_forces;
        }

        private Vector3 SeekBehaviour(advertizer agent)
        {
            Vector3 seek_vector = Vector3.Normalize(agent.destination - agent.transform.position);
            return seek_vector;
        }
    }
}
