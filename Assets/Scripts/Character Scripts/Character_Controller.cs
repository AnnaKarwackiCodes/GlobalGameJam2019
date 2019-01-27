using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stances
{
    Up,
    Middle,
    Down,
    None
}

public class Character_Controller : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public Stances stance;
    public float TurnTime;
    public bool isTurn;
}
