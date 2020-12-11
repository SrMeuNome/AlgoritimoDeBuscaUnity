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

    private Node find;
    public Vector2Int buscar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Aspirador");
        tree = new Tree(new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(2, -2));
        tree.printTree();
        listaDeNos = tree.retornarCaminho();
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        //if (i < listaDeNos.Count & time <= 0)
        //{
        //    player.transform.position = new Vector3(listaDeNos[i].value.x + 0.5f, listaDeNos[i].value.y - 0.5f, 0);
        //    i++;
        //    time = 0.5f;
        //}
        /*else*/ if (time <= 0)
        {
            find = (Node) tree.widthSearch(buscar);
            //print(find.value);
            player.transform.position = new Vector3(find.value.x + 0.5f, find.value.y - 0.5f, 0);
            time = 0.5f;
        }
    }
}
