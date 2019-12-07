using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shapes
{
    star,
    circle,
    square,
    triangle,
    Pentagon,
    count
}

public class Shape : MonoBehaviour
{
    public Shapes mShape;
    public Board mBoard;
    public Vector2 gridPos;
    public bool Selected = false;
    public Sprite currentSprite;
    public Sprite[] listSprite = new Sprite[(int)Shapes.count];
    public int index;
    public Shape up, down, left, right;
    private SpriteRenderer sr;


    public void SetShapePos(int x, int y)
    {
        gridPos.x = x;
        gridPos.y = y;
    }
    public void UpdatePos()
    {
        transform.position = gridPos;
    }
    public void UpdateAdjacent()
    {
        int i;
        int check;

        //Up
        i = (int)(index - mBoard.boardSize.x);
        if (i >= 0 && mBoard.shapeList[i] != null) 
        {
            up = mBoard.shapeList[i].GetComponent<Shape>();
        }

        //Down
        i = (int)(index + mBoard.boardSize.x);
        if(i < mBoard.shapeList.Length && mBoard.shapeList[i] != null) { down = mBoard.shapeList[i].GetComponent<Shape>(); }

        //Left
        check = index % (int)(mBoard.boardSize.x);
        if (check != 0)
        {
            i = (int)(index - 1);
            if (i >= 0 && i < mBoard.shapeList.Length && mBoard.shapeList[i] != null) { left = mBoard.shapeList[i].GetComponent<Shape>(); }
        }

        //Right
        check = (index + 1) % (int)(mBoard.boardSize.x);
        if (check != 0)
        {
            i = (int)(index + 1);
            if (i >= 0 && i < mBoard.shapeList.Length && mBoard.shapeList[i] != null) { right = mBoard.shapeList[i].GetComponent<Shape>(); }
        }
    }
    public void Match3()
    {
        bool vertical = false;
        bool horizontal = false;
        GameObject temp;
        if(up != null && down != null)
        {
            if(mShape == up.mShape && mShape == down.mShape) //check up and down
            {
                vertical = true;
            }
        }
        if(left != null && right != null)
        {
            if(mShape == left.mShape && mShape == right.mShape)
            {
                horizontal = true;
            }
        }
        if(vertical)
        {
            Destroy(up.gameObject);
            Destroy(down.gameObject);
        }
        if(horizontal)
        {
            Destroy(left.gameObject);
            Destroy(right.gameObject);
        }
        if(vertical || horizontal)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnDestroy()
    {
        if (up != null) { up.down = null; }
        if (down != null) { down.up = null; }
        if (left != null) { left.right = null; }
        if (right != null) { right.left = null; }
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int r = Random.Range(0, (int)Shapes.count);
        currentSprite = listSprite[r];
        sr.sprite = currentSprite;
        mShape = (Shapes)r;
    }

    // Update is called once per frame
    void Update()
    {
        if (mBoard.boardReady)
        {
            UpdateAdjacent();
            Match3();
        }
    }

    private void OnMouseDown()
    {
    }
}
