using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    public GameObject basicTile;
    public int rows;
    public int cols;
 
    public Tile[,] tiles;
    public GameObject player;

    public Color visitedTileColor;

    public GameObject text;

    

    // Start is called before the first frame update
    void Start()
    {
       
        //generate grid
        GenerateGrid(1, 1);
       
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

        rows = rowsNo;
        cols = colsNo;
        tiles = new Tile[rows, cols];
        Vector3 tilePos = new Vector3(0, 0, 0);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                tilePos.y = row;
                tilePos.x = col;
                GameObject newTile = Instantiate(basicTile, tilePos, Quaternion.identity, GameObject.Find("Grid").transform);
                tiles[row, col] = newTile.GetComponent<Tile>();
            }
        }
        
    }

    public void ClearGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Destroy(tiles[row, col].gameObject);
            }
        }
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
                    tiles[row, col].spriteRenderer.color = visitedTileColor;
                    tiles[row, col].visited = true;                    
                    //print("Position Found. Tile colored");
                    WereAllTilesVisited();
                }
            }
        }
    }

    //check if all tiles are marked visited
    void WereAllTilesVisited()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (tiles[row, col].visited == false)
                {
                    print("found tile that was not visited.");
                    return;
                }
            }
        }

        
        text.SetActive(true);
        print("ALL TILES Visited!");
    }
}
