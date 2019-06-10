using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    Collider2D collider;

    NodeType nodeType = NodeType.None;
    public NodeType nType
    {
        get { return nodeType; }
    }

    int hCost = 0;
    int fCost = 0;
    int gCost = 0;
    public int GCost
    {
        get { return gCost; }
    }
    public int HCost
    {
        get { return hCost; }
    }
    public int FCost
    {
        get { return gCost + hCost; }
    }

    int row = 0;
    int col = 0;
    public int Row
    {
        get { return row; }
    }
    public int Col
    {
        get { return col; }
    }

    Node parent;
    public Node Parent
    {
        get { return parent; }
    }

    public Vector3 Pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }

    public void SetGCost(int cost)
    {
        gCost = cost;
    }

    public void SetHCost(int cost)
    {
        hCost = cost;
    }

    public void SetNode(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetParent(Node parent)
    {
        this.parent = parent;
    }

    public void SetNodeType(NodeType nodeType)
    {
        this.nodeType = nodeType;
    }

    public bool Contains(Vector3 pos)
    {
        return collider.bounds.Contains(pos);
    }    
}
