using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Vector2 boardSize; // 5x5
    public Vector2 cellSize; //1x1
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(boardSize.x, boardSize.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
