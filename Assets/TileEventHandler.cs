using UnityEngine;
using System.Collections;

public class TileEventHandler : MonoBehaviour
{
    public PlayerController player;

    public IEnumerator HandleEvent(Tile tile)
    {
        switch (tile.tileType)
        {
            case TileType.Reward:
                yield return HandleReward();
                break;

            case TileType.Trap:
                yield return HandleTrap();
                break;

            case TileType.Challenge:
                yield return HandleChallenge(tile);
                break;

            case TileType.None:
                UIManager.instance.ShowMessage("Normal tile");
                player.EndTurn();
                break;
            default:
                UIManager.instance.ShowMessage("Unknown tile type");
                player.EndTurn();
                break;
        }
    }
    
    IEnumerator HandleReward()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            int step = Random.Range(1, 3);
            UIManager.instance.ShowMessage("Reward: Move +" + step);
            yield return player.StartCoroutine(player.DelayMove(step));
        }
        else
        {
            UIManager.instance.ShowMessage("Reward: Get card");
            player.AddCard(player.GenerateRandomCard());
            player.EndTurn();
            yield return null;
        }
    }

    IEnumerator HandleTrap()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            UIManager.instance.ShowMessage("Trap: Move -1");
            yield return player.StartCoroutine(player.DelayMove(-1));
        }
        else
        {   
            UIManager.instance.ShowMessage("Trap: Lose card");
            player.RemoveRandomCard();
            player.EndTurn();
            yield return null;

        }
    }
    public CombatUI combatUI;
    IEnumerator HandleChallenge(Tile tile)
    {
        int enemyHP = GetEnemyHP(tile.tileIndex);
        bool finished = false;

        combatUI.Show(enemyHP, () =>
        {
            StartCoroutine(DoFight(tile, enemyHP));
            finished = true;
        });

    yield return new WaitUntil(() => finished);
    }
    IEnumerator DoFight(Tile tile, int enemyHP)
    {
        int dice = Random.Range(1, 5);
        int finalAP = player.currentAP + player.tempAP;
        int damage = dice * finalAP;

        combatUI.UpdateCombat(dice, finalAP);

        yield return new WaitForSeconds(0.5f);

        bool win = damage > enemyHP;

        combatUI.ShowResult(win);

        yield return new WaitForSeconds(1f);

        if (win)
        {
            int reward = GetRewardAP(tile.tileIndex);
            player.currentAP += reward;
            UIManager.instance.ShowMessage("You won! AP +" + reward);
        }
        else
        {
            if (player.hasTempShield)            {
                player.hasTempShield = false;
                player.currentTileIndex = 0;
                player.transform.position = player.tiles[0].position;
                UIManager.instance.ShowMessage("You lost! back to start but shield protected you");
            }
            else
            {
                UIManager.instance.ShowMessage("You lost! HP -1");
                player.currentTileIndex = 0;
                player.currentHP -= 1;
                player.transform.position = player.tiles[0].position;
            }
        }

        yield return new WaitForSeconds(0.5f);
        combatUI.Hide();        
        player.ResetCombatState();
        player.EndTurn();
    }
    int GetEnemyHP(int index)
    {
        switch (index)
        {
            case 1: return 5;
            case 2: return 10;
            case 3: return 15;
            case 4: return 20;
            case 5: return 30;
            default: return 5;
        }
    }

    int GetRewardAP(int index)
    {
        switch (index)
        {
            case 1: return 1;
            case 2: return 1;
            case 3: return 2;
            case 4: return 2;
            case 5: 
                UIManager.instance.ShowMessage("You win in " + player.turnCount + " turns!");
                return 0;
            default: return 1;
        }
    }
}

