using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shapes
{
    none = -1,
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
    public bool init_flag = false;
    public bool Selected = false;
    public Sprite currentSprite;
    public Sprite[] listSprite = new Sprite[(int)Shapes.count];
    public int index;
    public Shape up, down, left, right;
    public SpriteRenderer sr;
    private float waitTime = 1.0f;
    private float count = 0.0f;
    public bool mEnabled = true;
    public int r;

    public void Disable()
    {
        mShape = Shapes.none;
        currentSprite = null;
        sr.sprite = currentSprite;
        mEnabled = false;
    }
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
            up.GetComponent<Shape>().Disable();
            down.GetComponent<Shape>().Disable();
            //Destroy(up.gameObject);
            //Destroy(down.gameObject);
        }
        if(horizontal)
        {
            left.GetComponent<Shape>().Disable();
            right.GetComponent<Shape>().Disable();
            //Destroy(left.gameObject);
            //Destroy(right.gameObject);
        }
        if(vertical || horizontal)
        {
            Disable();
            //Destroy(this.gameObject);
        }
    }
    public void Reroll()
    {
        r = Random.Range(0, (int)Shapes.count);
        currentSprite = listSprite[r];
        sr.sprite = currentSprite;
        mShape = (Shapes)r;

        UpdateAdjacent();

        if ((up != null && down != null))
        {
            if (mShape == up.mShape && mShape == down.mShape) //check up and down
            {
                Reroll();
            }
        }
        if ((left != null && right != null))
        {
            if (mShape == left.mShape && mShape == right.mShape)
            {
                Reroll();
            }
        }
        mEnabled = true;
    }
    public void RerollCheck()
    {
        if (!init_flag && (up != null || down != null || left != null || right != null))
        {
            if ((up != null && down != null))
            {
                if (mShape == up.mShape && mShape == down.mShape) //check up and down
                {
                    Reroll();
                    return;
                }
            }
            if ((left != null && right != null))
            {
                if (mShape == left.mShape && mShape == right.mShape)
                {
                    Reroll();
                    return;
                }
            }
            init_flag = true;
        }
    }
    public void CheckBelow()
    {
        if(down != null)
        {
            if(down.GetComponent<Shape>().mEnabled == false)
            {
                mBoard.Swap(this, down.GetComponent<Shape>());
            }
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
        r = Random.Range(0, (int)Shapes.count);
        currentSprite = listSprite[r];
        sr.sprite = currentSprite;
        mShape = (Shapes)r;
    }

    // Update is called once per frame
    void Update()
    {
        if(Selected)
        {
            sr.color = Color.gray;
        }
        else
        {
            sr.color = Color.white;
        }
        if (mBoard.boardReady) //Game loop for when the board is ready
        {
            UpdateAdjacent();
            CheckBelow();
            Match3();
            if (mEnabled == false)
            {
                Disable();
            }
        }
        else
        {
            while (count <= waitTime) //Reroll shapes when board is not ready
            {
                UpdateAdjacent();
                RerollCheck();
                count += 1 * Time.deltaTime;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!Selected)
        {
            if (mBoard.select1 == null)
            {
                mBoard.select1 = this;
                Selected = true;
            }
            else
            {
                if(mBoard.select1 != this)
                {
                    if(mBoard.select1 == this.up || mBoard.select1 == this.down || mBoard.select1 == this.left || mBoard.select1 == this.right)
                    {
                        mBoard.select2 = this;
                        Selected = true;
                    }
                }
            }
        }
        else
        {
            mBoard.select1 = null;
            Selected = false;
        }
    }
}
