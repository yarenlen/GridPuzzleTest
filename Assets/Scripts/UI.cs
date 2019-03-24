using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider rowsSlider, colsSlider;
    public Grid grid;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        rowsSlider.value = grid.rows;
        colsSlider.value = grid.cols;

        rowsSlider.onValueChanged.AddListener(delegate { grid.GenerateGrid(Mathf.RoundToInt(rowsSlider.value), Mathf.RoundToInt(colsSlider.value));});
        colsSlider.onValueChanged.AddListener(delegate { grid.GenerateGrid(Mathf.RoundToInt(rowsSlider.value), Mathf.RoundToInt(colsSlider.value));});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGrid()
    {
        //reset slider
        rowsSlider.value = 0;
        colsSlider.value = 0;
        //reset grid
        grid.GenerateGrid(Mathf.RoundToInt(rowsSlider.value), Mathf.RoundToInt(colsSlider.value));
        player.SetPlayerStartPos(0,0);
    }
}
