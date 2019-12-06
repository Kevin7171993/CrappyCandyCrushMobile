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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }
}
