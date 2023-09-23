using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Edge {
    private Node src;
	private Node dst;

	private float w;
	public Edge(Node src, Node dst)
	{
		this.src = src;
		this.dst = dst;

		this.w = Mathf.Sqrt(Mathf.Pow((src.X - dst.X), 2) + Mathf.Pow((src.Y - dst.Y), 2));
	}

	public Node Src
    {
		get { return src; }
    }

	public Node Dst
    {
        get { return dst; }
    }

	public float W
    {
		get { return w; }
    }
}