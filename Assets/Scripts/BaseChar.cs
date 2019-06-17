using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChar : MonoBehaviour
{
    private List<Node> path = new List<Node>();
    
    public void SetPath(List<Node> path)
    {
        if (path == null)
            return;

        this.path.Clear();
        foreach (Node p in path)
        {
            this.path.Add(p);
        }
    }

    private void Update()
    {
        if (path.Count > 0)
        {
            Vector3 dir = path[0].transform.position - transform.position;
            dir.Normalize();
            transform.Translate(dir * 10);

            float distance = Vector3.Distance(path[0].transform.position, transform.position);
            if (distance < 5f)
            {
                Debug.Log(distance);
                path.RemoveAt(0);
            }
        }
    }
}
