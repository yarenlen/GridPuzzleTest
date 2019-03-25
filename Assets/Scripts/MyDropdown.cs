using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MyDropdown : MonoBehaviour
{
    //List of Dropdown options
    List<string> dropOptions = new List<string>();
    //This is the Dropdown
    Dropdown dropdown;
    int saveID = 0;


    private void Start()
    {
        UpdateDropdown();
    }

    public void UpdateDropdown()
    {
        while (File.Exists(Application.dataPath + "/levels/level_" + saveID + ".txt"))
        {
            dropOptions.Add("Level_" + saveID); 
            saveID++;
        }

        dropdown = GetComponent<Dropdown>();
        //Clear the old options of the Dropdown menu
        if (dropdown.options != null) { dropdown.ClearOptions(); }
        //Add the options created in the List above
        dropdown.AddOptions(dropOptions);
        //print("dropdown updated");
    }
}
