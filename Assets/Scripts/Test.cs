using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static Tree tree;
    public static List<Vector3Int> testeRoad;
    public GameObject coco;
    private int i;
    float time = 0.5f;

    private List<Node> find;
    public Vector2Int buscar;

    bool teste = false;
    // Start is called before the first frame update
    private void Awake()
    {
        tree = new Tree(new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(4, -4));
        tree.branchNode.player = true;
        tree.branchNode.bottomChild.blocked = true;
        tree.branchNode.bottomChild.rightChild.blocked = true;
        tree.branchNode.bottomChild.rightChild.rightChild.blocked = true;
        //tree.branchNode.bottomChild.bottomChild.rightChild.blocked = true;
        //tree.printTree();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
