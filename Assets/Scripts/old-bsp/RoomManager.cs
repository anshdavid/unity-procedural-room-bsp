using UnityEngine;
using System.Collections.Generic;

public class RoomManager
{
    int Trim;
    int BoardWidht;
    int BoardHeight;

    public List<Rect> Rooms;

    public RoomManager(int trim, int boardWidht, int boardHeight)
    {
        this.Rooms = new List<Rect>();
        this.Trim = trim;
        this.BoardWidht = boardWidht;
        this.BoardHeight = boardHeight;
    }

    public List<Rect> trimRooms()
    {
        List<Rect> new_ = new List<Rect>();
        if (this.Rooms.Count == 0)
        {
            Debug.LogErrorFormat("[RoomManager] room list emtpy!");
        }
        else
        {
            foreach (Rect _room in Rooms)
            {
                int x = 0, y = 0, w = 0, h = 0;

                if (_room.x == 0 && _room.xMax != BoardWidht)
                {
                    w = (int)_room.width - Trim;
                }
                else if (_room.xMax == BoardWidht && _room.x != 0)
                {
                    x = (int)_room.x + Trim;
                }
                else
                {
                    x = (int)_room.x + Trim;
                    w = (int)_room.width - Trim;
                }

                if (_room.y == 0 && _room.yMax != BoardHeight)
                {
                    h = (int)_room.height - Trim;

                }
                else if (_room.yMax == BoardHeight && _room.y != 0)
                {
                    y = (int)_room.y + Trim;
                }
                else
                {
                    y = (int)_room.y + Trim;
                    h = (int)_room.height - Trim;
                }

                new_.Add(new Rect(x, y, w, h));

                // new_.Add(new Rect(_room.x + Trim, _room.y + Trim, _room.width - Trim - 1, _room.height - Trim - 1));

            }
        }
        return new_;
    }
}