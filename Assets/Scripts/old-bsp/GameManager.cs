

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int maxRoomWidth, maxRoomHeight;
    public int minRoomWidth, minRoomHeight;
    public int bspIteration;
    public int trim;
    public float spiltProb;
    public GameObject floorTile;
    public GameObject discarded;
    private GameObject[,] Floor;


    public void DrawRooms(List<Rect> Rooms)
    {
        // Floor = new GameObject();
        // Floor.name = "Floor";
        GameObject _tile;
        foreach (Rect _room in Rooms)
        {
            if (!CheckRoomSize(_room))
            {
                _tile = discarded;
            }
            else
            {
                _tile = floorTile;
            }

            for (int i = (int)_room.x; i <= _room.xMax; i++)
            {
                for (int j = (int)_room.y; j <= _room.yMax; j++)
                {
                    GameObject instance = Instantiate(_tile,
                        new Vector3(i, j, 0f),
                        Quaternion.identity) as GameObject;
                    instance.transform.SetParent(transform);
                    try
                    {
                        Floor[i, j] = instance;
                    }
                    catch (System.Exception)
                    {
                        Debug.LogError(string.Format("x: {0}, y: {1}", i.ToString(), j.ToString()));
                    }
                }

            }
        }
    }

    private bool CheckRoomSize(Rect room)
    {
        if (room.width < minRoomWidth ||
            room.height < minRoomHeight)
        {
            Debug.LogFormat("discarding room x:{0} y:{1} width:{2} height:{3}",
                room.x, room.xMax, room.width, room.height);
            return false;
        }
        else
        {
            return true;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            main();
        }
    }
    void Start()
    {
        main();
    }

    private void main()
    {
        Partition Board = new Partition(new Rect(0, 0, boardRows, boardColumns));
        Floor = new GameObject[boardRows, boardColumns];

        RoomManager objRoomManager =
            new RoomManager(
                trim,
                boardRows,
                boardColumns);

        BSPTree objBspTree =
            new BSPTree(
                maxRoomWidth,
                maxRoomHeight,
                spiltProb);


        for (int i = -1; i < bspIteration; ++i)
        {
            objBspTree.CreateBSP(Board);
        }
        objBspTree.InstantiateRooms(Board);

        objRoomManager.Rooms = objBspTree.getRooms(Board);
        // DrawRooms(objRoomManager.Rooms);

        List<Rect> trimmed = objRoomManager.trimRooms();
        DrawRooms(trimmed);
    }
}
