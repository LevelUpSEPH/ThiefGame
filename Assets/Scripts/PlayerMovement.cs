using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public joypadController js;
    private float inputX, inputZ;
    public Animator anim;
    private Vector3 movementDirection;
    public float rotationDegree;
    public float MovementSpeed;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = js.horizontalInput();
        inputZ = js.verticalInput();
        movementDirection = new Vector3(inputX/4, 0, inputZ/4 );
        movementDirection.Normalize();
        transform.position = Vector3.MoveTowards(transform.position, transform.position + movementDirection, movementDirection.magnitude * MovementSpeed * Time.deltaTime);
        //transform.Translate(movementDirection * Time.deltaTime);
        if (inputX > 0 || inputX < 0)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
        if (movementDirection != Vector3.zero)
        {
            Quaternion rot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection), movementDirection.magnitude * rotationDegree * Time.deltaTime);

            transform.rotation = rot;   
        }

    }
    
}