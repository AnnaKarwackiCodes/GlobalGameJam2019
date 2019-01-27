using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum TouchInputs //refers to where the player finger starts
{
    Top,
    Bottom,
    Left,
    Right,
    None
}

public class TouchInputManager : MonoBehaviour {
   
    public Text text;
    private Vector2 start;
    private Vector2 end;
    private int marginOfError = 300;
    public Vector2 holder;
    private TouchInputs currentInput;
    private RaycastHit hit;
    private Player_Controller player;
    private float startTime;
    private float journey;
    private Camera camera;
    // Update is called once per frame
    void Start () {
        player = FindObjectOfType<Player_Controller>();
        camera = FindObjectOfType<Camera>();
    }

    public void SwipeInput()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                start = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                end = Input.GetTouch(0).position;
                player.GetComponent<Animator>().Play("Player_Idle");
                DetermineDirection();
            }
        }
    }

    void DetermineDirection()
    {
        currentInput = TouchInputs.None;

        if ((start.x > end.x) &&(Mathf.Abs(start.y - end.y) < marginOfError))
        {
            currentInput = TouchInputs.Right;
            if (player.isTurn) { player.GetComponent<Animator>().Play("Player_Attack_Mid_A"); }
            else { player.GetComponent<Animator>().Play("Player_Defend_Mid"); }
            //player.GetComponent<Animator>().SetInteger("Attack", 2);
        }
        else if((start.x < end.x) && (Mathf.Abs(start.y - end.y) < marginOfError))
        {
            currentInput = TouchInputs.Left;
            if (player.isTurn) { player.GetComponent<Animator>().Play("Player_Attack_Mid_A"); }
            else { player.GetComponent<Animator>().Play("Player_Defend_Mid"); }
            //player.GetComponent<Animator>().SetInteger("Attack", 2);
        }
        else if ((start.y > end.y) && (Mathf.Abs(start.x - end.x) < marginOfError))
        {
            currentInput = TouchInputs.Top;
            if (player.isTurn) { player.GetComponent<Animator>().Play("Player_Attack_High_A"); }
            else { player.GetComponent<Animator>().Play("Player_Defend_High"); }
            //player.GetComponent<Animator>().SetInteger("Attack", 3);
        }
        else if ((start.y < end.y) && (Mathf.Abs(start.x - end.x) < marginOfError))
        {
            currentInput = TouchInputs.Bottom;
            if (player.isTurn) { player.GetComponent<Animator>().Play("Player_Attack_Low_A"); }
            else { player.GetComponent<Animator>().Play("Player_Defend_Low"); }
            //player.GetComponent<Animator>().SetInteger("Attack", 1);
        }
        else
        {
            currentInput = TouchInputs.None;
            player.GetComponent<Animator>().Play("Player_Idle");
        }
    }

    public void MoveCharacter(Player_Controller player)
    {
        if (Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                Physics.Raycast(ray, out hit);
                startTime = Time.time;
                journey = Vector3.Distance(player.transform.position, hit.point);
            }
        }
    }

    public void HaveCharacterMove()
    {
        
        if(Random.Range(0,300) == 50 && Time.time > 5 && Vector3.Distance(player.transform.position,player.Final.transform.position) > 15)
        {
            GameManager.playerPos = player.transform.position;
            GameManager.cameraPos = camera.transform.position;
            GameManager.moveToPreBattlePos = true;
            SceneManager.LoadScene("test");
        }
        if(hit.collider)
        {
            float temp = ((Time.time - startTime) / journey);
            player.transform.LookAt(hit.point);
            player.transform.position = Vector3.Lerp(player.transform.position, hit.point, temp);
            float x = Mathf.Lerp(camera.transform.position.x, hit.point.x, temp);
            float z = Mathf.Lerp(camera.transform.position.z, hit.point.z, temp);
            camera.transform.position = new Vector3(x, camera.transform.position.y, z);
        }
    }

    public TouchInputs CurrentInput
    {
        get { return currentInput; }
        set { currentInput = value; }
    }
}
