using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public int GetDistance(Node a, Node b)
    {
        int x = Mathf.Abs(a.Col - b.Col);
        int y = Mathf.Abs(a.Row - b.Row);

        return 14 * Mathf.Min(x, y) + 10 * Mathf.Abs(x - y);
    }
}
