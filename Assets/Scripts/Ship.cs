using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float shipSpeed = .01f;
    public float neighborhood = 3;
    public Rigidbody2D rb;
    public GameObject ships;

    float alignmentWeight = .1f;
    float cohesionWeight = .1f;
    float seperationWeight = .1f;
    float mousePullWeight = .3f;

    Camera camera;

    Vector2 vel = Vector2.zero;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        ships = GameObject.Find("Ships");
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //rb.velocity = new Vector2(shipSpeed,shipSpeed);
    }

    public void Update()
    {
        vel = rb.velocity;

        Vector2 alignment = ComputeAlignment();
        Vector2 cohesion = ComputeCohesion();
        Vector2 seperation = ComputeSeperation();
        Vector2 mousePull = MousePull();

        rb.velocity += mousePullWeight * mousePull;
        float mag = rb.velocity.magnitude;
        rb.velocity += alignmentWeight * alignment + cohesionWeight*cohesion + seperationWeight*seperation;
        //Debug.Log(alignmentWeight * alignment);
        Vector2 a = rb.velocity;
        //rb.angularVelocity = ((a.x > 0) ? 1:-1)*Vector3.Angle(Vector3.down, a);

            a.Normalize();
            rb.velocity = a;
            //Debug.Log("after: " + rb.velocity +" whaaa "+ a.x +", "+ a.y);
            rb.velocity *= mag;
        
        //Debug.Log(a +", "+Vector3.Angle(Vector3.right, a));
    }
    public Vector2 ComputeAlignment()
    {
        Vector2 p = Vector2.zero;
        int neighborCount = 0;

        for (int i=0;i<ships.transform.childCount;i++) {
            GameObject newShip = ships.transform.GetChild(i).gameObject;

            if (newShip == gameObject) {
                continue;
            }

            if ( Distance(newShip.transform.position,transform.position) < neighborhood ) {
                p += (Vector2)newShip.GetComponent<Ship>().vel;
                //Debug.Log(p);
                neighborCount++;
            }
        }

        if (neighborCount == 0)
            return p;
        p.x /= neighborCount;
        p.y /= neighborCount;
        p.Normalize();
        return p;
    }
    Vector2 ComputeCohesion() {
        Vector2 p = Vector2.zero;
        int neighborCount = 0;

        for (int i = 0; i < ships.transform.childCount; i++)
        {
            GameObject newShip = ships.transform.GetChild(i).gameObject;

            if (newShip == gameObject)
            {
                continue;
            }
            float dist = Distance(newShip.transform.position, transform.position);
            if (dist < neighborhood && dist > 1)
            {
                p += (Vector2)newShip.transform.position;
                neighborCount++;
            }
        }

        if (neighborCount == 0)
            return p;
        p.x /= neighborCount;
        p.y /= neighborCount;
        p -= (Vector2)transform.position;
        //p.Normalize();
        return p;
    }
    Vector2 ComputeSeperation()
    {
        Vector2 p = Vector2.zero;
        int neighborCount = 0;

        for (int i = 0; i < ships.transform.childCount; i++)
        {
            GameObject newShip = ships.transform.GetChild(i).gameObject;

            if (newShip == gameObject)
            {
                continue;
            }

            if (Distance(newShip.transform.position, transform.position) < neighborhood)
            {
                p -= (Vector2)newShip.transform.position - (Vector2)transform.position;
                neighborCount++;
            }
        }

        if (neighborCount == 0)
            return p;
        p.x /= neighborCount;
        p.y /= neighborCount;
        //p.Normalize();
        return p;
    }
    Vector2 MousePull()
    {
        if (Input.GetMouseButton(0))
        {
            return Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }
        else {
            return Vector2.zero;
        }
    }
    float Distance(Vector2 a,Vector2 b)
    {
        return Mathf.Sqrt( Mathf.Pow(a.x-b.x,2)+ Mathf.Pow(a.y - b.y, 2));
    }
}
