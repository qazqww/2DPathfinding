using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    None,
    Wall,
}

public class Grid2D : MonoBehaviour
{
    NodeType nodeType;

    Node[,] nodeArr;
    // List<List<Node>> nodeArr = new List<List<Node>>();

    List<Node> neighbours = new List<Node>(); // 이웃 노드들을 담을 리스트
    
    public int nodeCount = 8;
    public Node nodePrefab;

    void Awake()
    {
        nodePrefab = Resources.Load<Node>("Node");
        CreateGrid(4);
    }

    void Update()
    {
        
    }

    void CreateGrid(int nodeCount)
    {
        float center = nodeCount / 2f;
        int count = 0;
        this.nodeCount = nodeCount;

        nodeArr = new Node[nodeCount, nodeCount];

        for(int row = 0; row < nodeCount; row++)
        {
            for (int col = 0; col < nodeCount; col++)
            {
                Node node = Instantiate(nodePrefab, new Vector3(col + 0.5f - center, row + 0.5f - center, 0), Quaternion.identity);
                nodeArr[row, col] = node;
                node.name = "Node-" + ++count;
                node.SetNode(row, col);

                Renderer renderer = node.GetComponent<Renderer>();
                if (renderer != null) {
                    Vector3 color = Random.insideUnitSphere;
                    renderer.material.color = new Color(color.x, color.y, color.z);
                }
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
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (i == 0 && j == 0) continue;
                if (CheckNode(node.Row + i, node.Col + j))
                    neighbours.Add(nodeArr[node.Row + i, node.Col + j]);
            }
        }

        return neighbours.ToArray();
    }

    // 위치값을 받아 작동
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

    public Node FindNode (Vector3 pos)
    {
        for (int row = 0; row < nodeCount; row++) {
            for (int col = 0; col < nodeCount; col++) {
                if (nodeArr[row, col].Contains(pos))
                {
                    return nodeArr[row, col];
                }
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
