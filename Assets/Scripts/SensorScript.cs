using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SensorScript : MonoBehaviour
{
    public float height = 1.0f;
    public float distance = 10;
    public float angle = 30;
    public int scanFrequency;
    public LayerMask layers;
    public LayerMask obstructionLayer;
    Collider[] colliders = new Collider[50];
    Mesh mesh;
    public int count;
    float scanInterval;
    float scanCounter;
    public List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        scanInterval = 1f / scanFrequency;
        scanCounter = scanInterval;
    }

    // Update is called once per frame
    void Update()
    {
        scanCounter -= Time.deltaTime;
        if(scanCounter < 0)
        {
            scanCounter = scanInterval;
            Scan();
        }
    }
    Mesh createMesh() // draw cone
    {
        Mesh mesh = new Mesh();
        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];
        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
        Vector3 topCenter = Vector3.zero + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;
        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomRight;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        
        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {

            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;
            // far side

            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;


            // upper side

            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // below side

            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;

            currentAngle += deltaAngle;
        }
       
        for (int i = 0; i < numVertices; i++)
            triangles[i] = i;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


        return mesh;
    }
    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; i++)
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        Gizmos.color = Color.green;
        foreach (var obj in objects)
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
    }
    private void OnValidate()
    {
        mesh = createMesh();
    }
    public bool isInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if (direction.y < -0.5f || direction.y > height)
            return false;
        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction,transform.forward);
        
        if (deltaAngle > angle)
            return false;
        origin.y = height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, obstructionLayer))
            return false;
        return true;
    }
    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);
        objects.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (isInSight(obj))
            {
                objects.Add(obj);
            }
        }
    }
}