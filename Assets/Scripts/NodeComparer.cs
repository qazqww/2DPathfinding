using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeComparer : IEqualityComparer<Node>
{
    public bool Equals(Node x, Node y)
    {
        if (x == null && y == null)
            return true;
        else if (x == null || y == null)
            return false;
        else if (x.FCost == y.FCost && x.HCost == y.HCost)
            return true;

        return false;
    }

    // 대소구분으로 정렬을 위한 함수
    public int GetHashCode(Node obj)
    {
        return obj.FCost;
    }
}
