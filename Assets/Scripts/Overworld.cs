using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overworld : MonoBehaviour {

    public Text text;
    private TouchInputManager touch;
    private Player_Controller player;

	// Use this for initialization
	void Start () {
        touch = FindObjectOfType<TouchInputManager>();
        player = FindObjectOfType<Player_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        touch.MoveCharacter(player);
        text.text = touch.holder.ToString();
	}
}
