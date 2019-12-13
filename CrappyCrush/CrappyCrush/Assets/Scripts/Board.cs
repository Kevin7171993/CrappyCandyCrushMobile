using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject shapeObj;
    public Vector2 boardSize; // 5x5
    public Vector2 cellSize; //1x1
    public Vector2 firstCell;
    public GameObject[] shapeList;
    public bool boardReady = false;
    public Shape select1;
    public Shape select2;

    public Vector2 TopLeft()
    {
        int x, y;
        x = (int)(-(boardSize.x / 2));
        y = (int)(boardSize.y / 2);
        return new Vector2(x, y);
    }
    public Vector2 BottomRight()
    {
        int x, y;
        x = (int)-(TopLeft().x);
        y = (int)-(TopLeft().y);
        return new Vector2(x, y);
    }
    public void Populate()
    {
        int sx = (int)TopLeft().x;
        int sy = (int)TopLeft().y;
        int ix = 0;
        int iy = 0;
        for (int i = 0; i < shapeList.Length; i++)
        {
            if(shapeList[i] == null)
            {
                shapeList[i] = Instantiate(shapeObj);
                Shape cShape = shapeList[i].GetComponent<Shape>();
                
                cShape.SetShapePos(sx + ix, sy + iy);
                cShape.UpdatePos();
                cShape.index = i;
                cShape.mBoard = this;

                if (ix >= (int)boardSize.x - 1)
                {
                    ix = 0;
                    iy--;
                }
                else
                {
                    ix++;
                }
                if(sy + iy < BottomRight().y)
                {
                    break;
                }
            }
        }
    }
    public bool CheckBoardInit()
    {
        for (int i = 0; i < shapeList.Length; i++)
        {
            if(!shapeList[i].GetComponent<Shape>().init_flag)
            {
                return false;
            }
        }
        return true;
    }
    public void AddShape(int i)
    {
        //shapeList[i] = Instantiate(shapeObj);
        //shapeList[i].GetComponent<Shape>().SetShapePos

    }
    /*
     x0123456  7x5
     0#######
     1#######
     2###v###
     3#######
     4#######
     */ 
    public void UpdateAllAdjacent()
    {
        for (int i = 0; i < shapeList.Length; i++)
        {
            if(shapeList[i].GetComponent<Shape>() != null)
            {
                shapeList[i].GetComponent<Shape>().UpdateAdjacent();
            }
        }
    }
    public int GetGridIndex(Vector2 pos)
    {
        int i = (int)(pos.x * pos.y) + (int)pos.x; //7 * 2 + 3 = 17 (width * pos.y + pos.x)
        return i;
    }

    public void Swap(Shape s1, Shape s2)
    {
        Shape t1, t2;

        t1 = new Shape();
        t1.mShape = s1.mShape;
        t1.r = s1.r;
        t1.mEnabled = s1.mEnabled;

        t2 = new Shape();
        t2.mShape = s2.mShape;
        t2.r = s2.r;
        t2.mEnabled = s2.mEnabled;

        s1.mShape = t2.mShape;
        s1.r = t2.r;
        s1.currentSprite = s1.listSprite[t2.r];
        s1.sr.sprite = s1.currentSprite;
        s1.mEnabled = t2.mEnabled;

        s2.mShape = t1.mShape;
        s2.r = t1.r;
        s2.currentSprite = s2.listSprite[t1.r];
        s2.sr.sprite = s2.currentSprite;
        s2.mEnabled = t1.mEnabled;

        s1.Selected = false;
        s2.Selected = false;
    }
    public void DropShape()
    {
        for (int i = 0;  i < boardSize.x;  i++)
        {
            if(shapeList[i].GetComponent<Shape>().mEnabled == false)
            {
                shapeList[i].GetComponent<Shape>().Reroll();
            }
        }
    }
    public void Collapse()
    {
        for (int i = shapeList.Length - 1; i < 0; i--)
        {
            if(shapeList[i].GetComponent<Shape>() == null)
            {
                while(i - (int)boardSize.x >= 0)
                {
                    i = i - (int)boardSize.x;
                    //if(shapeList[i].GetComponent<Shape>())
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(boardSize.x, boardSize.y);
        firstCell = TopLeft();
        shapeList = new GameObject[(int)(boardSize.x * boardSize.y)]; //Create an array of shapes based on board size

        Populate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!boardReady)
        {
            boardReady = CheckBoardInit();
        }
        else if(boardReady)
        {
            if(select1 != null && select2 != null)
            {
                Swap(select1, select2);
                select1 = null;
                select2 = null;
                UpdateAllAdjacent();
            }
            DropShape();
            //UpdateAllAdjacent();
        }
    }
}
