using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class StoneGiant : Unit
{
    private void Start()
    {
        health = 200;
        damage = 30;
        cost = 150;
        moveSpeed = 1f;
        isPlayerOne = true; // Ou false pour l'IA
    }
}