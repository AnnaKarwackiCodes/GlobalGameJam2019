using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour {
    private Player_Controller player;
    private Enemy_Base enemy;
    private TouchInputManager touch;
    public Slider timeSlider;
    public GameObject playerPanel;
    public GameObject enemyPanel;
    public Slider playerHealthBar;
    public Slider enemyHealthBar;
    public GameObject[] arrows;
    public Sprite playerSoul;
    public Sprite enemySoul;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player_Controller>();
        enemy = FindObjectOfType<Enemy_Base>();
        touch = FindObjectOfType<TouchInputManager>();
        playerPanel.SetActive(player.isTurn);
        enemyPanel.SetActive(enemy.isTurn);
        playerHealthBar.maxValue = player.maxHealth;
        enemyHealthBar.maxValue = enemy.maxHealth;
    }
	
	// Update is called once per frame
	void Update () {

        playerHealthBar.value = player.currentHealth;
        enemyHealthBar.value = enemy.currentHealth;

        touch.SwipeInput();

        if(player.currentHealth > 0)
        {
            if (player.isTurn)
            {
                timeSlider.value = (Time.time - player.StartTime) / player.TurnTime;
                timeSlider.transform.GetChild(2).GetComponentInChildren<Image>().sprite = playerSoul;
                //dealing damange
                PlayerAttackInputCheck();
                touch.CurrentInput = TouchInputs.None;
                enemy.StartTime = Time.time;
                if (Time.time - player.StartTime > player.TurnTime)
                {
                    player.isTurn = false;
                    enemy.isTurn = true;
                    touch.CurrentInput = TouchInputs.None;
                    playerPanel.SetActive(player.isTurn);
                    enemyPanel.SetActive(enemy.isTurn);
                }
            }

            else if (enemy.isTurn)
            {
                timeSlider.value = 1 - ((Time.time - enemy.StartTime) / enemy.TurnTime);
                timeSlider.transform.GetChild(2).GetComponentInChildren<Image>().sprite = enemySoul;

                DisplayEnemyAttack();
                EnemyAttackInputCheck();
                touch.CurrentInput = TouchInputs.None;
                player.StartTime = Time.time;
                if (Time.time - enemy.StartTime > enemy.TurnTime)
                {
                    enemy.isTurn = false;
                    player.isTurn = true;
                    touch.CurrentInput = TouchInputs.None;
                    playerPanel.SetActive(player.isTurn);
                    enemyPanel.SetActive(enemy.isTurn);
                    arrows[0].SetActive(false);
                    arrows[1].SetActive(false);
                    arrows[2].SetActive(false);
                }
            }
        }
        if(player.currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else if(enemy.currentHealth <= 0)
        {
           SceneManager.LoadScene("OverWorld");
        }
    }

    void EnemyAttackInputCheck()
    {
        switch (enemy.MyAttack)
        {
            case Stances.Up:
                if ((touch.CurrentInput != TouchInputs.Bottom) && (Time.time - enemy.attackTimeStart >= enemy.attackTimeFrame))
                {
                    player.currentHealth--;
                    enemy.MyAttack = Stances.None;
                }
                else if(touch.CurrentInput == TouchInputs.Bottom && Time.time - enemy.attackTimeStart <= enemy.attackTimeFrame)
                {
                    arrows[0].SetActive(false);
                    enemy.MyAttack = Stances.None;
                    touch.CurrentInput = TouchInputs.None;
                    //enemy.GetComponent<Animator>().SetInteger("Attacking", 0);
                }
                return;
            case Stances.Down:
                if (touch.CurrentInput != TouchInputs.Top && Time.time - enemy.attackTimeStart >= enemy.attackTimeFrame)
                {
                    player.currentHealth--;
                    enemy.MyAttack = Stances.None;
                }
                else if(touch.CurrentInput == TouchInputs.Top && Time.time - enemy.attackTimeStart <= enemy.attackTimeFrame)
                {
                    arrows[2].SetActive(false);
                    enemy.MyAttack = Stances.None;
                    touch.CurrentInput = TouchInputs.None;
                    //enemy.GetComponent<Animator>().SetInteger("Attacking", 0);
                }
                return;
            case Stances.Middle:
                if ((touch.CurrentInput != TouchInputs.Left || touch.CurrentInput != TouchInputs.Right) && Time.time - enemy.attackTimeStart >= enemy.attackTimeFrame)
                {
                    player.currentHealth--;
                    enemy.MyAttack = Stances.None;
                }
                else if (touch.CurrentInput == TouchInputs.Left || touch.CurrentInput == TouchInputs.Right && Time.time - enemy.attackTimeStart <= enemy.attackTimeFrame)
                {
                    arrows[1].SetActive(false);
                    enemy.MyAttack = Stances.None;
                    touch.CurrentInput = TouchInputs.None;
                    //enemy.GetComponent<Animator>().SetInteger("Attacking", 0);
                }
                return;
            default:
                Debug.Log("Enemy is doing nothing");
                return;
        }
    }

    void PlayerAttackInputCheck()
    {
        switch (enemy.stance)
        {
            case Stances.Up:
                if(touch.CurrentInput != TouchInputs.Top && touch.CurrentInput != TouchInputs.None)
                {
                    enemy.currentHealth--;
                    touch.CurrentInput = TouchInputs.None;
                }
                break;
            case Stances.Down:
                if (touch.CurrentInput != TouchInputs.Bottom && touch.CurrentInput != TouchInputs.None)
                {
                    enemy.currentHealth--;
                    touch.CurrentInput = TouchInputs.None;
                }
                break;
            case Stances.Middle:
                if ((touch.CurrentInput != TouchInputs.Left && touch.CurrentInput != TouchInputs.Right) && touch.CurrentInput != TouchInputs.None)
                {
                    enemy.currentHealth--;
                    touch.CurrentInput = TouchInputs.None;
                }
                break;
            default:
                break;
        }
        //player.GetComponent<Animator>().SetInteger("State", 0);
    }

    void DisplayEnemyAttack()
    {
        switch (enemy.MyAttack)
        {
            case Stances.Up:
                arrows[0].SetActive(true);
                arrows[1].SetActive(false);
                arrows[2].SetActive(false);
                break;
            case Stances.Middle:
                arrows[0].SetActive(false);
                arrows[1].SetActive(true);
                arrows[2].SetActive(false);
                break;
            case Stances.Down:
                arrows[0].SetActive(false);
                arrows[1].SetActive(false);
                arrows[2].SetActive(true);
                break;
            default:
                arrows[0].SetActive(false);
                arrows[1].SetActive(false);
                arrows[2].SetActive(false);
                break;
        }
    }

}
