using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    enum TypeMove
    {
        Top,
        Bottom,
        Left,
        Right
    }
    // Start is called before the first frame update

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Move(gameObject, TypeMove.Left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(gameObject, TypeMove.Right);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(gameObject, TypeMove.Top);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(gameObject, TypeMove.Bottom);
        }
    }

    void Move(GameObject gameObject, TypeMove typeMove)
    {
        Node nodeAux;
        switch (typeMove)
        {
            case TypeMove.Top:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.player = false;
                gameObject.transform.Translate(new Vector3Int(0, 1, 0));
                break;

            case TypeMove.Bottom:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.player = false;
                gameObject.transform.Translate(new Vector3Int(0, -1, 0));
                break;

            case TypeMove.Right:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.player = false;
                gameObject.transform.Translate(new Vector3Int(1, 0, 0));
                break;

            case TypeMove.Left:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.player = false;
                gameObject.transform.Translate(new Vector3Int(-1, 0, 0));
                break;

            default:
                nodeAux = Gerador.tree.SearchByValue(gameObject.transform.position, Tree.TypeSearch.depth);
                nodeAux.player = true;
                break;
        }
    }
}
