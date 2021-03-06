﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public List<Vector3Int> roat;
    public int i = 0;
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

        if (roat != null)
        {
            if (i < qtd & time <= 0)
            {
                Node nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.npc = false;
                Node nextPositionNode = Gerador.tree.SearchByValue(roat[i], Tree.TypeSearch.depth);
                if(nextPositionNode.npc == false && nextPositionNode.player == false)
                {
                    gameObject.transform.position = roat[i];
                    nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                    nodeAux.npc = true;
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
        }
        else
        {
            timeTrash -= Time.deltaTime;
            if (timeTrash <= 0 && attempt <= 3)
            {
                timeTrash = 0.5f;
                roat = Gerador.tree.MakeRoad(gameObject.transform.position, typeSearch);
                i = 0;
                attempt++;
            }
        }
    }
}
