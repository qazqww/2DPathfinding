using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Node startNode;
    Node endNode;    
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startNode = RayCast();
            Debug.Log(startNode);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            endNode = RayCast();
            Debug.Log(endNode);
        }
    }

    Node RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, 100);
        if (hit2D.transform != null)
            return hit2D.transform.GetComponent<Node>();

        return null;
    }
}
