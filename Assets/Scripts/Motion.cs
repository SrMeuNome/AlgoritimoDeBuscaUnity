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
        switch (typeMove)
        {
            case TypeMove.Top:
                gameObject.transform.Translate(new Vector3Int(0, 1, 0));
                break;

            case TypeMove.Bottom:
                gameObject.transform.Translate(new Vector3Int(0, -1, 0));
                break;

            case TypeMove.Right:
                gameObject.transform.Translate(new Vector3Int(1, 0, 0));
                break;

            case TypeMove.Left:
                gameObject.transform.Translate(new Vector3Int(-1, 0, 0));
                break;
        }
    }
}
