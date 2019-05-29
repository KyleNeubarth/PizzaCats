using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Sprite selectPizza;
    public Sprite normalPizza;

    Camera cam;
    GameObject ships;
    GameObject selectSphere;

    public List<GameObject> selectedShips = new List<GameObject>();

    bool mousedown = false;

    Vector2 selectZone = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        ships = GameObject.Find("Ships");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        selectSphere = GameObject.Find("SelectSphere");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            selectZone = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousedown = true;
            selectSphere.transform.position = selectZone;
        }
        if (mousedown) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dist = 2*Distance(mousePos,selectZone);
            selectSphere.transform.localScale = new Vector3(dist,dist,0.1f);
            for (int i=0;i<ships.transform.childCount;i++) {
                if ( Distance(ships.transform.GetChild(i).position,selectZone) <= selectSphere.transform.localScale.x/2) {
                    if (!selectedShips.Contains(ships.transform.GetChild(i).gameObject)) {
                        selectedShips.Add(ships.transform.GetChild(i).gameObject);
                        ships.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = selectPizza;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            selectSphere.transform.localScale = new Vector3(0,0,0.1f);
            mousedown = false;
        }
    }
    float Distance(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
    }
}
