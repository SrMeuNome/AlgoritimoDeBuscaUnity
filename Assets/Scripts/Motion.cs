using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    private PlayerScript playerScript;
    private bool move = false;
    enum TypeMove
    {
        Top,
        Bottom,
        Left,
        Right
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Move(gameObject, TypeMove.Left);
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(gameObject, TypeMove.Right);
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(gameObject, TypeMove.Top);
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(gameObject, TypeMove.Bottom);
            move = true;
        }

        if (move && playerScript.isActiveAndEnabled)
        {
            playerScript.enabled = false;
            playerScript.activateMove = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            playerScript.enabled = true;
            move = false;
        }
    }

    void Move(GameObject gameObject, TypeMove typeMove)
    {
        Node nodeAux;
        switch (typeMove)
        {
            case TypeMove.Top:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, playerScript.typeSearch);
                if(nodeAux.upChild != null && !nodeAux.upChild.blocked && !nodeAux.upChild.npc)
                {
                    nodeAux.player = false;
                    gameObject.transform.Translate(new Vector3Int(0, 1, 0));
                }
                break;

            case TypeMove.Bottom:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, playerScript.typeSearch);
                if (nodeAux.bottomChild != null && !nodeAux.bottomChild.blocked && !nodeAux.bottomChild.npc)
                {
                    nodeAux.player = false;
                    gameObject.transform.Translate(new Vector3Int(0, -1, 0));
                }
                break;

            case TypeMove.Right:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, playerScript.typeSearch);
                if (nodeAux.rightChild != null && !nodeAux.rightChild.blocked && !nodeAux.rightChild.npc)
                {
                    nodeAux.player = false;
                    gameObject.transform.Translate(new Vector3Int(1, 0, 0));
                }
                break;

            case TypeMove.Left:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, playerScript.typeSearch);
                if (nodeAux.leftChild != null && !nodeAux.leftChild.blocked && !nodeAux.leftChild.npc)
                {
                    nodeAux.player = false;
                    gameObject.transform.Translate(new Vector3Int(-1, 0, 0));
                }
                break;

            default:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, playerScript.typeSearch);
                nodeAux.player = true;
                break;
        }
    }
}
