using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform[] tiles;

    void Start()
    {
        Debug.Log("Total tiles: " + tiles.Length);
        GenerateBoard();
    }

    public void GenerateBoard()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i].GetComponent<Tile>();
            if (tile == null) continue;

            tile.tileIndex = i;
            tile.tileIndex = 0;

            if (i == 0)
            {
                tile.SetTileType(TileType.None);
                continue;
            }

            int rand = Random.Range(0, 3);

            if (rand == 0)
                tile.SetTileType(TileType.None);
            else if (rand == 1)
                tile.SetTileType(TileType.Reward);
            else
                tile.SetTileType(TileType.Trap);
        }

        SetChallengeTile(5, 1);
        SetChallengeTile(11, 2);
        SetChallengeTile(17, 3);
        SetChallengeTile(22, 4);
        SetChallengeTile(31, 5);

        Debug.Log("Random map generated");
    }

    void SetChallengeTile(int index, int challengeNumber)
    {
        if (index < 0 || index >= tiles.Length)
            return;

        Tile tile = tiles[index].GetComponent<Tile>();

        if (tile != null)
        {
            tile.tileIndex = challengeNumber;
            tile.SetTileType(TileType.Challenge);
        }
    }
}