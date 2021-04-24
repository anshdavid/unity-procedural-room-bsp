using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
    public int xpos;
    public int zpos;
    public int width;
    public int depth;
    int scale;
    int roomMin = 5;

    public Leaf leftChild;
    public Leaf rightChild;

    public Leaf(int x, int z, int w, int d, int s)
    {
        xpos = x;
        zpos = z;
        width = w;
        depth = d;
        scale = s;
    }

    public bool Split()
    {
        if (width <= roomMin || depth <= roomMin) return false;

        bool splitHorizontal = Random.Range(0, 100) > 50;
        if (width > depth && width / depth >= 1.2)
            splitHorizontal = false;
        else if (depth > width && depth / width >= 1.2)
            splitHorizontal = true;

        int max = (splitHorizontal ? depth : width) - roomMin;
        if (max <= roomMin)
            return false;

        if (splitHorizontal)
        {
            int l1depth = Random.Range(roomMin, max);
            leftChild = new Leaf(xpos, zpos, width, l1depth, scale);
            rightChild = new Leaf(xpos, zpos + l1depth, width, depth - l1depth, scale);
        }
        else
        {
            int l1width = Random.Range(roomMin, max);
            leftChild = new Leaf(xpos, zpos, l1width, depth, scale);
            rightChild = new Leaf(xpos + l1width, zpos, width - l1width, depth, scale);
        }

        return true;
    }

    public void Draw(byte[,] map)
    {
        int wallSize = Random.Range(1, 3);
        for (int x = xpos + wallSize; x < width + xpos - wallSize; x++)
            for (int z = zpos + wallSize; z < depth + zpos - wallSize; z++)
            {
                map[x, z] = 0;
            }
    }
}
