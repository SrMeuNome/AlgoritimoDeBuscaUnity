using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Tree tree;
    private List<Node> listaDeNos;
    private GameObject player;
    private int i;
    float time = 0.5f;

    private List<Node> find;
    public Vector2Int buscar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Aspirador");
        tree = new Tree(new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(3, -3));
        tree.branchNode.player = true;
        tree.branchNode.bottomChild.trash = true;
        tree.branchNode.rightChild.blocked = true;
        tree.branchNode.bottomChild.bottomChild.trash = true;
        //tree.printTree();
        listaDeNos = tree.retornarCaminho();
        find = tree.Search(Tree.ObjSearch.trash, Tree.TypeSearch.depth);
        print("Find count: " + find[0].accessed);
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (i < find.Count & time <= 0)
        {
            player.transform.position = new Vector3(find[i].value.x + 0.5f, find[i].value.y - 0.5f, 0);
            //player.transform.position = new Vector3(listaDeNos[i].value.x + 0.5f, listaDeNos[i].value.y - 0.5f, 0);
            i++;
            time = 0.5f;
        }
        /*else*/
        if (time <= 0)
        {
            //print(find.value);
            //   foreach(Node aux in find)
            //    {
            //        player.transform.position = new Vector3(aux.value.x + 0.5f, aux.value.y - 0.5f, 0);
            //    }
            //    time = 0.5f;
        }
    }
}
