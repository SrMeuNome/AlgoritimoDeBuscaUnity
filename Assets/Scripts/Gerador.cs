using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gerador : MonoBehaviour
{
    //Tamanho do mapa
    public int SizeX;

    public int SizeY;

    public static Tree tree;
    //Tilemap é onde fica localizado os Tile Base
    public Tilemap tileMap;

    //TileBase é o tile que queremos utilizar
    public TileBase block;
    public TileBase none;

    public GameObject player;
    public GameObject trash;
    public GameObject npcs;
    public Vector2Int LocalSpawnPlayer;

    public Vector2Int LocalSpawnNPC1;
    public Vector2Int LocalSpawnNPC2;
    public Vector2Int LocalSpawnNPC3;
    public Vector2Int LocalSpawnNPC4;

    public bool spawnNPCs = true;
    public bool spawnWalls = true;
    public bool allTrashs = false;

    // Start is called before the first frame update
    void Start()
    {
        SizeX = SizeX < 0 ? SizeX * -1 : SizeX;
        SizeY = SizeY < 0 ? SizeY * -1 : SizeY;
        tree = new Tree(new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(SizeX, -SizeY));
        int auxX = LocalSpawnPlayer.x < 0 ? LocalSpawnPlayer.x * -1 : LocalSpawnPlayer.x;
        int auxY = LocalSpawnPlayer.y < 0 ? LocalSpawnPlayer.y * -1 : LocalSpawnPlayer.y;
        LocalSpawnPlayer = new Vector2Int(auxX, -auxY);
        GenerateMap();
    }

    public void GenerateMap()
    {
        bool end = false;
        Node nodeAux = tree.branchNode;
        while (!end)
        {
            int sortTrash = allTrashs? 1 : Random.Range(0, 5);

            if (spawnWalls && (nodeAux.value.x == SizeX | nodeAux.value.y == -SizeY | nodeAux.value.x == 0 | nodeAux.value.y == 0
                | (nodeAux.value.x == SizeX / 2 - 1 && (nodeAux.value.y >= -(SizeY / 2 - 1) || nodeAux.value.y <= -(SizeY - (SizeY / 2 - 1))))
                | (nodeAux.value.y == -(SizeY / 2 - 1) && ((nodeAux.value.x <= SizeX / 2 - 1 && nodeAux.value.x != SizeX / 2 - 3) || (nodeAux.value.x >= SizeX - (SizeX / 2 - 1) && nodeAux.value.x != SizeX - (SizeX / 2 - 3))))
                | (nodeAux.value.x == SizeX - (SizeX / 2 - 1) && (nodeAux.value.y >= -(SizeY / 2 - 1) || nodeAux.value.y <= -(SizeY - (SizeY / 2 - 1))))
                | (nodeAux.value.y == -(SizeY - (SizeY / 2 - 1)) && ((nodeAux.value.x >= SizeX - (SizeX / 2 - 1) && nodeAux.value.x != SizeX - (SizeX / 2 - 2)) || (nodeAux.value.x <= SizeX / 2 - 1) && nodeAux.value.x != SizeX / 2 - 5))))
            {
                nodeAux.blocked = true;
            }
            else if(nodeAux.value.x == SizeX | nodeAux.value.y == -SizeY | nodeAux.value.x == 0 | nodeAux.value.y == 0)
            {
                nodeAux.blocked = true;
            }

            if (nodeAux.value.Equals(LocalSpawnPlayer))
            {
                nodeAux.player = true;
                Instantiate(player, new Vector3Int(LocalSpawnPlayer.x, LocalSpawnPlayer.y, 0), Quaternion.identity);
            }

            if (sortTrash == 1 & !nodeAux.blocked)
            {
                nodeAux.trash = true;
                Instantiate(trash, new Vector3Int(nodeAux.value.x, nodeAux.value.y, 0), Quaternion.identity);
            }

            if (spawnNPCs && ((nodeAux.value.Equals(LocalSpawnNPC1) & !nodeAux.blocked) || (nodeAux.value.Equals(LocalSpawnNPC2) & !nodeAux.blocked) || (nodeAux.value.Equals(LocalSpawnNPC3) & !nodeAux.blocked) || (nodeAux.value.Equals(LocalSpawnNPC4) & !nodeAux.blocked)))
            {
                nodeAux.npc = true;
                Instantiate(npcs, new Vector3Int(nodeAux.value.x, nodeAux.value.y, 0), Quaternion.identity);
            }

            if (nodeAux.blocked && !nodeAux.accessed)
            {
                tileMap.SetTile(new Vector3Int(nodeAux.value.x, nodeAux.value.y, 0), block);
                nodeAux.accessed = true;
            }
            else
            {
                tileMap.SetTile(new Vector3Int(nodeAux.value.x, nodeAux.value.y, 0), none);
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
            else if (nodeAux.bottomChild != null && !nodeAux.Equals(tree.branchNode))
            {
                nodeAux = nodeAux.bottomChild;
            }
            else
            {
                end = true;
                print("Cheguei no else");
            }
            print("Fim gerador");
        }
        //Limpando acesso aos Nós
        nodeAux = tree.branchNode;
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
            else if (nodeAux.bottomChild != null && !nodeAux.Equals(tree.branchNode))
            {
                nodeAux = nodeAux.bottomChild;
            }
            else
            {
                end = true;
            }
        }
    }
}
