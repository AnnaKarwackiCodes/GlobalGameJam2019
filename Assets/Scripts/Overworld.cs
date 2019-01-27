using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overworld : MonoBehaviour {

    public Text text;
    private TouchInputManager touch;
    private Player_Controller player;
    private Camera cam;

	// Use this for initialization
	void Start () {
        touch = FindObjectOfType<TouchInputManager>();
        player = FindObjectOfType<Player_Controller>();
        cam = FindObjectOfType<Camera>();
        if (GameManager.moveToPreBattlePos)
        {
            player.transform.position = GameManager.playerPos;
            cam.transform.position = GameManager.cameraPos;
            GameManager.moveToPreBattlePos = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        touch.MoveCharacter(player);
        touch.HaveCharacterMove();
        text.text = touch.holder.ToString();
	}
}
