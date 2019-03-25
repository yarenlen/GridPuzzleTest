using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveLoadLevel : MonoBehaviour
{
    //variables for Load and Save
    string path;
    int saveID = 0;
    int loadID = 0;
    //variables to store in save
    public int _playerRow, _playerCol;
    List<int> _visitedTileIDs;
    
    public Dropdown dropdownmenu;
    public GameObject player;

    void Awake()
    {
        //path were files are saved
        path = Application.dataPath + "/levels";

        //check if directory exists, if not create it
        if (Directory.Exists(path))
        {
            return;
        } else { 
            var folder = Directory.CreateDirectory(path); // returns a DirectoryInfo object
        }
    }
       
    public void Save()
    {
        //find the current active grid and the player
        Grid currentGrid = GameObject.Find("Grid").GetComponent<Grid>();

        //new save Object to store variables
        saveObject saveObject = new saveObject {
            gridRows = currentGrid.rows,
            gridCols = currentGrid.cols,
            playerRow = player.GetComponent<PlayerController>().row,
            playerCol = player.GetComponent<PlayerController>().col,
            visitedTileIDs = currentGrid.visitedTileIDs,                   
        };

        //generate new saveID, that wasn't used before
        while (File.Exists(path + "/level_" + saveID + ".txt"))
        {
            saveID++;
        }
       
        //convert saveObject to json string 
        //toJson returns a string, we save it in the string called saveString
        string saveString = JsonUtility.ToJson(saveObject);
        print("Saved: "+ saveString);
        //write that string in a txt file 
        File.WriteAllText(path + "/level_" + saveID + ".txt", saveString);
    }

    public void Load()
    {
        //get the value of currently selected option in dropdown
        loadID = dropdownmenu.value;
        //check if save with that value/ID exists 
        if (File.Exists(path + "/level_" + loadID + ".txt"))
        {
            //Read from save
            string saveString = File.ReadAllText(path + "/level_" + loadID + ".txt");
            //convert from string to saveObject
            saveObject saveObject = JsonUtility.FromJson<saveObject>(saveString);
            print("Loaded Level_" + loadID + ": "  + saveString);

            //assign the variables from the saveObejct to generate the grid
            Grid myGrid = GetComponent<Grid>();
            //make grid
            //myGrid.ClearGrid();
            myGrid.GenerateGrid(saveObject.gridRows, saveObject.gridCols);
            //set visited tiles
            foreach (int tileID in saveObject.visitedTileIDs)
            {
                myGrid.SetTileVisited(tileID);
            }
            //set player
            player.GetComponent<PlayerController>().SetPlayerStartPos(saveObject.playerCol, saveObject.playerRow);
            myGrid.ColorTileAtPlayerPos();
        } else {
            Debug.LogWarning("No Level with ID: " + loadID);
        }
    }

    public void LoadNext()
    {
        loadID++;
        //get the number of options in the dropdown 
        int options = dropdownmenu.options.Count;
        //if loadId is smaller than number of options load next.
        if (loadID < options)
        {
            dropdownmenu.value = loadID;
            Load();
        }
        else //finish game
        {
            Debug.LogWarning("Juhu! Last Level FINISHED");
            player.SetActive(false);
        }


    }

    //contains all variables we want to store 
    public class saveObject {
        public int gridRows;
        public int gridCols;
        public int playerRow;
        public int playerCol;
        public List<int> visitedTileIDs;
    }
}
