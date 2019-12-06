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
                shapeList[i].GetComponent<Shape>().SetShapePos(sx + ix, sy + iy);
                shapeList[i].GetComponent<Shape>().UpdatePos();
                if (ix >= (int)boardSize.x - 1)
                {
                    ix = 0;
                    iy--;
                }
                else
                {
                    ix++;
                }
                if(iy <= BottomRight().y)
                {
                    break;
                }
            }
        }
    }

    public void AddShape(Vector2 pos)
    {
        

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
        //shapeList[0] = Instantiate(shapeObj);
        //shapeList[0].GetComponent<Shape>().SetShapePos((int)TopLeft().x, (int)TopLeft().y);
        //shapeList[0].GetComponent<Shape>().UpdatePos();
        Populate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
