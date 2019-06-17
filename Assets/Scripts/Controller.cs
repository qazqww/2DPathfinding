using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Node startNode;
    Node endNode;
    Player player;
    Target target;

    Grid2D grid2D;
    Pathfinding pathfinding;

    void Awake()
    {
        grid2D = FindObjectOfType<Grid2D>();
        pathfinding = FindObjectOfType<Pathfinding>();
        player = FindObjectOfType<Player>();
        target = FindObjectOfType<Target>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startNode = RayCast();
            player.transform.position = RayCast().Pos;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            endNode = RayCast();
            target.transform.position = RayCast().Pos;
            pathfinding.FindPathCoroutine(player, target);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            pathfinding.Step();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            pathfinding.ResetNode();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Node node = RayCast();
            if (node != null)
                node.SetNodeType(NodeType.Wall);
        }
    }

    Node RayCast()
    {
        return grid2D.ClickNode();
    }
}
