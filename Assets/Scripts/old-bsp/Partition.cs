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

    public bool Split(int maxRoomWidth, int maxRoomHeight, float spiltProb)
    {
        bool _splitV = false;
        bool _splitH = false;
        bool _doSplit = false;
        if (!IAmLeaf())
        {
            return false;
        }
        else
        {
            if (rect.width > maxRoomWidth) { _splitV = true; }
            if (rect.height > maxRoomHeight) { _splitH = true; }
            if (Random.Range(0.0f, 1.0f) > spiltProb) { _doSplit = true; }

            if (_splitV && _doSplit)
            {
                int split = Random.Range(maxRoomWidth, (int)(rect.width - maxRoomWidth));

                left = new Partition(
                    new Rect(rect.x, rect.y, split, rect.height));

                right = new Partition(
                    new Rect(rect.x + split, rect.y, rect.width - split, rect.height));

                return true;

            }

            else if (_splitH && _doSplit)
            {
                int split = Random.Range(maxRoomHeight, (int)(rect.height - maxRoomHeight));

                left = new Partition(
                    new Rect(rect.x, rect.y, rect.width, split));

                right = new Partition(
                    new Rect(rect.x, rect.y + split, rect.width, rect.height - split));

                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void InstantiateRoom()
    {
        // room = new Rect(rect.x, rect.y, (int)(rect.width - 4), (int)(rect.height - 4));
        room = new Rect(rect.x, rect.y, (int)rect.width, (int)rect.height);

    }
}
