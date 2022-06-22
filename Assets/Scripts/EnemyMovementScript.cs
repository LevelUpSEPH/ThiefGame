using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovementScript : MonoBehaviour
{

    public List<Transform> toPos;
    private int counter;
    private NavMeshAgent navmeshAgent;
    private bool pathSet;
    public bool isWalking=true;
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();

        navmeshAgent.SetDestination(toPos[counter].position);
        pathSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        setPos();
        if (!isWalking)
            Destroy(navmeshAgent);
    }

    private void setPos()
    {
        if (pathSet)
        {
            if (navmeshAgent.pathStatus == NavMeshPathStatus.PathComplete && navmeshAgent.remainingDistance == 0)
            {
                
                pathSet = false;
            }
        }
        else
        {
            counter++;

            if (counter >= toPos.Count)
            {
                counter = 0;
            }

            navmeshAgent.SetDestination(toPos[counter].position);

            pathSet = true;
        }

    }

}
