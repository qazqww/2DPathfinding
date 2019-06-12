using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NodeType
{
    None,
    Wall,
}

public class Node : MonoBehaviour
{
    Collider2D collider;
    
    Text gText;
    Text hText;
    Text fText;
    Image image;

    NodeType nodeType = NodeType.None;
    public NodeType nType
    {
        get { return nodeType; }
    }

    int gCost = 0;
    int hCost = 0;
    int fCost = 0;    
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
        gText = transform.Find("GCost").GetComponent<Text>();
        hText = transform.Find("HCost").GetComponent<Text>();
        fText = transform.Find("FCost").GetComponent<Text>();
        image = GetComponent<Image>();
        collider = GetComponent<Collider2D>();
    }

    public void SetGCost(int cost)
    {
        gCost = cost;
        gText.text = "G: " + cost;        
    }

    public void SetHCost(int cost)
    {
        hCost = cost;
        hText.text = "H: " + cost;        
        fText.text = "F: " + (hCost + gCost);
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

    public void SetColor(Color color)
    {
        if (image != null)
            image.color = color;
    }

    public bool Contains(Vector3 pos)
    {
        return collider.bounds.Contains(pos);
    }    
}
