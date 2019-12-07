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

    public int GetGridIndex(Vector2 pos)
    {
        int i = (int)(pos.x * pos.y) + (int)pos.x; //7 * 2 + 3 = 17 (width * pos.y + pos.x)
        return i;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(boardSize.x, boardSize.y);
        firstCell = TopLeft();
        shapeList = new GameObject[(int)(boardSize.x * boardSize.y)]; //Create an array of shapes based on board size

        Populate();
        //bool populated = false;
        //while (!populated)
        //{
        //    Populate();
        //    for (int i = 0; i < shapeList.Length; i++)
        //    {
        //        shapeList[i].GetComponent<Shape>().UpdateAdjacent();
        //    }
        //    for (int i = 0; i < shapeList.Length; i++)
        //    {
        //        shapeList[i].GetComponent<Shape>().Match3();
        //    }
        //    populated = true;
        //    for (int i = 0; i < shapeList.Length; i++)
        //    {
        //        if(shapeList[i] == null)
        //        {
        //            populated = false;
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(!boardReady)
        {
            Populate();
            boardReady = true;
            for (int i = 0; i < shapeList.Length; i++)
            {
                shapeList[i].GetComponent<Shape>().UpdateAdjacent();
            }
            for (int i = 0; i < shapeList.Length; i++)
            {
                shapeList[i].GetComponent<Shape>().Match3();
                Debug.Log("Generated Match3");
            }
            for (int i = 0; i < shapeList.Length; i++)
            {
                if(shapeList[i] == null)
                {
                    boardReady = false;
                }
            }
        }
    }
}
