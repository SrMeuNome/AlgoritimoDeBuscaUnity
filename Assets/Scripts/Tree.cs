using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public Node branchNode { get; }
    private Vector2Int startPositionMap;
    private Vector2Int endPositionMap;

    public Tree(Vector2Int positionBranch, Vector2Int startPositionMap, Vector2Int endPositionMap)
    {
        this.branchNode = new Node();
        this.branchNode.value = positionBranch;
        this.startPositionMap = startPositionMap;
        this.endPositionMap = endPositionMap;

        Node auxNodeX = branchNode;
        Node auxNodeY = branchNode;

        //Adicionando nos para direita
        for (int i = positionBranch.x; i < endPositionMap.x; i++)
        {
            //Adicionando nos para cima
            for(int j = positionBranch.y; j < startPositionMap.y; j++)
            {
                auxNodeY.upChild = new Node();
                auxNodeY.upChild.value = new Vector2Int(auxNodeY.value.x, auxNodeY.value.y + 1);
                auxNodeY.upChild.bottomChild = auxNodeY;
                auxNodeY = auxNodeY.upChild;
            }

            auxNodeY = auxNodeX;

            //Adicionando nos para baixo
            for (int j = positionBranch.y; j > endPositionMap.y; j--)
            {
                auxNodeY.bottomChild = new Node();
                auxNodeY.bottomChild.value = new Vector2Int(auxNodeY.value.x, auxNodeY.value.y - 1);
                auxNodeY.bottomChild.upChild = auxNodeY;
                auxNodeY = auxNodeY.bottomChild;
            }

            auxNodeX.rightChild = new Node();
            auxNodeX.rightChild.value = new Vector2Int(auxNodeX.value.x + 1, auxNodeX.value.y);
            auxNodeX.rightChild.leftChild = auxNodeX;
            auxNodeX = auxNodeX.rightChild;
            auxNodeY = auxNodeX;
        }

        auxNodeX = branchNode;
        auxNodeY = branchNode;

        //Adicionando nos para esquerda
        for (int i = positionBranch.x; i > startPositionMap.x; i--)
        {
            //Adicionando nos para cima
            for (int j = positionBranch.y; j < startPositionMap.y; j++)
            {
                auxNodeY.upChild = new Node();
                auxNodeY.upChild.value = new Vector2Int(auxNodeY.value.x, auxNodeY.value.y + 1);
                auxNodeY.upChild.bottomChild = auxNodeY;
                auxNodeY = auxNodeY.upChild;
            }

            auxNodeY = auxNodeX;

            //Adicionando nos para baixo
            for (int j = positionBranch.y; j > endPositionMap.y; j--)
            {
                auxNodeY.bottomChild = new Node();
                auxNodeY.bottomChild.value = new Vector2Int(auxNodeY.value.x, auxNodeY.value.y - 1);
                auxNodeY.bottomChild.upChild = auxNodeY;
                auxNodeY = auxNodeY.bottomChild;
            }

            auxNodeX.leftChild = new Node();
            auxNodeX.leftChild.value = new Vector2Int(auxNodeX.value.x - 1, auxNodeX.value.y);
            auxNodeX.leftChild.rightChild = auxNodeX;
            auxNodeX = auxNodeX.leftChild;
            auxNodeY = auxNodeX;
        }
    }

    public void printTree()
    {
        Node auxNode = this.branchNode;
        
        //printando esquerda
        while(auxNode != null)
        {
            if(auxNode != null)
            {
                MonoBehaviour.print("Esquerda: " + auxNode.value);
            }
            auxNode = auxNode.leftChild;
        }

        auxNode = this.branchNode;

        //printando direita
        while(auxNode != null)
        {
            if(auxNode != null)
            {
                MonoBehaviour.print("Direita: " + auxNode.value);
            }
            auxNode = auxNode.rightChild;
        }

        auxNode = this.branchNode;

        //printando cima
        while (auxNode != null)
        {
            if (auxNode != null)
            {
                MonoBehaviour.print("Cima: " + auxNode.value);
            }
            auxNode = auxNode.upChild;
        }

        auxNode = this.branchNode;

        //printando baixo
        while (auxNode != null)
        {
            if (auxNode != null)
            {
                MonoBehaviour.print("Baixo: " + auxNode.value);
            }
            auxNode = auxNode.bottomChild;
        }
    }

    public List<Node> retornarCaminho()
    {
        List<Node> listaDeNos = new List<Node>();

        Node auxNode = this.branchNode;

        //printando esquerda
        while (auxNode != null)
        {
            if (auxNode != null)
            {
                listaDeNos.Add(auxNode);
            }
            auxNode = auxNode.leftChild;
        }

        auxNode = this.branchNode;

        //printando direita
        while (auxNode != null)
        {
            if (auxNode != null)
            {
                listaDeNos.Add(auxNode);
            }
            auxNode = auxNode.rightChild;
        }

        auxNode = this.branchNode;

        //printando cima
        while (auxNode != null)
        {
            listaDeNos.Add(auxNode);
            auxNode = auxNode.upChild;
        }

        auxNode = this.branchNode;

        //printando baixo
        while (auxNode != null)
        {
            if (auxNode != null)
            {
                listaDeNos.Add(auxNode);
            }
            auxNode = auxNode.bottomChild;
        }

        return listaDeNos;
    }
}
