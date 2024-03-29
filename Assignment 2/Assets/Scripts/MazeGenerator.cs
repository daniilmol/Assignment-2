using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
// using UnityEditor.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour {
    /**
    * Size of width and length
    */
    [SerializeField] int size;
    /**
    * Start point location for z value
    */
    [SerializeField] int startPointZ;
    /**
    * Floor prefab
    */
    [SerializeField] GameObject cell;
    /**
    * Horizontal wall prefab made up of quads
    */
    [SerializeField] GameObject horizontalWall;
    /**
    * Vertical wall prefab made up of quads
    */
    [SerializeField] GameObject verticalWall;

    [SerializeField] GameObject pongWall;
    /**
    * Player prefab
    */
    [SerializeField] GameObject player;
    /**
    * Enemy prefab
    */
    [SerializeField] GameObject enemy;
    /**
    * Win trigger prefab
    */
    [SerializeField] GameObject trigger;
    [SerializeField] GameObject[] dontDestroyObjects;
    /**
    * 2D Array of cells to represent each one by their x and z positions
    */
    public static Cell[,] cells;
    /**
    * Enemy prefab spawn position
    */
    private int cellX;
    private int cellY;
    /**
    * Player prefab scale (only get height for y)
    */
    private Vector3 playerScale;
    /**
    * Exit cell index
    */
    private int exitIndex;

    /**
    * Initializes 2D array, fills them, instantiates the cell prefabs
    * Draws the surrounding borders, draws walls on every cell's edge
    * Begins DFS on a random point, builds a nav mesh for enemy and spawns enemy
    */
    void Start(){
        if (!DontDestroyOnLoadManager.CheckDontDestoryExist())
        {
            cells = new Cell[size,size];
            DrawCells(cells); 
            DrawWalls(size);
            DrawBorders(size);
            Cell startingCell = GetStartingPoint();
            DepthFirstSearch(startingCell);
            NavMeshSurface nms = GameObject.Find("NavMeshBuilder").GetComponent<NavMeshSurface>();
            nms.buildHeightMesh = true;
            nms.BuildNavMesh();
            print("PRINTED");
            playerScale = player.transform.localScale;
            playerScale.x = 0;
            playerScale.z = 0;
            DontDestroyOnLoadManager.DontDestroyOnLoad(Instantiate(player, cells[0, startPointZ].transform.position + playerScale, Quaternion.identity));
            SpawnEnemy();
            DontDestroyOnLoadManager.DontDestroyOnLoad(Instantiate(trigger, cells[size - 1, exitIndex].transform.position, Quaternion.identity));
            SpawnPongGameDoor();
            // DontDestroyOnLoad(gameObject);
            // Instantiate(gameObject);
            DontDestroyOnLoadManager.DontDestroyOnLoad(gameObject);
            foreach (var obj in dontDestroyObjects)
            {
                DontDestroyOnLoadManager.DontDestroyOnLoad(obj);
            }
        }
        else
        {
            DontDestroyOnLoadManager.EnableAll();
        }
    }

    private void SpawnPongGameDoor(){
        var pos = cells[size / 2, size / 2].transform.position;
        RaycastHit hit;

        if (Physics.Raycast(pos + new Vector3(0, 1, 0), Vector3.right, out hit, 100.0f))
        {
            DontDestroyOnLoadManager.DontDestroyOnLoad(Instantiate(pongWall, hit.transform.position, Quaternion.identity));
            GameObject.Destroy(hit.collider.gameObject);
        }
    }

    /**
    * Spawns enemy in a random position
    */
    private void SpawnEnemy() {
        cellX = Random.Range(0, size);
        cellY = Random.Range(0, size);
        DontDestroyOnLoadManager.DontDestroyOnLoad(Instantiate(enemy, cells[cellX, cellY].transform.position, Quaternion.identity));
    }

    /**
    * Fills 2D array of cells and instantiates the cells
    */
    private void DrawCells(Cell[,] cell){
        for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                GameObject createdCell = Instantiate(this.cell, new Vector3(x, 0, y), Quaternion.identity);
                createdCell.transform.parent = gameObject.transform;
                createdCell.GetComponent<Cell>().SetCoordinates(x, y);
                cell[x, y] = createdCell.GetComponent<Cell>();
            }
        }
    }

    /**
    * Instantiates surrounding maze borders with one entrance and exit
    */
    private void DrawBorders(int cellCount){
        float startingPoint = -0.5f;
        int indexVariable = 0;
        int entrance = Random.Range(1, cellCount - 1);
        int exit = Random.Range(1, cellCount - 1);
        exitIndex = exit;
        for(int y = 0; y < cellCount * 2; y++){
            if(y < cellCount){
                if (y != entrance) {
                    GameObject wall = Instantiate(this.verticalWall, new Vector3(startingPoint, 1, y), Quaternion.identity);
                    wall.transform.parent = gameObject.transform;
                }else{
                    startPointZ = entrance;
                }
            }
            else{
                print(indexVariable);
                if (indexVariable != exit)
                {
                    GameObject wall = Instantiate(this.verticalWall, new Vector3(startingPoint + size, 1, indexVariable++), Quaternion.identity);
                    wall.transform.parent = gameObject.transform;
                }
                else {
                    indexVariable++;
                }
            }
        }
        indexVariable = 0;
        for(float x = 0; x < cellCount * 2; x++){
            if(x < cellCount){
                GameObject wall = Instantiate(this.horizontalWall, new Vector3(x, 1, startingPoint), Quaternion.Euler(0, 90f, 0));
                wall.transform.parent = gameObject.transform;
            }else{
                GameObject wall = Instantiate(this.horizontalWall, new Vector3(indexVariable++, 1, startingPoint + size), Quaternion.Euler(0, 90f, 0));
                wall.transform.parent = gameObject.transform;
            }
        }
    }

    /**
    * Instantiates all walls at the edges of every cell
    */
    private void DrawWalls(int cellCount){
        int walls = 0;
        float startingPoint = 0.5f;
        int indexVariable = 0;
        int passedRows = 0;
        for(int y = indexVariable; passedRows < cellCount - 1; y++){
            if(y < cellCount){
                GameObject wall = Instantiate(this.verticalWall, new Vector3(startingPoint + passedRows, 1, y), Quaternion.identity);
                wall.transform.parent = gameObject.transform;
                walls++;
            }else{
                passedRows++;
                y = -1;
            }
        }
        indexVariable = 0;
        passedRows = 0;
        for(float x = indexVariable; passedRows < cellCount - 1; x++){
            if(x < cellCount){
                GameObject wall = Instantiate(this.horizontalWall, new Vector3(x, 1, startingPoint + passedRows), Quaternion.Euler(0, 90f, 0));
                wall.transform.parent = gameObject.transform;
                walls++;
            }else{
                passedRows++;
                x = -1;
            }
        }
    }

    /**
    * Returns a random cell from the 2D array
    */
    private Cell GetStartingPoint(){
        int startX = Random.Range(0, size);
        int startY = Random.Range(0, size);
        return cells[startX, startY];
    }

    /**
    * Recursive DFS Algorithm to visit unvisited cells, destroying the walls
    * between them, setting the newly visited cell as the current cell
    * backtracks when no available neighbors left to visit
    */
    private void DepthFirstSearch(Cell currentCell){
        cells[currentCell.GetX(), currentCell.GetY()].visit();
        int[] directions = {0, 1, 2, 3};
        while(directions.Length > 0){
            int directionIndex = Random.Range(0, directions.Length);
            switch(directions[directionIndex]){
                case 0: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetX() - 1 >= 0 && !cells[currentCell.GetX() - 1, currentCell.GetY()].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX() - 1, currentCell.GetY()]);
                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX() - 1, currentCell.GetY()].GetRayCastPoint());
                    }
                    break;
                case 1: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetX() + 1 < size && !cells[currentCell.GetX() + 1, currentCell.GetY()].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX() + 1, currentCell.GetY()]);
                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX() + 1, currentCell.GetY()].GetRayCastPoint());
                    }
                    break;
                case 2: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetY() + 1 < size && !cells[currentCell.GetX(), currentCell.GetY() + 1].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX(), currentCell.GetY() + 1]);
                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX(), currentCell.GetY() + 1].GetRayCastPoint());
                    }
                    break;
                case 3: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetY() - 1 >= 0 && !cells[currentCell.GetX(), currentCell.GetY() - 1].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX(), currentCell.GetY() - 1]);
                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX(), currentCell.GetY() - 1].GetRayCastPoint());

                    }
                    break;
            }
        }
    }

    /**
    * Destroys the wall between the current cell and the visited neighbor cell
    */
    private void DestroyWall(GameObject currentPoint, GameObject targetPoint) {
        RaycastHit rayHit;
        Vector3 dir = targetPoint.transform.position - currentPoint.transform.position;
        Ray ray = new Ray(currentPoint.transform.position, (targetPoint.transform.position - currentPoint.transform.position).normalized * 10);
        if (Physics.Raycast(currentPoint.transform.position, dir, out rayHit, 5))
        {
            if (rayHit.collider.gameObject.tag == "Wall")
            {
                Destroy(rayHit.collider.gameObject);
            }
        }
    }

    public void RespawnEnemy(){
        cellX = Random.Range(0, size);
        cellY = Random.Range(0, size);
        StartCoroutine(RespawnCooldown());
    }

    private IEnumerator RespawnCooldown(){
        yield return new WaitForSeconds(5);
        DontDestroyOnLoadManager.DontDestroyOnLoad(Instantiate(enemy, cells[cellX, cellY].transform.position, Quaternion.identity));
    }
}
