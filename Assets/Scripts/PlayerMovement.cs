using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class PlayerMovement : MonoBehaviour
{
    public FloatingJoystick variableJoystick;
    private float inputX, inputZ;
    public Animator anim;
    private Vector3 movementDirection;
    public float rotationDegree;
    public float MovementSpeed=1;
    private Inventory inventory;
    public EnemyScript enemy;
    private NavMeshAgent playerNav;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        playerNav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {


        //transform.Translate(movementDirection * Time.deltaTime);

        if (movementDirection.magnitude > 0)
        {
            if (!enemy.calledCops)
                anim.SetBool("Walking", true);
            if (enemy.calledCops)
                anim.SetBool("Running", true);
        }
        

        if (Input.GetMouseButton(0))
        {
            
            movementDirection = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

            if (movementDirection.magnitude > 0)
            {

                playerNav.Move(movementDirection * Time.deltaTime * MovementSpeed);
                playerNav.SetDestination(transform.position + movementDirection);
                Quaternion rot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection), movementDirection.magnitude * rotationDegree * Time.deltaTime);

                transform.rotation = rot;

                
            }
            
            
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }

    }
    
}