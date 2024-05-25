using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class StoneAgeWarrior : Unit
{
    private void Start()
    {
        health = 100;
        damage = 10;
        cost = 50;
        moveSpeed = 2f;
        isPlayerOne = true; // Ou false pour l'IA
    }
}