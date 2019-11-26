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
           
            sum_forces += ObstacleAvoidance(shopper);
            sum_forces += separation_forces(shopper);

            shopper.direction = Vector3.Normalize(sum_forces) ;
            return shopper.direction * speed;
        }

        private Vector3 separation_forces(shopper agent)
        {
            Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(agent.transform.position, 3);
            Vector3 separation_forces = new Vector3(0, 0, 0);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].name == "Shopper(Clone)" && Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position)>0.3)
                {



                    if (Vector3.SignedAngle(hitColliders[i].transform.position - agent.transform.position, agent.direction, Vector3.up) > 0)
                    {
                        separation_forces += Quaternion.AngleAxis(100, Vector3.up) * agent.direction / (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) / 6);
                    }
                    else
                    {
                        separation_forces += Quaternion.AngleAxis(-100, Vector3.up) * agent.direction / (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) /6);
                    }


                }
            }
            return separation_forces;
        }

        private static Vector3 ObstacleAvoidance( shopper agent)
        {Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(agent.transform.position, 20);
            Vector3 avoidance_forces = new Vector3(0, 0, 0);
            Vector3 avoidance_vector = new Vector3(0, 0, 0);
            Vector3 normal_vector = new Vector3(0, 0, 0);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].name != "Floor" && hitColliders[i].name != agent.name && hitColliders[i].name != "Shopper(Clone)")
                {


                    avoidance_vector = hitColliders[i].transform.position - agent.transform.position;
                    

                    if (Vector3.SignedAngle(hitColliders[i].transform.position - agent.transform.position,agent.direction,Vector3.up)>0)
                    {
                        avoidance_forces += Quaternion.AngleAxis(100, Vector3.up) * agent.direction/ (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) / 13);
                    }
                    else
                    {
                        avoidance_forces+=Quaternion.AngleAxis(-100, Vector3.up) * agent.direction/ (float)(Vector3.Magnitude(agent.transform.position - hitColliders[i].transform.position) / 13);
                    }

                }
            }

            return avoidance_forces;
        }

        private Vector3 SeekBehaviour(shopper agent)
        {
            Vector3 seek_vector = Vector3.Normalize(agent.destination - agent.transform.position);
            return seek_vector;
        }
    }
}
