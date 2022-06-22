using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform Target;

    public float FollowSpeed;

    public Vector3 FollowOffset;
    private void Start()
    {
        FollowOffset = new Vector3(transform.position.x - Target.position.x , transform.position.y - Target.position.y , transform.position.z - Target.position.z);
    }

    void Update()
    {
        if (Target == null)
        {
            return;
        }
        
        Vector3 FollowPos = new Vector3(Target.position.x + FollowOffset.x, Target.position.y + FollowOffset.y, Target.position.z + FollowOffset.z);

        transform.position = Vector3.Lerp(transform.position, FollowPos, FollowSpeed * Time.deltaTime);
    }

}
