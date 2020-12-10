using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int value { get; set; }
    public int score { get; set; }
    public Node upChild { get; set; }
    public Node bottomChild { get; set; }
    public Node leftChild { get; set; }
    public Node rightChild { get; set; }
    public bool player { get; set; }
    public bool blocked { get; set; }
    public bool trash { get; set; }
    public bool accessed { get; set; }

    public Node()
    {
        this.upChild = null;
        this.bottomChild = null;
        this.leftChild = null;
        this.rightChild = null;
        this.player = false;
        this.blocked = false;
        this.trash = false;
    }

}
