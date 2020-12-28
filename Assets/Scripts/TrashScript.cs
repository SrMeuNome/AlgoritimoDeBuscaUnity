using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player.transform.position.Equals(gameObject.transform.position))
        {
            Node node = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
            node.trash = false;
            //List<Vector3Int> listAux = player.GetComponent<PlayerScript>().roat;
            //listAux.RemoveRange(0, listAux.Count);
            //if (Gerador.tree.MakeRoad() != null)
            //{
                //player.GetComponent<PlayerScript>().i = 0;
                //player.GetComponent<PlayerScript>().roat = Gerador.tree.MakeRoad();
            //}
            Destroy(gameObject);
        }
    }
}
