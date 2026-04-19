using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None,
    Reward,
    Trap,
    Challenge
    
}

public class Tile : MonoBehaviour
{
    public int tileID;
    public bool isOccupied;
    public TileType tileType;
    public int tileIndex;
    void Start()
{
    UpdateColor();
}

void UpdateColor()
{
    SpriteRenderer sr = GetComponent<SpriteRenderer>();

    switch (tileType)
    {
        case TileType.Reward:
            sr.color = Color.yellow;
            break;
        case TileType.Trap:
            sr.color = Color.magenta;
            break;
        case TileType.Challenge:
            sr.color = Color.red;
            break;
        default:
            sr.color = Color.white;
            break;
    }
}
}

