using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    /**
    * Raycast Point prefab used to destroy walls
    */
    [SerializeField] GameObject rayCastPoint;
    /**
    * Tells whether the cell has been visited or not
    */
    private bool visited;
    /**
    * Cell's x coordinate
    */
    private int x;
    /**
    * Cell's y coordinate, (z coordinate in world space)
    */
    private int y;

    /**
    * Returns the cell's raycast point
    */
    public GameObject GetRayCastPoint(){
        return rayCastPoint;
    }

    /**
    * Ensures a newly created cell is not visited
    */
    void Start(){
        visited = false;
    }

    /**
    * Visits the cell
    */
    public void visit(){
        visited = true;
    }

    /**
    * Checks whether the cell is visited or not
    */
    public bool isVisited(){
        return visited;
    }

    /**
    * Returns the cell's x coordinate
    */
    public int GetX(){
        return x;
    }
    
    /**
    * Returns the cell's y coordinate (z coordinate in world space)
    */
    public int GetY(){
        return y;
    }

    /**
    * Sets the cell's coordinates for the 2D array
    */
    public void SetCoordinates(int x, int y){
        this.x = x;
        this.y = y;
    }
}
