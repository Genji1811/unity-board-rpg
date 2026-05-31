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

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        UpdateTileVisual();
    }

    public void SetTileType(TileType newType)
    {
        tileType = newType;
        UpdateTileVisual();
    }

    public void UpdateTileVisual()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError(name + " missing SpriteRenderer");
            return;
        }

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