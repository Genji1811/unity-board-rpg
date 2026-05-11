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
    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite rewardSprite;
    public Sprite trapSprite;
    public Sprite challengeSprite;

    private SpriteRenderer sr;
 void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        UpdateTileVisual();
    }

    void UpdateTileVisual()
    {
        switch (tileType)
        {
            case TileType.Reward:
                sr.sprite = rewardSprite;
                break;

            case TileType.Trap:
                sr.sprite = trapSprite;
                break;

            case TileType.Challenge:
                sr.sprite = challengeSprite;
                break;

            default:
                sr.sprite = normalSprite;
                break;
        }
    }

}

