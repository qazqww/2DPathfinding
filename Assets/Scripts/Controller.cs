using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Node startNode;
    Node endNode;

    Grid2D grid2D;
    Pathfinding pathfinding;

    void Awake()
    {
        grid2D = FindObjectOfType<Grid2D>();
        pathfinding = FindObjectOfType<Pathfinding>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startNode = RayCast();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            endNode = RayCast();
            if (startNode != null && endNode != null)
                pathfinding.FindPath(startNode.Pos, endNode.Pos);
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
