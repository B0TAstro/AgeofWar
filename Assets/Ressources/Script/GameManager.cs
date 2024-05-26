using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerGold = 100; // Or initial pour le joueur
    public int playerXP = 0; // XP initial pour le joueur
    public int playerAge = 1; // Age initial du joueur
    public int[] xpThresholds; // Seuils d'XP pour chaque âge
    public Text playerGoldText; // Référence au texte UI pour afficher l'or
    public Text playerXPText; // Référence au texte UI pour afficher l'XP
    public Text playerAgeText; // Référence au texte UI pour afficher l'âge
    public Text gameOverText; // Référence au texte UI pour afficher le message de fin de partie

    void Start()
    {
        UpdateGoldText();
        UpdateXPText();
        UpdateAgeText();
        gameOverText.gameObject.SetActive(false); // Masquer le message de fin de partie au début
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
            // Logique pour débloquer de nouvelles unités ici
        }
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
