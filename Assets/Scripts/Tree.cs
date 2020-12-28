using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public Node branchNode { get; }
    private Vector2Int startPositionMap;
    private Vector2Int endPositionMap;
    public enum TypeSearch
    {
        width,
        depth
    }

    public enum ObjSearch
    {
        player,
        npc,
        blocked,
        trash
    }

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

            if (i != startPositionMap.x)
            {
                auxNodeX.leftChild = new Node();
                auxNodeX.leftChild.value = new Vector2Int(auxNodeX.value.x - 1, auxNodeX.value.y);
                auxNodeX.leftChild.rightChild = auxNodeX;
                auxNodeX = auxNodeX.leftChild;
                auxNodeY = auxNodeX;
            }
        }

        if (branchNode.upChild != null)
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

            if (j != endPositionMap.y - 1)
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

            if (j != endPositionMap.y - 1)
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
            if (listNodes[i] != null)
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

    public List<Node> Search(ObjSearch objSearch, TypeSearch typeSearch)
    {
        List<Node> nodeFindList = new List<Node>();
        List<Node> listNodes = new List<Node>();
        int i = 0;

        listNodes.Add(branchNode);

        if (typeSearch == TypeSearch.width)
        {
            if (objSearch == ObjSearch.player)
            {
                //Buscar na raiz
                if (listNodes[i].player)
                {
                    nodeFindList.Add(listNodes[i]);
                }

                while (true)
                {
                    if (listNodes[i].upChild != null)
                    {
                        if (!listNodes[i].upChild.accessed)
                        {
                            if (listNodes[i].upChild.player)
                            {
                                nodeFindList.Add(listNodes[i].upChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].upChild))
                            {
                                listNodes.Add(listNodes[i].upChild);
                            }
                        }
                    }
                    if (listNodes[i].bottomChild != null)
                    {
                        if (!listNodes[i].bottomChild.accessed)
                        {
                            if (listNodes[i].bottomChild.player)
                            {
                                nodeFindList.Add(listNodes[i].bottomChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].bottomChild))
                            {
                                listNodes.Add(listNodes[i].bottomChild);
                            }
                        }
                    }
                    if (listNodes[i].leftChild != null)
                    {
                        if (!listNodes[i].leftChild.accessed)
                        {
                            if (listNodes[i].leftChild.player)
                            {
                                nodeFindList.Add(listNodes[i].leftChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].leftChild))
                            {
                                listNodes.Add(listNodes[i].leftChild);
                            }
                        }
                    }
                    if (listNodes[i].rightChild != null)
                    {
                        if (!listNodes[i].rightChild.accessed)
                        {
                            if (listNodes[i].rightChild.player)
                            {
                                nodeFindList.Add(listNodes[i].rightChild);
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
                    if (i >= listNodes.Count)
                    {
                        break;
                    }
                }
            }
            else if (objSearch == ObjSearch.trash)
            {
                //Buscar na raiz
                if (listNodes[i].trash)
                {
                    nodeFindList.Add(listNodes[i]);
                }

                while (true)
                {
                    if (listNodes[i].upChild != null)
                    {
                        if (!listNodes[i].upChild.accessed)
                        {
                            if (listNodes[i].upChild.trash)
                            {
                                nodeFindList.Add(listNodes[i].upChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].upChild))
                            {
                                listNodes.Add(listNodes[i].upChild);
                            }
                        }
                    }
                    if (listNodes[i].bottomChild != null)
                    {
                        if (!listNodes[i].bottomChild.accessed)
                        {
                            if (listNodes[i].bottomChild.trash)
                            {
                                nodeFindList.Add(listNodes[i].bottomChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].bottomChild))
                            {
                                listNodes.Add(listNodes[i].bottomChild);
                            }
                        }
                    }
                    if (listNodes[i].leftChild != null)
                    {
                        if (!listNodes[i].leftChild.accessed)
                        {
                            if (listNodes[i].leftChild.trash)
                            {
                                nodeFindList.Add(listNodes[i].leftChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].leftChild))
                            {
                                listNodes.Add(listNodes[i].leftChild);
                            }
                        }
                    }
                    if (listNodes[i].rightChild != null)
                    {
                        if (!listNodes[i].rightChild.accessed)
                        {
                            if (listNodes[i].rightChild.trash)
                            {
                                nodeFindList.Add(listNodes[i].rightChild);
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
                    if (i >= listNodes.Count)
                    {
                        break;
                    }
                }
            }
            else if (objSearch == ObjSearch.blocked)
            {
                //Buscar na raiz
                if (listNodes[i].blocked)
                {
                    nodeFindList.Add(listNodes[i]);
                }

                while (true)
                {
                    if (listNodes[i].upChild != null)
                    {
                        if (!listNodes[i].upChild.accessed)
                        {
                            if (listNodes[i].upChild.blocked)
                            {
                                nodeFindList.Add(listNodes[i].upChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].upChild))
                            {
                                listNodes.Add(listNodes[i].upChild);
                            }
                        }
                    }
                    if (listNodes[i].bottomChild != null)
                    {
                        if (!listNodes[i].bottomChild.accessed)
                        {
                            if (listNodes[i].bottomChild.blocked)
                            {
                                nodeFindList.Add(listNodes[i].bottomChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].bottomChild))
                            {
                                listNodes.Add(listNodes[i].bottomChild);
                            }
                        }
                    }
                    if (listNodes[i].leftChild != null)
                    {
                        if (!listNodes[i].leftChild.accessed)
                        {
                            if (listNodes[i].leftChild.blocked)
                            {
                                nodeFindList.Add(listNodes[i].leftChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].leftChild))
                            {
                                listNodes.Add(listNodes[i].leftChild);
                            }
                        }
                    }
                    if (listNodes[i].rightChild != null)
                    {
                        if (!listNodes[i].rightChild.accessed)
                        {
                            if (listNodes[i].rightChild.blocked)
                            {
                                nodeFindList.Add(listNodes[i].rightChild);
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
                    if (i >= listNodes.Count)
                    {
                        break;
                    }
                }
            }
            else if (objSearch == ObjSearch.npc)
            {
                //Buscar na raiz
                if (listNodes[i].npc)
                {
                    nodeFindList.Add(listNodes[i]);
                }

                while (true)
                {
                    if (listNodes[i].upChild != null)
                    {
                        if (!listNodes[i].upChild.accessed)
                        {
                            if (listNodes[i].upChild.npc)
                            {
                                nodeFindList.Add(listNodes[i].upChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].upChild))
                            {
                                listNodes.Add(listNodes[i].upChild);
                            }
                        }
                    }
                    if (listNodes[i].bottomChild != null)
                    {
                        if (!listNodes[i].bottomChild.accessed)
                        {
                            if (listNodes[i].bottomChild.npc)
                            {
                                nodeFindList.Add(listNodes[i].bottomChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].bottomChild))
                            {
                                listNodes.Add(listNodes[i].bottomChild);
                            }
                        }
                    }
                    if (listNodes[i].leftChild != null)
                    {
                        if (!listNodes[i].leftChild.accessed)
                        {
                            if (listNodes[i].leftChild.npc)
                            {
                                nodeFindList.Add(listNodes[i].leftChild);
                            }
                            else if (!listNodes.Contains(listNodes[i].leftChild))
                            {
                                listNodes.Add(listNodes[i].leftChild);
                            }
                        }
                    }
                    if (listNodes[i].rightChild != null)
                    {
                        if (!listNodes[i].rightChild.accessed)
                        {
                            if (listNodes[i].rightChild.npc)
                            {
                                nodeFindList.Add(listNodes[i].rightChild);
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
                    if (i >= listNodes.Count)
                    {
                        break;
                    }
                }
            }

            foreach (Node node in listNodes)
            {
                node.accessed = false;
            }

            return nodeFindList;
        }
        else if (typeSearch == TypeSearch.depth)
        {
            Node nodeAux = this.branchNode;
            bool end = false;
            if (objSearch == ObjSearch.player)
            {
                while (!end)
                {
                    if (nodeAux.player && !nodeAux.accessed)
                    {
                        nodeFindList.Add(nodeAux);
                        nodeAux.accessed = true;
                    }
                    else
                    {
                        nodeAux.accessed = true;
                    }

                    if (nodeAux.upChild != null && !nodeAux.upChild.accessed)
                    {
                        nodeAux = nodeAux.upChild;
                    }
                    else if (nodeAux.rightChild != null && !nodeAux.rightChild.accessed)
                    {
                        nodeAux = nodeAux.rightChild;
                    }
                    else if (nodeAux.leftChild != null && !nodeAux.leftChild.accessed)
                    {
                        nodeAux = nodeAux.leftChild;
                    }
                    else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
                    {
                        nodeAux = nodeAux.bottomChild;
                    }
                    else
                    {
                        end = true;
                    }
                }
            }
            else if (objSearch == ObjSearch.trash)
            {
                while (!end)
                {
                    if (nodeAux.trash && !nodeAux.accessed)
                    {
                        nodeFindList.Add(nodeAux);
                        nodeAux.accessed = true;
                    }
                    else
                    {
                        nodeAux.accessed = true;
                    }

                    if (nodeAux.upChild != null && !nodeAux.upChild.accessed)
                    {
                        nodeAux = nodeAux.upChild;
                    }
                    else if (nodeAux.rightChild != null && !nodeAux.rightChild.accessed)
                    {
                        nodeAux = nodeAux.rightChild;
                    }
                    else if (nodeAux.leftChild != null && !nodeAux.leftChild.accessed)
                    {
                        nodeAux = nodeAux.leftChild;
                    }
                    else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
                    {
                        nodeAux = nodeAux.bottomChild;
                    }
                    else
                    {
                        end = true;
                    }
                }
            }
            else if (objSearch == ObjSearch.blocked)
            {
                while (!end)
                {
                    if (nodeAux.blocked && !nodeAux.accessed)
                    {
                        nodeFindList.Add(nodeAux);
                        nodeAux.accessed = true;
                    }
                    else
                    {
                        nodeAux.accessed = true;
                    }

                    if (nodeAux.upChild != null && !nodeAux.upChild.accessed)
                    {
                        nodeAux = nodeAux.upChild;
                    }
                    else if (nodeAux.rightChild != null && !nodeAux.rightChild.accessed)
                    {
                        nodeAux = nodeAux.rightChild;
                    }
                    else if (nodeAux.leftChild != null && !nodeAux.leftChild.accessed)
                    {
                        nodeAux = nodeAux.leftChild;
                    }
                    else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
                    {
                        nodeAux = nodeAux.bottomChild;
                    }
                    else
                    {
                        end = true;
                    }
                }
            }
            else if (objSearch == ObjSearch.npc)
            {
                while (!end)
                {
                    if (nodeAux.npc && !nodeAux.accessed)
                    {
                        nodeFindList.Add(nodeAux);
                        nodeAux.accessed = true;
                    }
                    else
                    {
                        nodeAux.accessed = true;
                    }

                    if (nodeAux.upChild != null && !nodeAux.upChild.accessed)
                    {
                        nodeAux = nodeAux.upChild;
                    }
                    else if (nodeAux.rightChild != null && !nodeAux.rightChild.accessed)
                    {
                        nodeAux = nodeAux.rightChild;
                    }
                    else if (nodeAux.leftChild != null && !nodeAux.leftChild.accessed)
                    {
                        nodeAux = nodeAux.leftChild;
                    }
                    else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
                    {
                        nodeAux = nodeAux.bottomChild;
                    }
                    else
                    {
                        end = true;
                    }
                }
            }

            //Limpando acesso aos Nós
            nodeAux = this.branchNode;
            end = false;
            while (!end)
            {
                if (nodeAux.accessed)
                {
                    nodeAux.accessed = false;
                }

                if (nodeAux.upChild != null && nodeAux.upChild.accessed)
                {
                    nodeAux = nodeAux.upChild;
                }
                else if (nodeAux.rightChild != null && nodeAux.rightChild.accessed)
                {
                    nodeAux = nodeAux.rightChild;
                }
                else if (nodeAux.leftChild != null && nodeAux.leftChild.accessed)
                {
                    nodeAux = nodeAux.leftChild;
                }
                else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
                {
                    nodeAux = nodeAux.bottomChild;
                }
                else
                {
                    end = true;
                }
            }

            return nodeFindList;
        }
        else
        {
            return null;
        }
    }

    public Node SearchByValue(Vector3 position, TypeSearch typeSearch)
    {
        Vector2Int value = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
        Node find = new Node();
        Node nodeAux = this.branchNode;
        bool end = false;
        List<Node> listNodes = new List<Node>();
        int i = 0;

        listNodes.Add(branchNode);

        if (typeSearch == TypeSearch.width)
        {

            //Buscar na raiz
            if (listNodes[i].value.Equals(value))
            {
                find = listNodes[i];
            }

            while (true)
            {
                if (listNodes[i].upChild != null)
                {
                    if (!listNodes[i].upChild.accessed)
                    {
                        if (listNodes[i].upChild.value.Equals(value))
                        {
                            find = listNodes[i].upChild;
                            break;
                        }
                        else if (!listNodes.Contains(listNodes[i].upChild))
                        {
                            listNodes.Add(listNodes[i].upChild);
                        }
                    }
                }
                if (listNodes[i].bottomChild != null)
                {
                    if (!listNodes[i].bottomChild.accessed)
                    {
                        if (listNodes[i].bottomChild.value.Equals(value))
                        {
                            find = listNodes[i].bottomChild;
                            break;
                        }
                        else if (!listNodes.Contains(listNodes[i].bottomChild))
                        {
                            listNodes.Add(listNodes[i].bottomChild);
                        }
                    }
                }
                if (listNodes[i].leftChild != null)
                {
                    if (!listNodes[i].leftChild.accessed)
                    {
                        if (listNodes[i].leftChild.value.Equals(value))
                        {
                            find = listNodes[i].leftChild;
                            break;
                        }
                        else if (!listNodes.Contains(listNodes[i].leftChild))
                        {
                            listNodes.Add(listNodes[i].leftChild);
                        }
                    }
                }
                if (listNodes[i].rightChild != null)
                {
                    if (!listNodes[i].rightChild.accessed)
                    {
                        if (listNodes[i].rightChild.value.Equals(value))
                        {
                            find = listNodes[i].rightChild;
                            break;
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
        else if (typeSearch == TypeSearch.depth)
        {
            while (!end)
            {
                if (nodeAux.value.Equals(value) && !nodeAux.accessed)
                {
                    find = nodeAux;
                    nodeAux.accessed = true;
                }
                else
                {
                    nodeAux.accessed = true;
                }

                if (nodeAux.upChild != null && !nodeAux.upChild.accessed)
                {
                    nodeAux = nodeAux.upChild;
                }
                else if (nodeAux.rightChild != null && !nodeAux.rightChild.accessed)
                {
                    nodeAux = nodeAux.rightChild;
                }
                else if (nodeAux.leftChild != null && !nodeAux.leftChild.accessed)
                {
                    nodeAux = nodeAux.leftChild;
                }
                else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
                {
                    nodeAux = nodeAux.bottomChild;
                }
                else
                {
                    end = true;
                }
            }
            //Limpando acesso aos Nós
            nodeAux = this.branchNode;
            end = false;
            while (!end)
            {
                if (nodeAux.accessed)
                {
                    nodeAux.accessed = false;
                }

                if (nodeAux.upChild != null && nodeAux.upChild.accessed)
                {
                    nodeAux = nodeAux.upChild;
                }
                else if (nodeAux.rightChild != null && nodeAux.rightChild.accessed)
                {
                    nodeAux = nodeAux.rightChild;
                }
                else if (nodeAux.leftChild != null && nodeAux.leftChild.accessed)
                {
                    nodeAux = nodeAux.leftChild;
                }
                else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
                {
                    nodeAux = nodeAux.bottomChild;
                }
                else
                {
                    end = true;
                }
            }
            return find;
        }
        return null;
    }

    public List<Vector3Int> MakeRoad()
    {
        List<Vector3Int> road = new List<Vector3Int>();
        List<Node> nodesTrash = null;
        Node nodeAux = this.branchNode;
        Node trash = null;
        List<Node> player = null;
        int near = int.MaxValue;
        bool end = false;
        bool find = false;

        nodesTrash = this.Search(ObjSearch.trash, TypeSearch.depth);
        player = this.Search(ObjSearch.player, TypeSearch.depth);

        nodesTrash.ForEach((node) =>
        {
            int valueNodeX = node.value.x;
            int valuePlayerX = player[0].value.x;
            int valueNodeY = node.value.y;
            int valuePlayerY = player[0].value.y;

            if (valueNodeX < 0) valueNodeX *= -1;

            if (valuePlayerX < 0) valuePlayerX *= -1;

            int valueX = valueNodeX - valuePlayerX;

            if (valueX < 0) valueX *= -1;

            if (valueNodeY < 0) valueNodeY *= -1;

            if (valuePlayerY < 0) valuePlayerY *= -1;

            int valueY = valueNodeY - valuePlayerY;

            if (valueY < 0) valueY *= -1;

            int distance = valueX + valueY;

            if (distance <= near)
            {
                trash = node;
                near = distance;
            }
        });

        if(trash == null)
        {
            return null;
        }

        //Adicionando heuristicas
        nodeAux = this.branchNode;
        while (!end)
        {
            int valueNodeX = nodeAux.value.x;
            int valueTrashX = trash.value.x;
            int valueNodeY = nodeAux.value.y;
            int valueTrashY = trash.value.y;
            int distance;
            int valueX, valueY;

            if (nodeAux.blocked)
            {
                distance = int.MaxValue;
            }
            else
            {

                if (valueNodeX < 0) valueNodeX *= -1;

                if (valueTrashX < 0) valueTrashX *= -1;

                valueX = valueNodeX - valueTrashX;

                if (valueX < 0) valueX *= -1;

                if (valueNodeY < 0) valueNodeY *= -1;

                if (valueTrashY < 0) valueTrashY *= -1;

                valueY = valueNodeY - valueTrashY;

                if (valueY < 0) valueY *= -1;

                distance = valueX + valueY;
            }

            if (!nodeAux.accessed)
            {
                nodeAux.accessed = true;
                nodeAux.heuristic = distance;
            }

            if (nodeAux.upChild != null && !nodeAux.upChild.accessed)
            {
                nodeAux = nodeAux.upChild;
            }
            else if (nodeAux.rightChild != null && !nodeAux.rightChild.accessed)
            {
                nodeAux = nodeAux.rightChild;
            }
            else if (nodeAux.leftChild != null && !nodeAux.leftChild.accessed)
            {
                nodeAux = nodeAux.leftChild;
            }
            else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
            {
                nodeAux = nodeAux.bottomChild;
            }
            else
            {
                end = true;
            }
        }

        //Limpando acesso aos Nós
        nodeAux = this.branchNode;
        end = false;
        while (!end)
        {
            if (nodeAux.accessed)
            {
                nodeAux.accessed = false;
            }

            if (nodeAux.upChild != null && nodeAux.upChild.accessed)
            {
                nodeAux = nodeAux.upChild;
            }
            else if (nodeAux.rightChild != null && nodeAux.rightChild.accessed)
            {
                nodeAux = nodeAux.rightChild;
            }
            else if (nodeAux.leftChild != null && nodeAux.leftChild.accessed)
            {
                nodeAux = nodeAux.leftChild;
            }
            else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
            {
                nodeAux = nodeAux.bottomChild;
            }
            else
            {
                end = true;
            }
        }

        MonoBehaviour.print("Finalizei a distribuição de heuristicas");

        Node auxRoad = null;

        nodeAux = player[0];

        List<Node> listNodes = new List<Node>();
        
        int l = 0;

        while (!find)
        {
            if (nodeAux.upChild != null && !nodeAux.upChild.blocked && !nodeAux.upChild.accessed)
            {
                listNodes.Add(nodeAux.upChild);
            }
            if (nodeAux.bottomChild != null && !nodeAux.bottomChild.blocked && !nodeAux.bottomChild.accessed)
            {
                listNodes.Add(nodeAux.bottomChild);
            }
            if (nodeAux.leftChild != null && !nodeAux.leftChild.blocked && !nodeAux.leftChild.accessed)
            {
                listNodes.Add(nodeAux.leftChild);
            }
            if (nodeAux.rightChild != null && !nodeAux.rightChild.blocked && !nodeAux.rightChild.accessed)
            {
                listNodes.Add(nodeAux.rightChild);
            }

            if (listNodes.Count == 0)
            {
                if (nodeAux.upChild != null && !nodeAux.upChild.blocked && nodeAux.upChild.accessed)
                {
                    nodeAux.upChild.accessed = false;
                }
                if (nodeAux.bottomChild != null && !nodeAux.bottomChild.blocked && nodeAux.bottomChild.accessed)
                {
                    nodeAux.bottomChild.accessed = false;
                }
                if (nodeAux.leftChild != null && !nodeAux.leftChild.blocked && nodeAux.leftChild.accessed)
                {
                    nodeAux.leftChild.accessed = false;
                }
                if (nodeAux.rightChild != null && !nodeAux.rightChild.blocked && nodeAux.rightChild.accessed)
                {
                    nodeAux.rightChild.accessed = false;
                }
            }

            listNodes.ForEach((node) =>
            {
                if (listNodes.FindIndex((obj) => { return obj.Equals(node); }) == 0)
                {
                    auxRoad = node;
                }
                else
                {
                    if (node.heuristic <= auxRoad.heuristic)
                    {
                        auxRoad = node;
                    }
                }
            });

            if (auxRoad.Equals(trash))
            {
                find = true;
            }

            road.Add(new Vector3Int(auxRoad.value.x, auxRoad.value.y, 0));
            auxRoad.accessed = true;
            nodeAux = auxRoad;
            listNodes.RemoveRange(0, listNodes.Count);

            if (l > 100)
            {
                break;
            }
            if(l>50)
            {
                MonoBehaviour.print("Up acessed: " + auxRoad.upChild.accessed + " Bottom acessed: " + auxRoad.bottomChild.accessed + " Right acessed: " + auxRoad.rightChild.accessed + " Left acessed: " + auxRoad.leftChild.accessed);
            }
            l++;
        }

        //Limpando acesso aos Nós
        nodeAux = this.branchNode;
        end = false;
        while (!end)
        {
            if (nodeAux.accessed)
            {
                nodeAux.accessed = false;
            }

            if (nodeAux.upChild != null && nodeAux.upChild.accessed)
            {
                nodeAux = nodeAux.upChild;
            }
            else if (nodeAux.rightChild != null && nodeAux.rightChild.accessed)
            {
                nodeAux = nodeAux.rightChild;
            }
            else if (nodeAux.leftChild != null && nodeAux.leftChild.accessed)
            {
                nodeAux = nodeAux.leftChild;
            }
            else if (nodeAux.bottomChild != null && !nodeAux.Equals(this.branchNode))
            {
                nodeAux = nodeAux.bottomChild;
            }
            else
            {
                end = true;
            }
        }

        MonoBehaviour.print("Finalizei o find");
        MonoBehaviour.print("Lista de caminhos: " + road.ToString());
        return road;
    }
}
