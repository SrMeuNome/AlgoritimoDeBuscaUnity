using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<Vector3Int> roat;
    public int i = 0;
    public bool activateMove = false;
    private float time = 0.5f;
    private float timeTrash = 0.5f;
    private int attempt = 0;

    public Tree.TypeSearch typeSearch;
    // Start is called before the first frame update
    void Start()
    {
        roat = Gerador.tree.MakeRoad(gameObject.transform.position, typeSearch);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;
        int qtd = roat.Count;

        if (roat != null && !activateMove)
        {
            if (i < qtd & time <= 0)
            {
                activateMove = false;
                Node nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, typeSearch);
                nodeAux.player = false;
                Node nextPositionNode = Gerador.tree.SearchByValue(roat[i], typeSearch);
                if (nextPositionNode.npc == false && nextPositionNode.player == false)
                {
                    gameObject.transform.position = roat[i];
                    nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, typeSearch);
                    nodeAux.player = true;
                    time = 0.5f;
                    i++;
                    attempt = 0;
                }
                else
                {
                    roat = Gerador.tree.MakeRoad(gameObject.transform.position, typeSearch);
                    i = 0;
                    time = 0.5f;
                    print("if do else");
                }
            }
            else if (i >= qtd & time <= 0)
            {
                roat = Gerador.tree.MakeRoad(gameObject.transform.position, typeSearch);
                i = 0;
                time = 0.5f;
                print("if do else");
            }
        }else if (activateMove)
        {
            activateMove = false;
            timeTrash = 0.5f;
            roat = Gerador.tree.MakeRoad(gameObject.transform.position, typeSearch);
            i = 0;
        }
        else
        {
            timeTrash -= Time.deltaTime;
            if(timeTrash <= 0 && attempt <= 3)
            {
                timeTrash = 0.5f;
                roat = Gerador.tree.MakeRoad(gameObject.transform.position, typeSearch);
                i = 0;
                attempt++;
            }
        }
    }

    
}
