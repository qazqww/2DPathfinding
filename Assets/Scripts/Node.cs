using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
}
