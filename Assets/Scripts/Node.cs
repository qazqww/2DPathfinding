using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    Collider2D collider;

    float gCost;
    float hCost;
    float fCost;
    public float GCost
    {
        get { return gCost; }
    }
    public float HCost
    {
        get { return hCost; }
    }
    public float FCost
    {
        get { return gCost + hCost; }
    }

    int row;
    int col;
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

    public void SetGCost(float cost)
    {
        gCost = cost;
    }

    public void SetHCost(float cost)
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

    public bool Contains(Vector3 pos)
    {
        return collider.bounds.Contains(pos);
    }
}
