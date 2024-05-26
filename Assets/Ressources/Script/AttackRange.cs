using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Unit unit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit otherUnit = collision.GetComponent<Unit>();
        if (otherUnit != null && otherUnit.unitData.teamId != unit.unitData.teamId)
        {
            unit.EnemyInRange(otherUnit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit otherUnit = collision.GetComponent<Unit>();
        if (otherUnit != null && otherUnit.unitData.teamId != unit.unitData.teamId)
        {
            unit.EnemyOutOfRange(otherUnit);
        }
    }
}
