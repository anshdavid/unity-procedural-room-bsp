using System.Collections.Generic;
using UnityEngine;

public class CreateDungeon : MonoBehaviour
{
    public int mapwidth = 50;
    public int mapdepth = 50;
    public int scale = 2;
    Leaf root;

    byte[,] map;
    List<Vector2> corridors = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        map = new byte[mapwidth, mapdepth];
        for (int z = 0; z < mapdepth; z++)
            for (int x = 0; x < mapwidth; x++)
                map[x, z] = 1;

        root = new Leaf(0, 0, mapwidth, mapdepth, scale);
        BSP(root, 6);

        AddCorridors();
        AddRandomCorridors(10);

        DrawMap();
    }

    void AddRandomCorridors(int numHalls)
    {
        for (int i = 0; i < numHalls; i++)
        {
            int startX = Random.Range(5, mapwidth - 5);
            int startZ = Random.Range(5, mapdepth - 5);
            int length = Random.Range(5, mapwidth - 5);

            if (Random.Range(0, 100) < 50)
                line(startX, startZ, length, startZ);
            else
                line(startX, startZ, startX, length);
        }
    }

    void BSP(Leaf leaf, int sDepth)
    {
        if (leaf == null)
            return;
        if (sDepth <= 0)
        {
            leaf.Draw(map);
            corridors.Add(new Vector2(leaf.xpos + leaf.width / 2, leaf.zpos + leaf.depth / 2));
            return;
        }

        if (leaf.Split())
        {
            BSP(leaf.leftChild, sDepth - 1);
            BSP(leaf.rightChild, sDepth - 1);
        }
        else
        {
            leaf.Draw(map);
            corridors.Add(new Vector2(leaf.xpos + leaf.width / 2, leaf.zpos + leaf.depth / 2));
        }
    }

    void AddCorridors()
    {
        for (int i = 1; i < corridors.Count; i++)
        {
            if ((int)corridors[i].x == (int)corridors[i - 1].x || (int)corridors[i].y == (int)corridors[i - 1].y)
                line((int)corridors[i].x, (int)corridors[i].y, (int)corridors[i - 1].x, (int)corridors[i - 1].y);
            else
            {
                line((int)corridors[i].x, (int)corridors[i].y, (int)corridors[i].x, (int)corridors[i - 1].y);
                line((int)corridors[i].x, (int)corridors[i].y, (int)corridors[i - 1].x, (int)corridors[i].y);
            }
        }
    }

    void DrawMap()
    {
        for (int z = 0; z < mapdepth; z++)
            for (int x = 0; x < mapwidth; x++)
            {
                Color c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                if (map[x, z] == 1)
                {
                    Debug.Log("1");
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * scale, 10, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                    cube.GetComponent<Renderer>().material.SetColor("_Color", c);
                }
                else if (map[x, z] == 2)
                {
                    Debug.Log("2");
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * scale, 10, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                    cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0));
                }
            }
    }

    //Adapted Bresenham's line algorithm
    //https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
    public void line(int x, int y, int x2, int y2)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1;
            else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            map[x, y] = 0;
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
    }
}
