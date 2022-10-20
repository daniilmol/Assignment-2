using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    
    [SerializeField] GameObject rayCastPoint;
    private bool visited;
    private int x;
    private int y;

    public GameObject GetRayCastPoint(){
        return rayCastPoint;
    }

    void Start(){
        visited = false;
    }

    public void visit(){
        visited = true;
    }

    public bool isVisited(){
        return visited;
    }

    public int GetX(){
        return x;
    }
    
    public int GetY(){
        return y;
    }

    public void SetCoordinates(int x, int y){
        this.x = x;
        this.y = y;
    }
}
