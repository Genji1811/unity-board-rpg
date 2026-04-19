using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public int maxHP = 5;
    public int currentHP = 5;
    public int baseAP = 5;
    public int currentAP = 5;
    public int maxCard = 5;
    public bool isMoving = false;
    // combat temp
[HideInInspector] public int tempAP = 0;
[HideInInspector] public bool hasTempShield = false;
[HideInInspector] public bool usedCardThisCombat = false;
    public TileEventHandler eventHandler;

    public Transform[] tiles;
    public int currentTileIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        tiles = FindObjectOfType<BoardManager>().tiles;
        transform.position = tiles[currentTileIndex].position;    
        Debug.Log("Start at: " + tiles[currentTileIndex].name);

    }
    public IEnumerator DelayMove(int steps)
    {
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(MoveStepByStep(steps));
    }
    public void Move(int steps)
    {
        if (isMoving)
        {
            Debug.Log("Already moving, wait...");
            return;
        }
        StartCoroutine(MoveStepByStep(steps));
    }
    IEnumerator MoveStepByStep(int steps)
    {
        isMoving = true;
        int direction = steps >= 0 ? 1 : -1;

        for (int i = 0; i < Mathf.Abs(steps); i++)
        {
            int nextIndex = currentTileIndex + direction;
            if (nextIndex < 0 || nextIndex >= tiles.Length)
            {
                Debug.Log("Can't move further");
                break;
            }
            currentTileIndex = nextIndex;
            
            Debug.Log("Moving to: " + tiles[currentTileIndex].name);

            Vector3 target = tiles[currentTileIndex].position;

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
            Tile tile = tiles[currentTileIndex].GetComponent<Tile>();
            if (tile != null && tile.tileType == TileType.Challenge)
            {
                yield return StartCoroutine(HandleTileEvent());

                isMoving = false;
                yield break;
            }
        }
        // 👉 gọi event sau khi move xong
        yield return StartCoroutine(HandleTileEvent());
    }
    
    IEnumerator HandleTileEvent()
    {
        Tile tile = tiles[currentTileIndex].GetComponent<Tile>();
        if (eventHandler == null)
        {
            Debug.LogError("EventHandler NULL");
            EndTurn(); // 🔥 tránh kẹt
            yield break;
        }
        if (tile == null)
        {
            Debug.Log("Tile NULL");
            EndTurn();
            yield break;
        }
        yield return StartCoroutine(eventHandler.HandleEvent(tile));
    }
    public int turnCount = 0;

    public void EndTurn()
    {
        isMoving = false;
        turnCount++;
        Debug.Log("Turn End: " + turnCount);
    }

    public void ResetCombatState()
    {
        tempAP = 0;
        hasTempShield = false;
        usedCardThisCombat = false;
    }
    public void UseCard(Card card)
    {
        if (usedCardThisCombat)
        {
            Debug.Log("Already used card this combat!");
            return;
        }

        switch (card.type)
        {
            case CardType.Attack:
                tempAP += card.value;
                Debug.Log("Use ATK +" + card.value);
                break;

            case CardType.Defense:
                hasTempShield = true;
                Debug.Log("Use DEF (block 1 hit)");
                break;

            case CardType.Special:
                currentHP += card.value;
                Debug.Log("Heal +" + card.value);
                break;
        }

        deck.Remove(card);
        usedCardThisCombat = true;
    }
    public List<Card> deck = new List<Card>();

    public void AddCard(Card card)
    {
        if (deck.Count >= maxCard)
        {
            Debug.Log("Deck full → remove oldest card");

            deck.RemoveAt(0); // xóa card đầu
        }

        deck.Add(card);
        Debug.Log("Add card: " + card.cardName);
        Debug.Log("Deck size: " + deck.Count + "/" + maxCard);
    }
    public Card GenerateRandomCard()
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
        {
            return new Card
            {
                cardName = "ATK+1",
                description = "increase AP by 1",
                type = CardType.Attack,
                value = 1
            };
        }
        else if (rand == 1)
        {
            return new Card
            {
                cardName = "DEF",
                description = "Block 1 hit",
                type = CardType.Defense,
                value = 0
            };
        }
        else
        {
            return new Card
            {
                cardName = "HEAL+1",
                description = "increase HP by 1",
                type = CardType.Special,
                value = 1
            };
        }
    }
    public void RemoveRandomCard()
    {
        if (deck.Count == 0)
        {
            Debug.Log("No card to remove");
            return;
        }

        int index = Random.Range(0, deck.Count);
        Debug.Log("Remove card: " + deck[index].cardName);
        deck.RemoveAt(index);
    }
}

