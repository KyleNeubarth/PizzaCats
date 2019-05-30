using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public List<GameObject> segments;
    GameObject segmentsObj;

    public Vector2 position;

    public Vector2 dir = Vector2.right;
    public float catSpeed;

    List<Vector2> breadcrumbs = new List<Vector2>();

    int spriteRenderLayer = 0;

    int segmentsQueued = 0;

    void Start() {
        segmentsObj = GameObject.Find("Segments");
        position = transform.position;
    }
    void Update() {
        breadcrumbs.Insert(0,transform.position);
        Vector2 offset = new Vector2(dir.y, dir.x) *Mathf.Cos(Time.frameCount/10f);
        Debug.Log(offset);
        //position = (Vector3)position + (Vector3)dir * catSpeed;
        transform.position =   (Vector3)offset;
        if (Input.GetKeyDown(KeyCode.Space)) {
            AddSegment();
        }
        MoveSegments();
    }
    void MoveSegments() {
        for (int i= 0;i<segments.Count;i++) {
            Segment s = segments[i].GetComponent<Segment>();
            s.transform.position = breadcrumbs[ ((i + 1) * 50) ];
        }
    }
    void AddSegment()
    {
        GameObject g = (GameObject)Instantiate(Resources.Load("Segment"));
        if (segments.Count > 0)
        {
            Vector3 pos = segments[segments.Count - 1].transform.position - .6f * (Vector3)segments[segments.Count - 1].GetComponent<Segment>().dir;
            g.transform.position = pos;
            g.GetComponent<Segment>().dir = segments[segments.Count - 1].transform.position - pos;
            g.GetComponent<Segment>().dir.Normalize();
        }
        else
        {
            Vector2 pos = transform.position - .6f * (Vector3)dir;
            g.transform.position = pos;
            g.GetComponent<Segment>().dir = transform.position - (Vector3)pos;
            g.GetComponent<Segment>().dir.Normalize();
        }
        g.transform.parent = segmentsObj.transform;
        segments.Add(g);
    }
}
