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

        //Fazendo ligações completas Cima-baixo
        //Adicionando nos para direita
        for (int i = positionBranch.x; i <= endPositionMap.x; i++)
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

            if (i != endPositionMap.x)
            {
                auxNodeX.rightChild = new Node();
                auxNodeX.rightChild.value = new Vector2Int(auxNodeX.value.x + 1, auxNodeX.value.y);
                auxNodeX.rightChild.leftChild = auxNodeX;
                auxNodeX = auxNodeX.rightChild;
                auxNodeY = auxNodeX;
            }
        }

        auxNodeX = branchNode;
        auxNodeY = branchNode;

        //Adicionando nos para esquerda
        for (int i = positionBranch.x; i >= startPositionMap.x; i--)
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

            if(i != startPositionMap.x)
            {
                auxNodeX.leftChild = new Node();
                auxNodeX.leftChild.value = new Vector2Int(auxNodeX.value.x - 1, auxNodeX.value.y);
                auxNodeX.leftChild.rightChild = auxNodeX;
                auxNodeX = auxNodeX.leftChild;
                auxNodeY = auxNodeX;
            }
        }

        if(branchNode.upChild != null)
        {
            auxNodeX = branchNode.upChild;
            auxNodeY = branchNode.upChild;
        }

        //Fazendo ligações completas Esquerda-Direita
        //Adicionando nos para cima
        for (int j = positionBranch.y; j < startPositionMap.y; j++)
        {
            //Adicionando nos para direita
            for (int i = positionBranch.x; i < endPositionMap.x; i++)
            {
                auxNodeX.rightChild = auxNodeX.bottomChild.rightChild.bottomChild;
                auxNodeX.rightChild.leftChild = auxNodeX;
                auxNodeX = auxNodeX.rightChild;
            }

            auxNodeX = auxNodeY;

            //Adicionando nos para esquerda
            for (int i = positionBranch.x; i > startPositionMap.x; i--)
            {
                auxNodeX.leftChild = auxNodeX.bottomChild.leftChild.bottomChild;
                auxNodeX.leftChild.rightChild = auxNodeY;
                auxNodeY = auxNodeY.leftChild;
            }

            if (j != endPositionMap.y -1)
            {
                auxNodeY = auxNodeY.upChild;
                auxNodeX = auxNodeY;
            }
        }

        if (branchNode.bottomChild != null)
        {
            auxNodeX = branchNode.bottomChild;
            auxNodeY = branchNode.bottomChild;
        }

        //Adicionando nos para baixo
        for (int j = positionBranch.y; j > endPositionMap.y; j--)
        {
            //Adicionando nos para direita
            for (int i = positionBranch.x; i < endPositionMap.x; i++)
            {
                auxNodeX.rightChild = auxNodeX.upChild.rightChild.bottomChild;
                auxNodeX.rightChild.leftChild = auxNodeX;
                auxNodeX = auxNodeX.rightChild;
            }

            auxNodeX = auxNodeY;

            //Adicionando nos para esquerda
            for (int i = positionBranch.x; i > startPositionMap.x; i--)
            {
                auxNodeX.leftChild = auxNodeX.upChild.leftChild.bottomChild;
                auxNodeX.leftChild.rightChild = auxNodeY;
                auxNodeY = auxNodeY.leftChild;
            }

            if (j != endPositionMap.y -1)
            {
                auxNodeY = auxNodeY.bottomChild;
                auxNodeX = auxNodeY;
            }
        }
    }

    public void printTree()
    {
        List<Node> listNodes = new List<Node>();
        int i = 0;
        listNodes.Add(branchNode);

        
        while (true)
        {
            if(listNodes[i] != null)
            {
                MonoBehaviour.print("pai:" + listNodes[i].value);
            }

            if (listNodes[i].upChild != null)
            {
                if (!listNodes[i].upChild.accessed)
                {
                    MonoBehaviour.print("Cima:" + listNodes[i].upChild.value);
                    if (!listNodes.Contains(listNodes[i].upChild))
                    {
                        listNodes.Add(listNodes[i].upChild);
                    }
                }
            }
            if (listNodes[i].bottomChild != null)
            {
                if (!listNodes[i].bottomChild.accessed)
                {
                    MonoBehaviour.print("Baixo:" + listNodes[i].bottomChild.value);
                    if (!listNodes.Contains(listNodes[i].bottomChild))
                    {
                        listNodes.Add(listNodes[i].bottomChild);
                    }
                }
            }
            if (listNodes[i].leftChild != null)
            {
                if (!listNodes[i].leftChild.accessed)
                {
                    MonoBehaviour.print("Esquerda:" + listNodes[i].leftChild.value);
                    if (!listNodes.Contains(listNodes[i].leftChild))
                    {
                        listNodes.Add(listNodes[i].leftChild);
                    }
                }
            }
            if (listNodes[i].rightChild != null)
            {
                if (!listNodes[i].rightChild.accessed)
                {
                    MonoBehaviour.print("Direita:" + listNodes[i].rightChild.value);
                    if (!listNodes.Contains(listNodes[i].rightChild))
                    {
                        listNodes.Add(listNodes[i].rightChild);
                    }
                }
            }
            listNodes[i].accessed = true;
            i++;

            //Se por acaso o i tiver o tamanho da lista deve ser parado imediatamente o loop, para evitar erro de index
            if (i >= listNodes.Count)
            {
                break;
            }
        }

        foreach (Node node in listNodes)
        {
            node.accessed = false;
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

    public object widthSearch(Vector2Int value)
    {
        Node nodeFind = null;
        List<Node> listNodes = new List<Node>();
        bool find = false;
        int i = 0;
        listNodes.Add(branchNode);

        //Buscar na raiz
        if (listNodes[i].value.Equals(value))
        {
            nodeFind = listNodes[i];
            find = true;
        }

        while (!find)
        {
            if(listNodes[i].upChild != null && !find)
            {
                if(!listNodes[i].upChild.accessed)
                {
                    if (listNodes[i].upChild.value.Equals(value))
                    {
                        nodeFind = listNodes[i].upChild;
                        find = true;
                    }
                    else if (!listNodes.Contains(listNodes[i].upChild))
                    {
                        listNodes.Add(listNodes[i].upChild);
                    }
                }
            }
            if (listNodes[i].bottomChild != null && !find)
            {
                if (!listNodes[i].bottomChild.accessed)
                {
                    if (listNodes[i].bottomChild.value.Equals(value))
                    {
                        nodeFind = listNodes[i].bottomChild;
                        find = true;
                    }
                    else if (!listNodes.Contains(listNodes[i].bottomChild))
                    {
                        listNodes.Add(listNodes[i].bottomChild);
                    }
                }
            }
            if (listNodes[i].leftChild != null && !find)
            {
                if (!listNodes[i].leftChild.accessed)
                {
                    if (listNodes[i].leftChild.value.Equals(value))
                    {
                        nodeFind = listNodes[i].leftChild;
                        find = true;
                    }
                    else if (!listNodes.Contains(listNodes[i].leftChild))
                    {
                        listNodes.Add(listNodes[i].leftChild);
                    }
                }
            }
            if (listNodes[i].rightChild != null && !find)
            {
                if (!listNodes[i].rightChild.accessed)
                {
                    if (listNodes[i].rightChild.value.Equals(value))
                    {
                        nodeFind = listNodes[i].rightChild;
                        find = true;
                    }
                    else if (!listNodes.Contains(listNodes[i].rightChild))
                    {
                        listNodes.Add(listNodes[i].rightChild);
                    }
                }
            }
            listNodes[i].accessed = true;
            i++;

            //Se por acaso o i tiver o tamanho da lista deve ser parado imediatamente o loop, para evitar erro de index
            if(i >= listNodes.Count)
            {
                break;
            }
        }

        foreach (Node node in listNodes)
        {
            node.accessed = false;
        }

        return nodeFind;
    }
}
