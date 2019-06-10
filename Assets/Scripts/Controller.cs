using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Node startNode;
    Node endNode;

    Grid2D grid2D;
    
    void Awake()
    {
        grid2D = FindObjectOfType<Grid2D>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            startNode = RayCast();
            //Debug.Log(startNode);

            Node[] nodeArr = grid2D.Neighbours(startNode);
            for (int i = 0; i < nodeArr.Length; i++)
                Debug.Log(nodeArr[i]);
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
