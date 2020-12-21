using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<Vector3Int> roat;
    public int i = 0;
    private float time = 0.5f;
    private float timeTrash = 0.5f;
    private int attempt = 0;
    // Start is called before the first frame update
    void Start()
    {
        roat = Gerador.tree.MakeRoad();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;


        if (roat != null)
        {
            print("i: " + i);
            print("Roat: " + roat.Count);
            print("Position: " + gameObject.transform.position);
            if (i < roat.Count & time <= 0)
            {
                Node nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.player = false;
                gameObject.transform.position = roat[i];
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.player = true;
                time = 0.5f;
                print("if");
                i++;
                attempt = 0;
            }
        } else
        {
            timeTrash -= Time.deltaTime;
            if(timeTrash <= 0 && attempt <= 3)
            {
                timeTrash = 0.5f;
                roat = Gerador.tree.MakeRoad();
                attempt++;
            }
        }
    }
}
