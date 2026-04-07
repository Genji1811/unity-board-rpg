using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    void HandleTileEvent()
    {
        Tile tile = tiles[currentTileIndex].GetComponent<Tile>();
        if (tile == null) return;
        Debug.Log("Landed on tile: " + tiles[currentTileIndex].name);
        switch (tile.tileType)
        {
            case TileType.Reward:
                Debug.Log("You got a reward!");
                Debug.Log("Forward 1 tile!");
                Move(1);
                break;
            case TileType.Trap:
                Debug.Log("You hit a trap!");
                Debug.Log("Backward 1 tile!");
                Move(-1);
                break;
            case TileType.Challenge:
                Debug.Log("You face a challenge!");
                break;
            default:
                Debug.Log("Nothing happens.");
                break;
        }
    }
    public Transform[] tiles;
    public int currentTileIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        tiles = FindObjectOfType<BoardManager>().tiles;
        transform.position = tiles[currentTileIndex].position;    
        Debug.Log("Start at: " + tiles[currentTileIndex].name);

    }
    public void Move(int steps)
    {
        StartCoroutine(MoveStepByStep(steps));
    }

    IEnumerator MoveStepByStep(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentTileIndex++;

            if (currentTileIndex >= tiles.Length)
                currentTileIndex = tiles.Length - 1;
            Debug.Log("Moveing to: " + tiles[currentTileIndex].name); //test log
            
            Vector3 target = tiles[currentTileIndex].position;

            // di chuyển mượt
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    5f * Time.deltaTime
                );

                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
        }
        HandleTileEvent();
    }

}
