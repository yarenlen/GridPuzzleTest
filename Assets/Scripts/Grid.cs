using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    public Color visitedTileColor;
    public GameObject basicTile;
    public GameObject player;

    public int rows, cols;

    [HideInInspector]
    public int tileID;
    [HideInInspector]
    public Tile[,] tiles;
    [HideInInspector]
    public List<int> visitedTileIDs;


    // Start is called before the first frame update
    void Start()
    {
        tileID = 0;
        rows = 1;
        cols = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            ColorTileAtPlayerPos();
        }
    }

    public void GenerateGrid(int rowsNo, int colsNo)
    {
        ClearGrid();
        //get number of rows and cols
        rows = rowsNo;
        cols = colsNo;
        //make new tile array with lenght of rows and cols
        tiles = new Tile[rows, cols];
        //start at 0,0,0
        Vector3 tilePos = new Vector3(0, 0, 0);
        //generate each tile
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                tilePos.y = row;
                tilePos.x = col;
                //instantiate tile at given row and col
                GameObject newTile = Instantiate(basicTile, tilePos, Quaternion.identity, GameObject.Find("Grid").transform);
                //get the tile component 
                tiles[row, col] = newTile.GetComponent<Tile>();
                //set tile ID
                tileID++;
                tiles[row, col].ID = tileID;
                //print("generated Tile with ID: " + tiles[row, col].ID);
                
            }
        }
        //reset tileID for next grid generation
        tileID = 0;
    }

    public void ClearGrid()
    {
        if (tiles != null)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Destroy(tiles[row, col].gameObject);
                }
            }
        }
        else
        {
            print("No grid to clear");
        }
    }

    public void SetTileVisited(int tileID)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (tiles[row, col].ID == tileID)
                {
                    VisitTile(row, col);
                    //print("Tile with ID " + tileID + " set to visited.");
                }
            }
        }
    }

    void VisitTile(int row, int col)
    {
        //set tile visited
        tiles[row, col].visited = true;
        //set new color
        tiles[row, col].spriteRenderer.color = visitedTileColor;
        //add to list of visited tiles
        visitedTileIDs.Add(tiles[row, col].ID);
        //print("Tile visited");
    }

    public void ColorTileAtPlayerPos()
    {    
        //check all tiles. if a tiles pos matches the players pos, change its color, mark as visited and check, if all tiles are now markes as visited
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (player.transform.position == tiles[row, col].transform.position)
                {
                    VisitTile(row, col);                   
                    //print("Position Found. Tile colored");
                    WereAllTilesVisited();
                }
            }
        }
    }

    void WereAllTilesVisited()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (tiles[row, col].visited == false)
                {
                    //print("found tile that was not visited.");
                    return;
                }
            }
        }

        //what happens when player finishes a level?
        print("ALL TILES Visited!");
        //load next level
        GetComponent<SaveLoadLevel>().LoadNext();
    }
}
