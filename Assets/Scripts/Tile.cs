using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool visited;
    public int ID;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        visited = false;
    }
}
