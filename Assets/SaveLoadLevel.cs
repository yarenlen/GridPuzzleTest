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
    public int _gridRows;
    public int _gridCols;
    public int _playerRow;
    public int _playerCol;
    
    public GameObject dropdownmenu;
    public GameObject player;

    void Awake()
    {
        path = Application.dataPath + "/saves";
    }
       
    public void Save()
    {
        //new save Object to store variables
        saveObject saveObject = new saveObject { 
            gridRows = _gridRows, 
            gridCols = _gridCols, 
            playerRow = _playerRow, 
            playerCol = _playerCol, 
        };

        //generate new saveID, that wasn't used before
        while (File.Exists(path + "/save_" + saveID + ".txt"))
        {
            saveID++;
        }
        //convert saveObject to json string 
        //toJson returns a string, we save it in the string called saveString
        string saveString = JsonUtility.ToJson(saveObject);
        print("Saved: "+ saveString);
        //write that string in a txt file 
        File.WriteAllText(path + "/save_" + saveID + ".txt", saveString);
    }

    public void Load()
    {
        //get the value of currently selected option in dropdown
        loadID = dropdownmenu.GetComponent<Dropdown>().value;
        //check if save with that value/ID exists 
        if (File.Exists(path + "/save_" + loadID + ".txt"))
        {
            //Read from save
            string saveString = File.ReadAllText(path + "/save_" + loadID + ".txt");
            //convert from string to saveObject
            saveObject saveObject = JsonUtility.FromJson<saveObject>(saveString);
            print("Laoded" + saveString);

            //assign the variables from the saveObejct to generate the grid
            //to the grid
            GetComponent<Grid>().ClearGrid();
            GetComponent<Grid>().GenerateGrid(saveObject.gridRows, saveObject.gridCols);
            //to the player
            player.GetComponent<PlayerController>().SetPlayerStartPos(saveObject.playerCol, saveObject.playerRow);
            GetComponent<Grid>().ColorTileAtPlayerPos();
        } else {
            Debug.LogWarning("No Save with ID: " + loadID);
        }


    }

    public class saveObject {
        public int gridRows;
        public int gridCols;
        public int playerRow;
        public int playerCol;
    }
}
