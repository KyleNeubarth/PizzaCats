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
        Vector2 mousePos = Input.mousePosition;
        cat.dir = mousePos - new Vector2(Screen.width / 2, Screen.height / 2);
        //cat.dir = mousePos - (Vector2)Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        cat.dir.Normalize();
    }
}
