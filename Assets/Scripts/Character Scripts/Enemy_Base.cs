using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Base : Character_Controller {

    private Stances myAttack;
    private float myTime;
    public float StartTime;
    public float attackFreq;
    public float attackTimeFrame;
    public float attackTimeStart;
    public Text text;
    public Stances[] attackPattern;
    private int curPatternPos;
    // Use this for initialization
    void Start () {
        currentHealth++;
        curPatternPos = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (isTurn)
        {
            Attack();
            text.text = " ";
        }
        else
        {
            Defend();
            //StartTime = Time.time;
            myTime = Time.time;
        }
	}
    private void Attack()
    {
        if(Time.time - myTime >= attackFreq)
        {
            attackTimeStart = Time.time;
            //myAttack = (Stances)UnityEngine.Random.Range(0, 3);
            myAttack = attackPattern[curPatternPos];
            if (myAttack == Stances.Up) { GetComponent<Animator>().SetInteger("Attacking", 1); }
            else { GetComponent<Animator>().SetInteger("Attacking", 2); }
            curPatternPos++;
            if(curPatternPos > attackPattern.Length - 1) { curPatternPos = 0; }
            myTime = Time.time;
        }
        //if(Time.time - StartTime >= TurnTime) { isTurn = false; }
    }

    private void Defend()
    {
        text.text = "Defense: " + stance.ToString();
        GetComponent<Animator>().SetInteger("Attacking", 0);
    }

    public Stances MyAttack
    {
        get { return myAttack; }
        set { myAttack = value;}
    }
    
}
