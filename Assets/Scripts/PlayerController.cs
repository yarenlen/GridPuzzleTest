using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int row;
    int col;
    //public int startPlayerRow;
    //public int startPlayerCol;
    int rows; // no. of rows in grid
    int cols; // no. of cols in grid

    Vector3 newPlayerPos;
    public GameObject myGrid;
   
    int currentPlayerRow;
    int currentPlayerCol;
    

    // Start is called before the first frame update
    void Start()
    {
     
    }

    public void SetPlayerStartPos(int startPlayerCol, int startPlayerRow)
    {
        col = startPlayerCol;
        row = startPlayerRow;
        //set start position of player
        this.transform.position = new Vector3(startPlayerCol, startPlayerRow, 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerRow = row;
        currentPlayerCol = col;
        rows = myGrid.GetComponent<Grid>().rows;
        cols = myGrid.GetComponent<Grid>().cols;

        if (Input.GetKeyDown(KeyCode.W) && row < rows -1)
        {
            //print("move one row up");
            row = row + 1;            
        }
        else if (Input.GetKeyDown(KeyCode.A) && col > 0)
        {
            //print("move one col left");
            col = col - 1;
        }
        else if (Input.GetKeyDown(KeyCode.S) && row > 0)
        {
            //print("move one row down");
            row = row - 1;
        }
        else if (Input.GetKeyDown(KeyCode.D) && col < cols - 1)
        {
            //print("move one col right");
            col = col + 1;
        }


      //  check if the tile at new col/ row was visited; if yes keep current position and current row/ col; if not set position to the new row/ col
        if (Input.anyKeyDown)
        {
            if (myGrid.GetComponent<Grid>().tiles[row, col].visited == true)
            {
                newPlayerPos = new Vector3(currentPlayerCol, currentPlayerRow, 0);
                row = currentPlayerRow;
                col = currentPlayerCol;
                print("movement denied. tile already visited");
            }
            else
            {
                newPlayerPos = new Vector3(col, row, 0);
            }
            gameObject.transform.position = newPlayerPos;
            //print("Player Pos: " + newPlayerPos);
        }

    }
}
