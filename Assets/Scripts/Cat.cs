using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public List<GameObject> segments;

    public Vector2 dir = Vector2.right;
    public float catSpeed;

    void Start() {

    }
    void Update() {
        transform.position += (Vector3)dir * catSpeed;
    }
    void AddSegment()
    {
        GameObject g = (GameObject)Instantiate(Resources.Load("Segment"));
        if (segments.Count > 1) {
            g.transform.position = segments[segments.Count].transform.position + Vector3.left;
        }
    }
}
