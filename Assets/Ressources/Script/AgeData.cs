using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgeData", menuName = "ScriptableObjects/AgeData", order = 1)]
public class AgeData : ScriptableObject
{
    public UnitData[] unitData; // Unités disponibles pour cet âge
    public int baseHealthIncrease; // Augmentation de la santé de la base
}