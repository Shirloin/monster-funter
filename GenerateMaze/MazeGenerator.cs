using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class MazeGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private GameObject corridorM;
    [SerializeField] private GameObject wallBottom;
    [SerializeField] private GameObject wallTop;                      
    [SerializeField] private GameObject wallLeft;                     
    [SerializeField] private GameObject wallRight;                    
    [SerializeField] private GameObject wallRoomBottom;
    [SerializeField] private GameObject wallRoomTop;
    [SerializeField] private GameObject wallRoomLeft;
    [SerializeField] private GameObject wallRoomRight;
    [SerializeField] private GameObject wallRoomBottonDoor;
    [SerializeField] private GameObject wallRoomTopDoor;
    [SerializeField] private GameObject wallRoomLeftDoor;
    [SerializeField] private GameObject wallRoomRightDoor;
    [SerializeField] private GameObject normalRoom;
    [SerializeField] private GameObject itemRoom1;
    [SerializeField] private GameObject itemRoom2;
    [SerializeField] private GameObject enemyRoom;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private Minimap minimap;
    private GameObject player;

    private List<GameObject> walls;
    private List<GameObject> wallsRoom;
    private List<GameObject> wallsRoomDoor;
    private char[,] map;
    private int[] parent;

    private List<int> sidesX;
    private List<int> sidesY;

    private System.Random rng = new System.Random();
    private bool[,] visited;

    private List<Node> nodeList;
    private List<Edge> edgeList;



    private void instantiateMaze()
    {
        
        walls = new List<GameObject>() { wallBottom, wallRight, wallTop, wallLeft };
        wallsRoom = new List<GameObject>() { wallRoomBottom, wallRoomRight, wallRoomTop, wallRoomLeft };
        wallsRoomDoor = new List<GameObject>() { wallRoomBottonDoor, wallRoomRightDoor, wallRoomTopDoor, wallRoomLeftDoor };
        
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++){
                if (map[i, j] == ' ' || map[i, j] == 'D')
                {
                    Instantiate(corridorM, new Vector3(i * 10, 0f, j * 10), Quaternion.identity);
                    for(int k = 0; k < 4; k++)
                    {
                        if(map[i + sidesX[k], j + sidesY[k]] == '#' || map[i + sidesX[k], j + sidesY[k]] == 'n' ||
                        map[i + sidesX[k], j + sidesY[k]] == 'i' || map[i + sidesX[k], j + sidesY[k]] == 'e' || 
                        map[i + sidesX[k], j + sidesY[k]] == 'b' ||  map[i + sidesX[k], j + sidesY[k]] == 's')
                        {
                            Instantiate(walls[k], new Vector3(i * 10 + sidesX[k] * 10, 0f, j * 10  + sidesY[k] * 10), Quaternion.identity);
                        }
                    }
                }
                

                if (map[i, j] == 'N')
                {
                    instantiateNormalRoom(i, j);
                }
                else if (map[i, j] == 'I')
                {
                    instantiateItemRoom(i, j);
                }
                else if (map[i, j] == 'E')
                {
                    instantiateEnemyRoom(i, j);
                }
                else if (map[i, j] == 'B')
                {
                    instantiateBossRoom(i, j);
                }
                else if(map[i, j] == 'S'){
                    instantiateSpawnRoom(i, j);
                }
            }
        }
    }

    private void instantiateNormalRoom(int i, int j)
    {
        GameObject obj = Instantiate(normalRoom, new Vector3(i * 10, 0f, j * 10), Quaternion.identity);
        obj.name = "NormalRoom";
        int[] x = {1, 0, -1, 0};
        int[] y = {0, 1, 0, -1};
        
        for (int k = 0; k < 4; k++){
            if(map[i + x[k], j + y[k]] == 'D'){
                Instantiate(wallsRoomDoor[k], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
            else if(map[i + x[k], j + y[k]] == 'n'  || map[i + x[k], j + y[k]] == ' '){
                Instantiate(wallsRoom[k], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
        }
    }

    private void instantiateItemRoom(int i, int j)
    {
        int[] x = {2, 0, -1, 0, 2, 1, -1, 1};
        int[] y = {0, 2, 0, -1, 1, 2, 1, -1};

        GameObject obj = Instantiate(itemRoom1, new Vector3(i * 10, 0f, j * 10), Quaternion.identity);
        obj.name = "ItemRoom";
        for(int k=0; k<8; k++){
            if(map[i + x[k], j + y[k]] == 'D'){
                Instantiate(wallsRoomDoor[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
            else if(map[i + x[k], j + y[k]] == 'i'  || map[i + x[k], j + y[k]] == ' '){
                Instantiate(wallsRoom[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
        }
    }

    private void instantiateEnemyRoom(int i, int j)
    {
        int[] x = {3, 0, -1, 0, 
                    3, 1, -1, 1, 
                    3, 2, -1, 2};
        int[] y = {0, 3, 0, -1,
                    1, 3, 1, -1,
                    2, 3, 2, -1};
        GameObject obj = Instantiate(enemy, new Vector3((i+2) * 10, 0f, (j+1) * 10), Quaternion.identity);
        obj = Instantiate(enemy1, new Vector3((i+2) * 10, 0f, (j+2) * 10), Quaternion.identity);

        obj = Instantiate(enemyRoom, new Vector3(i * 10, 0f, j * 10), Quaternion.identity);
        obj.name = "EnemyRoom";
        for(int k=0; k<12; k++){
            if(map[i + x[k], j + y[k]] == 'D'){
                Instantiate(wallsRoomDoor[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
            else if(map[i + x[k], j + y[k]] == 'e'  || map[i + x[k], j + y[k]] == ' '){
                Instantiate(wallsRoom[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
        }
    }

    private void instantiateBossRoom(int i, int j)
    {
        int[] x = {3, 0, -1, 0, 
                    3, 1, -1, 1, 
                    3, 2, -1, 2};
        int[] y = {0, 3, 0, -1,
                    1, 3, 1, -1,
                    2, 3, 2, -1};

        GameObject obj = Instantiate(bossRoom, new Vector3(i * 10, 0f, j * 10), Quaternion.identity);
        obj.name = "BossRoom";
        for(int k=0; k<12; k++){
            if(map[i + x[k], j + y[k]] == 'D'){
                Instantiate(wallsRoomDoor[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
            else if(map[i + x[k], j + y[k]] == 'b' || map[i + x[k], j + y[k]] == ' '){
                Instantiate(wallsRoom[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
        }
    }

    private void instantiateSpawnRoom(int i, int j){
        int[] x = {3, 0, -1, 0, 
                    3, 1, -1, 1, 
                    3, 2, -1, 2};
        int[] y = {0, 3, 0, -1,
                    1, 3, 1, -1,
                    2, 3, 2, -1};

        GameObject obj = Instantiate(spawnRoom, new Vector3(i * 10, 0f, j * 10), Quaternion.identity);
        obj.name = "SpawnRoom";
        for(int k=0; k<12; k++){
            if(map[i + x[k], j + y[k]] == 'D'){
                Instantiate(wallsRoomDoor[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
            else if(map[i + x[k], j + y[k]] == 's' || map[i + x[k], j + y[k]] == ' '){
                Instantiate(wallsRoom[k%4], new Vector3(i * 10 + x[k] * 10, 0f, j * 10 + y[k] * 10), Quaternion.identity);
            }
        }
    }

    private void generateNormalRoom()
    {
        int xRand = 0;
        int yRand = 0;

        bool detect;
        int[] x = {1, 0, 0, -1};
        int[] y = {0, 1, -1, 0};

        //Loop 3 kali karna roomnya ada 3
        for (int i = 0; i < 3; i++)
        {
            //Ngambil posisi buat roomnya
            do
            {
                detect = false;
                xRand = rng.Next(2, sizeX-2);
                yRand = rng.Next(2, sizeY-2);

                if (map[xRand, yRand] != '#')
                {
                    detect = true;
                }
                for(int j=0; j<4; j++){
                    if(map[xRand + x[j], yRand + y[j]] != '#'){
                        detect = true;
                        break;
                    }
                }
            } while (detect);
            //Kalo udah pasti dan ga tabrakan baru kunci roomnya ke maze
            map[xRand, yRand] = 'N';

            for(int j=0; j<4; j++){
                map[xRand + x[j], yRand + y[j]] = 'n';
            }
            //Buat nyari pintu
            if(i==0){
                int k = rng.Next(2, 5);
                for(int j = 0; j<k; j++){
                    int a = rng.Next(4);
                    map[xRand + x[a], yRand + y[a]] = 'D';
                }

            }
            else{
                int k = rng.Next(4);
                map[xRand + x[k], yRand + y[k]] = 'D';
            }
        }
    }

    private void generateItemRoom()
    {
        int xRand = 0;
        int yRand = 0;

        int[] x = {-1, -1, 0, 0, 1, 1, 2, 2};
        int[] y = {0, 1, -1, 2, -1, 2, 0, 1};

        bool detect;
        for (int i = 0; i < 4; i++)
        {
            do{
                detect = false;
                xRand = rng.Next(2, sizeX-3);
                yRand = rng.Next(2, sizeY-3);
                for(int j=0; j<2; j++){
                    for(int k=0; k<2; k++){
                        if(map[xRand + j, yRand + k] != '#'){
                            detect = true;
                            break;
                        }
                    }
                }
                for(int j=0; j<8; j++){
                    if(map[xRand + x[j], yRand + y[j]] != '#'){
                        detect = true;
                        break;
                    }
                }
            }while(detect);
            for(int j=0; j<2; j++){
                for(int k=0; k<2; k++){
                    map[xRand + j, yRand + k] = 'x';
                }
            }
            map[xRand, yRand] = 'I';


            int a = 0;
            for(int j=0; j<8; j++){
                map[xRand + x[a], yRand + y[a]] = 'i';
                a++;
            }

            if(i<2){
                int l = rng.Next(2, 5);
                for(int j=0; j<l; j++){
                    int b = rng.Next(8);
                    map[xRand + x[b], yRand + y[b]] = 'D';
                }
            }
            else{
                int l = rng.Next(8);
                map[xRand + x[l], yRand + y[l]] = 'D';
            }
        }

    }

    private void generateEnemyRoom()
    {
        int xRand = 0;
        int yRand = 0;
        
        int[] x = {3, 0, -1, 0, 
                    3, 1, -1, 1, 
                    3, 2, -1, 2};
        int[] y = {0, 3, 0, -1,
                    1, 3, 1, -1,
                    2, 3, 2, -1};
        

        bool detect;
        for (int i = 0; i < 1; i++)
        {
           do{
                detect = false;
                xRand = rng.Next(2, sizeX-5);
                yRand = rng.Next(2, sizeY-5);
                for(int j=0; j<3; j++){
                    for(int k=0; k<3; k++){
                        if(map[xRand + j, yRand + k] != '#'){
                            detect = true;
                            break;
                        }
                    }
                }
                for(int j=0; j<12; j++){
                    if(map[xRand + x[j], yRand + y[j]] != '#'){
                        detect = true;
                        break;
                    }
                }
            }while(detect);
            for(int j=0; j<3; j++){
                for(int k=0; k<3; k++){
                    map[xRand + j, yRand + k] = 'x';
                }
            }
            map[xRand, yRand] = 'E';
            for(int j=0; j<12; j++){
                map[xRand + x[j], yRand + y[j]] = 'e';
            }
            
            int l = rng.Next(12);
            map[xRand + x[l], yRand + y[l]] = 'D';

        }
    }

    private void generateBossRoom()
    {
        int xRand = 0;
        int yRand = 0;
        
        int[] x = {3, 0, -1, 0, 
                    3, 1, -1, 1, 
                    3, 2, -1, 2};
        int[] y = {0, 3, 0, -1,
                    1, 3, 1, -1,
                    2, 3, 2, -1};
        

        bool detect;
           do{
                detect = false;
                xRand = rng.Next(2, sizeX-5);
                yRand = rng.Next(2, sizeY-5);
                for(int j=0; j<3; j++){
                    for(int k=0; k<3; k++){
                        if(map[xRand + j, yRand + k] != '#'){
                            detect = true;
                            break;
                        }
                    }
                }
                for(int j=0; j<12; j++){
                    if(map[xRand + x[j], yRand + y[j]] != '#'){
                        detect = true;
                        break;
                    }
                }
            }while(detect);
            for(int j=0; j<3; j++){
                for(int k=0; k<3; k++){
                    map[xRand + j, yRand + k] = 'x';
                }
            }
            map[xRand, yRand] = 'B';
            for(int j=0; j<12; j++){
                map[xRand + x[j], yRand + y[j]] = 'b';
            }
            
            int l = rng.Next(12);
            map[xRand + x[l], yRand + y[l]] = 'D';
    }
    
    private void generateSpawnRoom(){
        int xRand = 0;
        int yRand = 0;
        
        int[] x = {3, 0, -1, 0, 
                    3, 1, -1, 1, 
                    3, 2, -1, 2};
        int[] y = {0, 3, 0, -1,
                    1, 3, 1, -1,
                    2, 3, 2, -1};
        

        bool detect;
        do{
            detect = false;
            xRand = rng.Next(2, sizeX-5);
            yRand = rng.Next(2, sizeY-5);
            for(int j=0; j<3; j++){
                for(int k=0; k<3; k++){
                    if(map[xRand + j, yRand + k] != '#'){
                        detect = true;
                        break;
                    }
                }
            }
            for(int j=0; j<12; j++){
                if(map[xRand + x[j], yRand + y[j]] != '#'){
                    detect = true;
                    break;
                }
            }
        }while(detect);
        for(int j=0; j<3; j++){
            for(int k=0; k<3; k++){
                map[xRand + j, yRand + k] = 'x';
            }
        }
        map[xRand, yRand] = 'S';
        for(int j=0; j<12; j++){
            map[xRand + x[j], yRand + y[j]] = 's';
        }
            
        int z = rng.Next(2, 13);
        for(int l=0; l<z; l++){
            int n = 0;
            do{
                n = rng.Next(12);
            }while(map[xRand + x[n], yRand + y[n]] == 'D' || (x[n]==3 && y[n]==1));
            map[xRand + x[n], yRand + y[n]] = 'D';
        }
    }

    public void mazeGenerator()  
    {
        map = new char[sizeX + 1, sizeY + 1];

        sidesX = new List<int> { 1, 0, -1, 0 };
        sidesY = new List<int> { 0, 1, 0, -1 }; 

        for (int i = 0; i < sizeX; i++)
        {
            for(int j = 0; j < sizeY; j++)
            {
                map[i, j] = '#';
            }
        }
        visited = new bool[sizeX, sizeY];
        // generateNormalRoom();
        // generateItemRoom();
        generateEnemyRoom();
        // generateBossRoom();
        generateSpawnRoom();

        MakeNode();
        MakeEdge();
        MakeSet();

        List<Node> connected = new List<Node>();
        List<Edge> graph = new List<Edge>();

        foreach(Edge e in edgeList){
            if(!isSameSet(nodeList.IndexOf(e.Dst), nodeList.IndexOf(e.Src))){
                join(nodeList.IndexOf(e.Dst), nodeList.IndexOf(e.Src));
                graph.Add(e);
            }
        }
        print("Kruskal Done");
        AStar astar = new AStar(sizeX, sizeY);
        foreach (Edge e in graph)
        {
            map = astar.FindPath(e.Src, e.Dst, map);
        }
        print("AStar Done");
    }

    private bool isSameSet(int a, int b){
        return getParent(a) == getParent(b);
    }
    private int getParent(int a){
        if(parent[a] == a){
            return a;
        }
        return parent[a] = getParent(parent[a]);
    }

    private void join(int a, int b){
        int parentA = getParent(a);
        int parentB = getParent(b);
        parent[parentA] = parentB;
    }


    private void MakeNode(){
        nodeList = new List<Node>();
        for(int i=0; i<sizeX; i++){
            for(int j=0; j<sizeY; j++){
                if(map[i, j] == 'D'){
                    nodeList.Add(new Node(i, j));
                }
            }
        }
    }

    private void MakeEdge(){
        edgeList = new List<Edge>();
        for(int i=0; i<nodeList.Count-1; i++){
            for(int j=1+i; j<nodeList.Count; j++){
                if(nodeList[i] != nodeList[j]){
                    //Buat edge baru
                    // print(nodeList[i].X + ", " + nodeList[i].Y + " | " + nodeList[j].X + ", " + nodeList[j].Y);
                    insert(new Edge(nodeList[i], nodeList[j]));
                }
            }
        }
    }

    private void MakeSet(){
        parent = new int[nodeList.Count];
        for(int i=0; i<nodeList.Count; i++){
            parent[i] = i;
        }
    }

    private void insert(Edge edge){
        for(int i=0; i<edgeList.Count; i++){
            if(edgeList[i].W >= edge.W){
                edgeList.Insert(i, edge);
                return;
            }
        }
        edgeList.Insert(edgeList.Count, edge);
    }
    void Start()
    {
        mazeGenerator();
        instantiateMaze();
        player = GameObject.FindGameObjectWithTag("Player");
        minimap = player.GetComponent<Minimap>();
        minimap.MakeMinimap(map);
    }
}