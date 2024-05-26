using UnityEngine;
using UnityEngine.UI;

public class UnitInfoDisplay : MonoBehaviour
{
    public Text unitHPText;
    public Text unitDamageText;
    public Text unitGoldRewardText;
    public Unit unit;

    void Start()
    {
        if (unit != null)
        {
            UpdateUnitInfo();
        }
    }

    void Update()
    {
        if (unit != null)
        {
            UpdateUnitInfo();
            Vector3 unitPosition = Camera.main.WorldToScreenPoint(unit.transform.position);
            unitHPText.transform.position = unitPosition + new Vector3(0, 30, 0); // Ajustez la position selon vos besoins
            unitDamageText.transform.position = unitPosition + new Vector3(0, 50, 0);
            unitGoldRewardText.transform.position = unitPosition + new Vector3(0, 70, 0);
        }
    }

    void UpdateUnitInfo()
    {
        unitHPText.text = $"HP: {unit.GetHealth()}";
        unitDamageText.text = $"Damage: {unit.unitData.attackPower}";
        unitGoldRewardText.text = $"Gold: {unit.unitData.goldReward}";
    }
}