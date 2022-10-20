using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.AI;

public class MazeGenerator : MonoBehaviour {

    [SerializeField] int size;
    [SerializeField] GameObject cell;
    [SerializeField] GameObject wall;
    int walls = 0;
    Cell[,] cells;
    private int wallCount;
    private float timer = 2f;

    public IEnumerator StartLifetime(Cell curCell)
    {
        yield return new WaitForSeconds(timer);
        DepthFirstSearch(curCell);
    }
    void Start(){
        cells = new Cell[size,size];
        wallCount = size * 2 - 2;
        DrawCells(cells);
        DrawBorders(size);
        DrawWalls(size);
        Cell startingCell = GetStartingPoint();
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
        DepthFirstSearch(startingCell);
        for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                Debug.Log(cells[x,y].isVisited());
            }
        }
        print("After DFS: " + walls);
    }

    private void DrawCells(Cell[,] cell){
        for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                GameObject createdCell = Instantiate(this.cell, new Vector3(x, 0, y), Quaternion.identity);
                createdCell.GetComponent<Cell>().SetCoordinates(x, y);
                cell[x, y] = createdCell.GetComponent<Cell>();
            }
        }
    }

    private void DrawBorders(int cellCount){
        float startingPoint = -0.5f;
        int indexVariable = 0;
        int entrance = Random.Range(1, cellCount);
        int exit = Random.Range(cellCount, cellCount * 2);
        for(int y = 0; y < cellCount * 2; y++){
            if(y < cellCount){
                if (y != entrance) {
                    GameObject wall = Instantiate(this.wall, new Vector3(startingPoint, 1, y), Quaternion.identity);
                }
            }
            else{
                if (y != exit) {
                    GameObject wall = Instantiate(this.wall, new Vector3(startingPoint + size, 1, indexVariable++), Quaternion.identity);
                }
            }
        }
        indexVariable = 0;
        for(float x = 0; x < cellCount * 2; x++){
            if(x < cellCount){
                GameObject wall = Instantiate(this.wall, new Vector3(x, 1, startingPoint), Quaternion.Euler(0, 90f, 0));
            }else{
                GameObject wall = Instantiate(this.wall, new Vector3(indexVariable++, 1, startingPoint + size), Quaternion.Euler(0, 90f, 0));
            }
        }
    }

    private void DrawWalls(int cellCount){
        int walls = 0;
        float startingPoint = 0.5f;
        int indexVariable = 0;
        int passedRows = 0;
        for(int y = indexVariable; passedRows < cellCount - 1; y++){
            if(y < cellCount){
                GameObject wall = Instantiate(this.wall, new Vector3(startingPoint + passedRows, 1, y), Quaternion.identity);
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
                GameObject wall = Instantiate(this.wall, new Vector3(x, 1, startingPoint + passedRows), Quaternion.Euler(0, 90f, 0));
                walls++;
            }else{
                passedRows++;
                x = -1;
            }
        }
        print("Before DFS: " + walls);
    }

    private Cell GetStartingPoint(){
        int startX = Random.Range(0, size);
        int startY = Random.Range(0, size);
        return cells[startX, startY];
    }

    private void DepthFirstSearch(Cell currentCell){
        cells[currentCell.GetX(), currentCell.GetY()].visit();
        Debug.Log("DFSing");
        int[] directions = {0, 1, 2, 3};
        while(directions.Length > 0){
            int directionIndex = Random.Range(0, directions.Length);
            switch(directions[directionIndex]){
                case 0: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetX() - 1 >= 0 && !cells[currentCell.GetX() - 1, currentCell.GetY()].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX() - 1, currentCell.GetY()]);

                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX() - 1, currentCell.GetY()].GetRayCastPoint());
                        //StartCoroutine(StartLifetime(cells[currentCell.GetX() - 1, currentCell.GetY()]));
                    }
                    break;
                case 1: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetX() + 1 < size && !cells[currentCell.GetX() + 1, currentCell.GetY()].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX() + 1, currentCell.GetY()]);

                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX() + 1, currentCell.GetY()].GetRayCastPoint());
                        //StartCoroutine(StartLifetime(cells[currentCell.GetX() + 1, currentCell.GetY()]));

                    }
                    break;
                case 2: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetY() + 1 < size && !cells[currentCell.GetX(), currentCell.GetY() + 1].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX(), currentCell.GetY() + 1]);

                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX(), currentCell.GetY() + 1].GetRayCastPoint());
                                                //StartCoroutine(StartLifetime(cells[currentCell.GetX(), currentCell.GetY() + 1]));

                    }
                    break;
                case 3: 
                    directions = directions.Where((source, index) => index != directionIndex).ToArray();
                    if(currentCell.GetY() - 1 >= 0 && !cells[currentCell.GetX(), currentCell.GetY() - 1].isVisited()){
                        DepthFirstSearch(cells[currentCell.GetX(), currentCell.GetY() - 1]);

                        DestroyWall(currentCell.GetRayCastPoint(), cells[currentCell.GetX(), currentCell.GetY() - 1].GetRayCastPoint());
                                                //StartCoroutine(StartLifetime(cells[currentCell.GetX(), currentCell.GetY() - 1]));

                    }
                    break;
            }
        }
    }

    private void DestroyWall(GameObject currentPoint, GameObject targetPoint) {
        RaycastHit rayHit;
        Vector3 dir = targetPoint.transform.position - currentPoint.transform.position;
        Ray ray = new Ray(currentPoint.transform.position, (targetPoint.transform.position - currentPoint.transform.position).normalized * 10);
        if (Physics.Raycast(currentPoint.transform.position, dir, out rayHit, 5))
        {
            if (rayHit.collider.gameObject.tag == "Wall")
            {
                walls--;
                Destroy(rayHit.collider.gameObject);
            }
        }
    }
}
