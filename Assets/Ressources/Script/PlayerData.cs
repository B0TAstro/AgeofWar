using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public int playerId;
    public int playerGold;
    public int playerXP;
    public int playerAge;
    public Base playerBase;

    // Références aux éléments de l'interface utilisateur
    public Text playerGoldText;
    public Text playerXPText;
    public Text playerAgeText;

    // Mettre à jour le texte de l'or
    public void UpdateGoldText()
    {
        if (playerGoldText != null)
        {
            playerGoldText.text = "Gold: " + playerGold.ToString();
        }
    }

    // Mettre à jour le texte de l'XP
    public void UpdateXPText()
    {
        if (playerXPText != null)
        {
            playerXPText.text = "XP: " + playerXP.ToString();
        }
    }

    // Mettre à jour le texte de l'âge
    public void UpdateAgeText()
    {
        if (playerAgeText != null)
        {
            playerAgeText.text = "Age: " + playerAge.ToString();
        }
    }
}
