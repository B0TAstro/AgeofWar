using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerGold = 100;
    public int playerXP = 0;
    public int playerAge = 1;
    public int[] xpThresholds = { 100, 300, 600 };
    public AgeData[] agesData;
    public Text playerGoldText;
    public Text playerXPText;
    public Text playerAgeText;
    public Text gameOverText;

    public Base playerBase; // Référence à la base du joueur

    void Start()
    {
        UpdateGoldText();
        UpdateXPText();
        UpdateAgeText();
        gameOverText.gameObject.SetActive(false);
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        UpdateGoldText();
        Debug.Log($"Player gold: {playerGold}");
    }

    public bool SpendGold(int amount)
    {
        if (playerGold >= amount)
        {
            playerGold -= amount;
            UpdateGoldText();
            Debug.Log($"Player spent {amount} gold. Remaining: {playerGold}");
            return true;
        }
        Debug.Log("Not enough gold to spend.");
        return false;
    }

    public void AddXP(int amount)
    {
        playerXP += amount;
        UpdateXPText();
        Debug.Log($"Player XP: {playerXP}");
        CheckForAgeUp();
    }

    private void CheckForAgeUp()
    {
        if (playerAge < xpThresholds.Length && playerXP >= xpThresholds[playerAge])
        {
            playerAge++;
            UpdateAgeText();
            Debug.Log($"Player has aged up to Age {playerAge}");
            AgeUp(playerAge - 1); // L'index des âges commence à 0
        }
    }

    private void AgeUp(int ageIndex)
    {
        // Mise à jour de la base
        playerBase.IncreaseMaxHealth(agesData[ageIndex].baseHealthIncrease);
        
        // Débloquer de nouvelles unités ici
        // Par exemple, mettre à jour les boutons de spawn pour utiliser les nouvelles unités

        // Mettez à jour l'interface utilisateur ou d'autres éléments ici
    }

    private void UpdateGoldText()
    {
        if (playerGoldText != null)
        {
            playerGoldText.text = $"Gold: {playerGold}";
        }
    }

    private void UpdateXPText()
    {
        if (playerXPText != null)
        {
            playerXPText.text = $"XP: {playerXP}";
        }
    }

    private void UpdateAgeText()
    {
        if (playerAgeText != null)
        {
            playerAgeText.text = $"Age: {playerAge}";
        }
    }

    public void EndGame(int losingTeamId)
    {
        string result = losingTeamId == 1 ? "You Lose!" : "You Win!";
        gameOverText.text = result;
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0; // Arrêter le temps
    }
}