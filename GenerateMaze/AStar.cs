using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    // Start is called before the first frame update

    private int sizeX;
    private int sizeY;

    private int counter;

    private List<Node> queue;
    public AStar(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
    }

    private char[,] BackTracking(Node node, char[,] map){
        Node now = node;
        do{
            // if(now==null){
            //     break;
            // }
            if(map[now.X, now.Y] != 'D'){
                map[now.X, now.Y] = ' ';
            }
            now = now.Prev;
        }while(now!=null);
        return map;
    }

    private char[,] CopyMap(char[,] map){
        char[,] tmpMap = new char[sizeX, sizeY];
        for(int i=0; i<sizeX; i++){
            for(int j=0;j<sizeY; j++){
                tmpMap[i, j] = map[i, j];
            }
        }
        return tmpMap;
    }

    private void AddNode(Node node){
        if(queue.Contains(node)){
            return;
        }
        for(int i=0; i<queue.Count; i++){
            if(queue[i].Heuristic > node.Heuristic){
                queue.Insert(i, node);
                return;
            }
        }
        queue.Add(node);
    }

    public char[,] FindPath(Node start, Node end, char[,] map){
        int[] x = { 0, 1, 0, -1 };
        int[] y = { 1, 0, -1, 0 };
        char[,] tmpMap = CopyMap(map);

        queue = new List<Node>();

        // AddNode(start);
        queue.Add(start);
        Node now = null;
        while(queue.Count>0){

            int resIdx = 0;
            for(int i=0; i<queue.Count; i++){
                if(queue[resIdx].Heuristic > queue[i].Heuristic){
                    resIdx = i;
                }
            }
            now = queue[resIdx];
            queue.RemoveAt(resIdx);
            tmpMap[now.X, now.Y] = 'X';
            if(now.X == end.X && now.Y == end.Y){
                break;
            }
            for(int i=0; i<4; i++){
                Node node = new Node(now.X + x[i], now.Y + y[i]);
                node.SetHeuristic(end.X, end.Y);
                node.Prev = now;

                if(node.X <= 0 || node.Y <=0 || node.X >= sizeX-1 || node.Y >= sizeY-1){
                    continue;
                }
                if(tmpMap[node.X, node.Y] == '#' || tmpMap[node.X, node.Y] == 'D' || tmpMap[node.X, node.Y] == ' ' || 
                tmpMap[node.X, node.Y] == 'n' || tmpMap[node.X, node.Y] == 'e' || tmpMap[node.X, node.Y] == 's' ||
                tmpMap[node.X, node.Y] == 'b' || tmpMap[node.X, node.Y] == 'i'){
                    queue.Add(node);
                }
                // if(now.X + x[i] <= 0 || now.Y + y[i] <= 0 || now.X + x[i] >= sizeX || now.Y + y[i] >= sizeY){
                //     continue;
                // }
                // if( tmpMap[now.X + x[i], now.Y + y[i]] == '#' || tmpMap[now.X + x[i], now.Y + y[i]] == 'D' || tmpMap[now.X + x[i], now.Y + y[i]] == ' '){
                //     AddNode(node);
                // }
            }
        }
        return BackTracking(now, map);
    }
}