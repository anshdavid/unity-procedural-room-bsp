using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Partition
{
    public Partition left, right;
    public Rect rect;
    public Rect room = new Rect(-1, -1, 0, 0); // i.e null

    public Partition(Rect mrect)
    {
        this.rect = mrect;
        this.left = null;
        this.right = null;
    }

    public bool IAmLeaf()
    {
        return left == null && right == null;
    }

    public bool Split(int minRoomSize, int maxRoomSize)
    {
        bool splitH;

        if (!IAmLeaf())
        {
            return false;
        }

        if (rect.width / rect.height >= 1.25)
        {
            splitH = false;
        }
        else if (rect.height / rect.width >= 1.25)
        {
            splitH = true;
        }
        else
        {
            splitH = Random.Range(0.0f, 1.0f) > 0.5;
        }

        // splitH = Random.Range(0.0f, 1.0f) > 0.5;

        if (Mathf.Min(rect.height, rect.width) / 2 < minRoomSize)
        {
            return false;
        }

        if (splitH)
        {
            // split so that the resulting sub-dungeons widths are not too small
            // (since we are splitting horizontally)

            int split = Random.Range(minRoomSize, (int)(rect.width - minRoomSize));
            // int split = (int)((rect.width - minRoomSize) / 2);


            left = new Partition(new Rect(rect.x, rect.y, rect.width, split));
            right = new Partition(
                new Rect(rect.x, rect.y + split, rect.width, rect.height - split));
        }
        else
        {
            int split = Random.Range(minRoomSize, (int)(rect.height - minRoomSize));
            // int split = (int)((rect.height - minRoomSize) / 2);


            left = new Partition(new Rect(rect.x, rect.y, split, rect.height));
            right = new Partition(
                new Rect(rect.x + split, rect.y, rect.width - split, rect.height));
        }

        return true;
    }

    public void CreateRoom()
    {
        if (left != null)
        {
            left.CreateRoom();
        }
        if (right != null)
        {
            right.CreateRoom();
        }
        if (IAmLeaf())
        {

            int roomWidth = (int)(rect.width - 2);
            int roomHeight = (int)(rect.height - 2);
            int roomX = (int)(rect.width - roomWidth - 2);
            int roomY = (int)(rect.height - roomHeight - 2);

            // int roomWidth = (int)(rect.width);
            // int roomHeight = (int)(rect.height);
            // int roomX = (int)(rect.width - roomWidth);
            // int roomY = (int)(rect.height - roomHeight);

            room = new Rect(rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
        }
    }
}
