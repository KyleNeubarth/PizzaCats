using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship2 : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject ships;
    public float neighborhood = 3;

    public string behaviorState = "";

    public Vector2 velocity;

    Vector2 pathTo;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ships = GameObject.Find("Ships");
        //rb.velocity = new Vector2(shipSpeed,shipSpeed);
    }

    void Update()
    {
        velocity = rb.velocity;
        Vector2 temp = rb.velocity;

        Vector2 cohesion = Vector2.zero;
        for (int i = 0; i < ships.transform.childCount; i++)
        {
            Vector2 pos = ships.transform.GetChild(i).position;
            if (Distance(transform.position,pos) < neighborhood) {
                Vector2 dir = (pos -(Vector2)transform.position);
                dir.Normalize();
                float x = dir.magnitude;
                cohesion +=  dir * .1f * Mathf.Pow(x-neighborhood/4,3);
                Debug.Log(cohesion);
            }
        }
        temp += cohesion;

        rb.velocity = temp*.95f;
    }
    float Distance(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
    }
}
