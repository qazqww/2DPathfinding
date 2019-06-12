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
            pathfinding.Ready(startNode.Pos, endNode.Pos);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            pathfinding.Step();
        }
    }

    Node RayCast()
    {
        return grid2D.ClickNode();
    }
}
