using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid2D grid2D;
    List<Node> closedList = new List<Node>();
    HashSet<Node> openList = new HashSet<Node>();

    private void Awake()
    {
        grid2D = FindObjectOfType<Grid2D>();
        openList = new HashSet<Node>(new NodeComparer());
    }

    public int GetDistance(Node a, Node b)
    {
        int x = Mathf.Abs(a.Col - b.Col);
        int y = Mathf.Abs(a.Row - b.Row);

        return 14 * Mathf.Min(x, y) + 10 * Mathf.Abs(x - y);
    }

    public void FindPath(Vector3 player, Vector3 target)
    {
        Node playerNode = grid2D.FindNode(player);
        Node targetNode = grid2D.FindNode(target);
        Node currentNode = playerNode;

        playerNode.SetGCost(0);
        playerNode.SetHCost(GetDistance(playerNode, targetNode));

        do
        {
            Node[] neighbours = grid2D.Neighbours(currentNode);

            // 이웃 노드 순회
            for (int i=0; i<neighbours.Length; i++)
            {
                if (closedList.Contains(neighbours[i]))     // 이미 처리한 노드 생략
                    continue;
                if (neighbours[i].nType == NodeType.Wall)   // 벽인 노드 생략
                    continue;

                // ※ G Cost : 시작 위치에서 현재 위치까지

                // 현재 노드에서 이웃 노드까지의 거리
                neighbours[i].SetGCost(GetDistance(neighbours[i], playerNode));
                // 현재 노드까지의 거리 + 현재 노드에서 이웃 노드까지의 거리
                int gCost = currentNode.GCost + GetDistance(neighbours[i], currentNode);

                // 오픈 노드 리스트에 없거나, 이웃 노드의 gCost 값이 현재 gCost보다 크다면 갱신 처리
                if (!openList.Contains(neighbours[i]) || gCost < neighbours[i].GCost)
                {
                    // ※ H Cost : 현재 위치(이웃노드)에서 목표점까지
                    int hCost = GetDistance(neighbours[i], targetNode);
                    neighbours[i].SetGCost(gCost);
                    neighbours[i].SetHCost(hCost);
                    neighbours[i].SetParent(currentNode);

                    openList.Add(neighbours[i]);
                }
            }

            if (openList.Contains(currentNode))
                openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
                break;

        } while (openList.Count > 0);
    }
}
