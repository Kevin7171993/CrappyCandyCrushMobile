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
    public Vector2 gridPos;
    public bool Selected = false;
    public Sprite currentSprite;
    public Sprite[] listSprite = new Sprite[(int)Shapes.count];
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
        
    }

    private void OnMouseDown()
    {
        
    }
}
