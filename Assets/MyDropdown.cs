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

    void Start()
    {
        while (File.Exists(Application.dataPath + "/saves/save_" + saveID + ".txt"))
        {
            dropOptions.Add("save_" + saveID); 
            saveID++;
        }

        dropdown = GetComponent<Dropdown>();
        //Clear the old options of the Dropdown menu
        dropdown.ClearOptions();
        //Add the options created in the List above
        dropdown.AddOptions(dropOptions);
    }
}
