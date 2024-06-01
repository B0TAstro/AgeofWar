using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerData[] players;
    public AgeData[] agesData;

    public Text gameOverText;

    void Start()
    {
        foreach (var player in players)
        {
            player.UpdateGoldText();
            player.UpdateXPText();
            player.UpdateAgeText();
        }
        gameOverText.gameObject.SetActive(false);
    }

    public void AddGold(int playerId, int amount)
    {
        players[playerId].playerGold += amount;
        players[playerId].UpdateGoldText();
    }

    public void AddXP(int playerId, int amount)
    {
        players[playerId].playerXP += amount;
        players[playerId].UpdateXPText();
        CheckForAgeUp(playerId);
    }

    private void CheckForAgeUp(int playerId)
    {
        var player = players[playerId];
        if (player.playerAge < agesData.Length && player.playerXP >= agesData[player.playerAge].baseHealthIncrease)
        {
            player.playerAge++;
            player.UpdateAgeText();
            AgeUp(playerId, player.playerAge);
        }
    }

    private void AgeUp(int playerId, int ageIndex)
    {
        players[playerId].playerBase.IncreaseMaxHealth(agesData[ageIndex].baseHealthIncrease);
    }

    public void SpawnUnit(int playerId, UnitData unitData, Vector2 position)
    {
        var player = players[playerId];
        if (player.playerGold >= unitData.cost)
        {
            player.playerGold -= unitData.cost;
            player.UpdateGoldText();

            GameObject unitPrefab = Instantiate(Resources.Load<GameObject>(unitData.unitName), position, Quaternion.identity);
            Unit unit = unitPrefab.GetComponent<Unit>();
            unit.unitData = unitData;
        }
        else
        {
            Debug.Log("Not enough gold to spawn unit.");
        }
    }

    public bool SpendGold(int playerId, int amount)
    {
        var player = players[playerId];
        if (player.playerGold >= amount)
        {
            player.playerGold -= amount;
            player.UpdateGoldText();
            return true;
        }
        else
        {
            Debug.Log("Not enough gold to spend.");
            return false;
        }
    }

    public void EndGame(int losingTeamId)
    {
        string result = losingTeamId == 1 ? "You Lose!" : "You Win!";
        gameOverText.text = result;
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
