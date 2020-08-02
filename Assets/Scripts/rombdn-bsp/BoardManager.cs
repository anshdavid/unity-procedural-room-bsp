using UnityEngine;
using System.Collections;

public partial class BoardManager : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject floorTile;
    public GameObject corridorTile;

    private GameObject[,] boardPositionsFloor;

    public void CreateBSP(Partition _subBoard)
    {
        if (_subBoard.IAmLeaf())
        {
            // if the sub-dungeon is too large split it
            if (_subBoard.rect.width > maxRoomSize
                || _subBoard.rect.height > maxRoomSize)
            // || Random.Range(0.0f, 1.0f) > 0.25)
            {

                if (_subBoard.Split(minRoomSize, maxRoomSize))
                {
                    CreateBSP(_subBoard.left);
                    CreateBSP(_subBoard.right);
                }
            }
        }
    }

    public void DrawRooms(Partition _subBoards)
    {
        if (_subBoards == null)
        {
            return;
        }
        if (_subBoards.IAmLeaf())
        {
            for (int i = (int)_subBoards.room.x; i < _subBoards.room.xMax; i++)
            {
                for (int j = (int)_subBoards.room.y; j < _subBoards.room.yMax; j++)
                {
                    GameObject instance = Instantiate(floorTile,
                        new Vector3(i, j, 0f),
                        Quaternion.identity) as GameObject;

                    instance.transform.SetParent(transform);
                    try
                    {
                        boardPositionsFloor[i, j] = instance;
                    }
                    catch (System.Exception)
                    {
                        Debug.LogError(string.Format("x: {0}, y: {1}", i.ToString(), j.ToString()));
                    }

                }
            }
        }
        else
        {
            DrawRooms(_subBoards.left);
            DrawRooms(_subBoards.right);
        }
    }

    void Start()
    {
        Partition Board = new Partition(new Rect(0, 0, boardRows, boardColumns));
        CreateBSP(Board);
        Board.CreateRoom();

        boardPositionsFloor = new GameObject[boardRows, boardColumns];
        DrawRooms(Board);
    }
}
