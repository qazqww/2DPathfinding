using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PairNode : IEquatable<PairNode>
{
    public BaseChar player;
    public BaseChar target;

    public bool Equals(PairNode other)
    {
        if (other.player == null || other.target == null)
            return true;
        else if (player == null)
            return true;

        return other.player.Equals(player);
    }
}

public class Pathfinding : MonoBehaviour
{
    Node startNode;
    Node curNode;
    Node prevNode;
    Node targetNode;
    List<Node> pathNode;
    List<Node> curNeighbours = new List<Node>();   

    Grid2D grid2D;
    List<Node> closedList = new List<Node>();
    List<Node> openList = new List<Node>();
    NodeComparer nodeComparer = new NodeComparer();

    bool execute = false;
    List<PairNode> orderlist = new List<PairNode>();

    private void Awake()
    {
        grid2D = FindObjectOfType<Grid2D>();
    }

    public int GetDistance(Node a, Node b)
    {
        int x = Mathf.Abs(a.Col - b.Col);
        int y = Mathf.Abs(a.Row - b.Row);

        return 14 * Mathf.Min(x, y) + 10 * Mathf.Abs(x - y);
    }

    public void Ready(Vector3 player, Vector3 target)
    {
        execute = true;

        startNode = grid2D.FindNode(player);
        targetNode = grid2D.FindNode(target);

        startNode.SetGCost(0);
        startNode.SetHCost(GetDistance(startNode, targetNode));
        startNode.SetParent(null);
        targetNode.SetParent(null);
        curNode = startNode;

        openList.Clear();
        closedList.Clear();

        ResetColor();
        startNode.SetColor(Color.green);
    }

    public void Step()
    {
        Node[] neighbours = grid2D.Neighbours(curNode);
        curNeighbours.Clear();
        curNeighbours.AddRange(neighbours);

        // 이웃 노드 순회
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (closedList.Contains(neighbours[i]))     // 이미 처리한 노드 생략
                continue;
            if (neighbours[i].nType == NodeType.Wall)   // 이동할 수 없는(벽) 노드 생략
                continue;

            // ※ G Cost : 시작 위치에서 현재 위치까지
            
            // 현재 노드까지의 거리 + 현재 노드에서 이웃 노드까지의 거리
            int gCost = curNode.GCost + GetDistance(neighbours[i], curNode);

            // 오픈 노드 리스트에 없거나, 이웃 노드의 gCost 값이 현재 gCost보다 크다면 갱신 처리
            if (!openList.Contains(neighbours[i]) || gCost < neighbours[i].GCost)
            {
                // ※ H Cost : 현재 위치(이웃노드)에서 목표점까지
                int hCost = GetDistance(neighbours[i], targetNode);
                neighbours[i].SetGCost(gCost);
                neighbours[i].SetHCost(hCost);
                neighbours[i].SetParent(curNode);

                if (!openList.Contains(neighbours[i]))
                    openList.Add(neighbours[i]);
            }
        }

        if (openList.Contains(curNode))
            openList.Remove(curNode);
        closedList.Add(curNode);

        // 최소 비용인 노드를 현재 노드로 설정 (다음 스텝으로)
        if (openList.Count > 0)
        {
            if(curNode != null)
                prevNode = curNode;
            openList.Sort(nodeComparer);
            curNode = openList[0];
        }

        if (prevNode == targetNode)
        {
            List<Node> nodes = RetracePath(curNode);
            pathNode = nodes;
            pathNode.RemoveAt(pathNode.Count - 1);

            // 2초 뒤에 ResetNode 함수 호출
            //Invoke("ResetNode", 2.0f);
            Debug.Log("찾음!");
        }
        ResetColor();
    }

    public void FindPathCoroutine(BaseChar player, BaseChar target)
    {
        if (!execute)
        {
            Ready(player.transform.position, target.transform.position);
            StartCoroutine(IEStep(player));
        }
        else
        {
            PairNode pairNode = new PairNode();
            pairNode.player = player;
            pairNode.target = target;
            if(orderlist.Contains(pairNode))
            {
                
            }
        }
    }

    public IEnumerator IEStep(BaseChar player)
    {
        Node[] neighbours = grid2D.Neighbours(curNode);
        curNeighbours.Clear();
        curNeighbours.AddRange(neighbours);

        // 이웃 노드 순회
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (closedList.Contains(neighbours[i]))     // 이미 처리한 노드 생략
                continue;
            if (neighbours[i].nType == NodeType.Wall)   // 이동할 수 없는(벽) 노드 생략
                continue;

            // ※ G Cost : 시작 위치에서 현재 위치까지

            // 현재 노드까지의 거리 + 현재 노드에서 이웃 노드까지의 거리
            int gCost = curNode.GCost + GetDistance(neighbours[i], curNode);

            // 오픈 노드 리스트에 없거나, 이웃 노드의 gCost 값이 현재 gCost보다 크다면 갱신 처리
            if (!openList.Contains(neighbours[i]) || gCost < neighbours[i].GCost)
            {
                // ※ H Cost : 현재 위치(이웃노드)에서 목표점까지
                int hCost = GetDistance(neighbours[i], targetNode);
                neighbours[i].SetGCost(gCost);
                neighbours[i].SetHCost(hCost);
                neighbours[i].SetParent(curNode);

                if (!openList.Contains(neighbours[i]))
                    openList.Add(neighbours[i]);
            }
        }

        closedList.Add(curNode);
        if (openList.Contains(curNode))
            openList.Remove(curNode);
        

        // 최소 비용인 노드를 현재 노드로 설정 (다음 스텝으로)
        if (openList.Count > 0)
        {
            openList.Sort(nodeComparer);
            if (curNode != null)
                prevNode = curNode;            
            curNode = openList[0];
        }

        ResetColor();
        yield return null;

        if (curNode == targetNode)
        {
            List<Node> nodes = RetracePath(curNode);
            pathNode = nodes;
            Debug.Log("찾음!");
            ResetColor();
            player.SetPath(nodes);
            execute = false;
        }
        else
            StartCoroutine(IEStep(player));

    }

    public void FindPath(Vector3 player, Vector3 target)
    {
        Node playerNode = grid2D.FindNode(player);
        Node targetNode = grid2D.FindNode(target);
        Node currentNode = playerNode;        
        
        playerNode.SetGCost(0);
        playerNode.SetHCost(GetDistance(playerNode, targetNode));
        playerNode.SetParent(null);
        targetNode.SetParent(null);

        openList.Clear();
        closedList.Clear();

        do
        {
            Node[] neighbours = grid2D.Neighbours(currentNode);

            // 이웃 노드 순회
            for (int i=0; i<neighbours.Length; i++)
            {
                if (closedList.Contains(neighbours[i]))     // 이미 처리한 노드 생략
                    continue;
                if (neighbours[i].nType == NodeType.Wall)   // 이동할 수 없는(벽) 노드 생략
                    continue;

                // ※ G Cost : 시작 위치에서 현재 위치까지
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

                    if (!openList.Contains(neighbours[i]))
                        openList.Add(neighbours[i]);
                }
            }

            if (openList.Contains(currentNode))
                openList.Remove(currentNode);
            closedList.Add(currentNode);

            // 최소 비용인 노드를 현재 노드로 설정
            if (openList.Count > 0)
            {
                openList.Sort(nodeComparer);
                currentNode = openList[0];
            }

            if (currentNode == targetNode)
            {
                List<Node> nodes = RetracePath(currentNode);

                // 2초 뒤에 ResetNode 함수 호출
                //Invoke("ResetNode", 2.0f);

                Debug.Log("찾음!");
                break;
            }

        } while (openList.Count > 0);
    }

    public List<Node> RetracePath(Node curNode)
    {
        List<Node> nodes = new List<Node>();

        while(curNode != null)
        {
            nodes.Add(curNode);
            curNode = curNode.Parent;
        }

        nodes.Reverse();
        return nodes;
    }

    public void ResetNode()
    {
        curNode = null;
        startNode = null;
        targetNode = null;
        prevNode = null;
        pathNode.Clear();
        curNeighbours.Clear();        
        openList.Clear();
        closedList.Clear();
        grid2D.ResetNode();
    }

    /* 검사완료 노드 : 회색
     * 검사할 노드 : 노랑
     * 이웃 노드 : 파랑
     * 현재 노드 : 초록
     * 타겟 노드 : 분홍
     */
    public void ResetColor()
    {
        foreach(var n in openList)
            n.SetColor(Color.yellow);
        
        foreach(var n in curNeighbours)
            n.SetColor(Color.blue);

        foreach (var n in closedList)
            n.SetColor(Color.gray);

        if (prevNode != null)
            prevNode.SetColor(Color.green);

        targetNode.SetColor(Color.magenta);

        if(pathNode != null)
            foreach (var n in pathNode)
                n.SetColor(Color.red);
    }
}
