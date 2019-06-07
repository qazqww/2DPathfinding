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
}
