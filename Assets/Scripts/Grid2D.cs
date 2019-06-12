using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
    Node[,] nodeArr;
    // List<List<Node>> nodeArr = new List<List<Node>>();

    List<Node> neighbours = new List<Node>(); // 이웃 노드들을 담을 리스트
    
    public int nodeCount = 8;
    public Node nodePrefab;
    public Transform root;

    void Awake()
    {
        root = transform.Find("Root");
        nodePrefab = Resources.Load<Node>("Node");
        CreateGrid(nodeCount);
    }

    void CreateGrid(int nodeCount)
    {
        float center = nodeCount / 2f;
        int count = 0;
        this.nodeCount = nodeCount;

        nodeArr = new Node[nodeCount, nodeCount];

        for(int row = 0; row < nodeCount; row++) {
            for (int col = 0; col < nodeCount; col++) {
                Node node = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity, root);
                nodeArr[row, col] = node;
                node.name = "Node-" + ++count;
                node.SetNode(row, col);
                node.transform.localPosition = new Vector3(col * 100 - (nodeCount - 1) * 50, row * 100 - (nodeCount - 1) * 50, 0);
            }
        }
    }

    // 배열 범위를 벗어나지 않는지 체크
    bool CheckNode(int row, int col)
    {
        if (row < 0 || row >= nodeCount)
            return false;
        if (col < 0 || col >= nodeCount)
            return false;

        return true;
    }

    // 인접한 노드들을 배열로 리턴해주는 함수
    public Node[] Neighbours(Node node)
    {
        neighbours.Clear();
        for (int row = -1; row <= 1; row++) {
            for (int col = -1; col <= 1; col++) {
                if (row == 0 && col == 0) continue;
                if (CheckNode(node.Row + row, node.Col + col))
                    neighbours.Add(nodeArr[node.Row + row, node.Col + col]);
            }
        }

        return neighbours.ToArray();
    }

    // 위치값을 받아 그 위치에 해당하는 노드를 넘김
    public Node[] Neighbours(Vector3 pos)
    {
        for (int row = 0; row < nodeCount; row++) {
            for (int col = 0; col < nodeCount; col++) {
                if(nodeArr[row,col].Contains(pos))
                {
                    return Neighbours(nodeArr[row, col]);
                }
            }
        }
        return null;
    }

    public Node ClickNode()
    {
        return FindNode(Input.mousePosition);
    }

    public Node FindNode (Vector3 pos)
    {
        for (int row = 0; row < nodeCount; row++)
            for (int col = 0; col < nodeCount; col++)
            {
                RectTransform rect = nodeArr[row, col].GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(rect, pos))
                {
                    Debug.Log(nodeArr[row, col] + " 클릭");
                    return nodeArr[row, col];
                }
            }
        return null;
    }

    public void ResetNode()
    {
        for (int row = 0; row < nodeCount; row++) {
            for (int col = 0; col < nodeCount; col++) {
                    nodeArr[row, col].SetColor(Color.white);
            }
        }
    }
}
