using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortColor : MonoBehaviour
{
    Color color;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float r = Random.Range(0, 255);
        float g = Random.Range(0, 255);
        float b = Random.Range(0, 255);
        color = new Color(r, g, b);

        spriteRenderer.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
