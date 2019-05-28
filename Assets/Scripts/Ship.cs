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
    float cohesionWeight = 0f;
    float seperationWeight = 0f;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        ships = GameObject.Find("Ships");
        //rb.velocity = new Vector2(shipSpeed,shipSpeed);
    }

    public void Update()
    {
        Vector2 alignment = ComputeAlignment();
        Vector2 cohesion = ComputeCohesion();
        Vector2 seperation = ComputeSeperation();

        rb.velocity +=  alignmentWeight*alignment + cohesionWeight*cohesion + seperationWeight*seperation;
        Debug.Log(alignmentWeight * alignment + cohesionWeight * cohesion + seperationWeight * seperation);
        //Vector2 a = rb.velocity;
        //a.Normalize();
        //rb.velocity = a;
        //Debug.Log("after: " + rb.velocity +" whaaa "+ a.x +", "+ a.y);
        //rb.velocity *= shipSpeed;
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
                p += (Vector2)newShip.GetComponent<Rigidbody2D>().velocity;
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

            if (Distance(newShip.transform.position, transform.position) < neighborhood)
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
        p.Normalize();
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
        p.Normalize();
        return p;
    }
    float Distance(Vector2 a,Vector2 b)
    {
        return Mathf.Sqrt( Mathf.Pow(a.x-b.x,2)+ Mathf.Pow(a.y - b.y, 2));
    }
}
