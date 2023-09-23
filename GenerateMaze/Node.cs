using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // Start is called before the first frame update

    private int x;
    private int y;
    private Node prev;

    private float heuristic;
    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public int X
    {
        get { return x; }
        set { x = value; }
    } 

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public float Heuristic
    {
        get { return heuristic; }
    }

    public Node Prev
    {
        get { return prev;  }
        set { prev = value; }
    }


    public void SetHeuristic(int targetX, int targetY)
    {
        heuristic = Mathf.Sqrt(Mathf.Pow(x - targetX, 2) + Mathf.Pow(y - targetY, 2));
        
    }
}