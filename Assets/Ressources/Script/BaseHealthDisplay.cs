using UnityEngine;
using UnityEngine.UI;

public class BaseHealthDisplay : MonoBehaviour
{
    public Text playerBaseHPText;
    public Text enemyBaseHPText;
    public Base playerBase;
    public Base enemyBase;

    void Update()
    {
        if (playerBase != null)
        {
            playerBaseHPText.text = "Player Base HP: " + playerBase.GetHealth().ToString();
        }
        
        if (enemyBase != null)
        {
            enemyBaseHPText.text = "Enemy Base HP: " + enemyBase.GetHealth().ToString();
        }
    }
}