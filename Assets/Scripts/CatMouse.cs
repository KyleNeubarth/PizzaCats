using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMouse : MonoBehaviour
{

    Cat cat;

    // Start is called before the first frame update
    void Start()
    {
        cat = GameObject.Find("Cat").GetComponent<Cat>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cat.dir = mousePos - (Vector2)cat.transform.position;
        cat.dir.Normalize();
    }
}
