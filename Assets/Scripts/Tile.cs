using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool visited;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        visited = false;
    }
}
