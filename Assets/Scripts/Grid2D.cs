using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
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

        for(int row = 0; row < nodeCount; row++)
        {
            for (int col = 0; col < nodeCount; col++)
            {
                Node node = Instantiate(nodePrefab, new Vector3(col + 0.5f - center, row + 0.5f - center, 0), Quaternion.identity);
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
}
