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
}
