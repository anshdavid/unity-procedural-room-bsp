using UnityEngine;
using System.Collections.Generic;

public class BSPTree
{
    int maxRoomWidth, maxRoomHeight;
    float spiltProb;

    public BSPTree(int maxW, int maxH, float sp)
    {
        this.maxRoomWidth = maxW;
        this.maxRoomHeight = maxH;
        this.spiltProb = sp;
    }

    public void CreateBSP(Partition Board)
    {
        if (Board.IAmLeaf())
        {
            if (Board.rect.width > maxRoomHeight
                || Board.rect.height > maxRoomHeight)
            {

                if (Board.Split(maxRoomWidth, maxRoomHeight, spiltProb))
                {
                    CreateBSP(Board.left);
                    CreateBSP(Board.right);
                }
            }
        }

        // for iter count
        else
        {
            CreateBSP(Board.left);
            CreateBSP(Board.right);
        }
    }

    public void InstantiateRooms(Partition Board)
    {
        if (Board.left != null)
        {
            InstantiateRooms(Board.left);
        }
        if (Board.right != null)
        {
            InstantiateRooms(Board.right);
        }
        if (Board.IAmLeaf())
        {
            Board.InstantiateRoom();
        }
    }


    public List<Rect> getRooms(Partition Board)
    {
        List<Rect> _rooms = new List<Rect>();
        _getRooms(Board, _rooms);
        Debug.LogFormat("number of rooms {0}", _rooms.Count);
        return _rooms;
    }

    private void _getRooms(Partition Board, List<Rect> Rooms)
    {
        if (Board == null)
        {
            return;
        }
        else if (Board.IAmLeaf())
        {
            Rooms.Add(Board.room);
        }
        else
        {
            _getRooms(Board.left, Rooms);
            _getRooms(Board.right, Rooms);
        }
    }
}