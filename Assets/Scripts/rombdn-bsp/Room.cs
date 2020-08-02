public class Room
{
    protected int posX, posY, width, height;

    public Room(int x, int y, int w, int h)
    {
        this.posX = x;
        this.posY = y;
        this.width = w;
        this.height = h;
    }

    public int getX() { return posX; }
    public int getY() { return posY; }
    public int getW() { return width; }
    public int getH() { return height; }
}

// public virtual GameObject Draw()
// {
//     GameObject roomContainer = new GameObject("Room");
//     Color debugColor = Random.ColorHSV();

//     for (int x = left; x <= right; x++)
//     {
//         for (int y = bottom; y <= top; y++)
//         {

//             GameObject tile = new GameObject("Tile");
//             tile.transform.position = new Vector3(x, y, 0);
//             tile.transform.localScale = Vector3.one * 6.25f;
//             tile.transform.SetParent(roomContainer.transform, true);

//             SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
//             sr.sprite = Resources.Load<Sprite>("square");
//             sr.color = debugColor;
//         }
//     }

//     return roomContainer;
// }
