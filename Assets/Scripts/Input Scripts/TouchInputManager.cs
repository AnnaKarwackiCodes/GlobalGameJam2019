using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update () {
        
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
        }
        else if((start.x < end.x) && (Mathf.Abs(start.y - end.y) < marginOfError))
        {
            currentInput = TouchInputs.Left;
        }
        else if ((start.y > end.y) && (Mathf.Abs(start.x - end.x) < marginOfError))
        {
            currentInput = TouchInputs.Top;
        }
        else if ((start.y < end.y) && (Mathf.Abs(start.x - end.x) < marginOfError))
        {
            currentInput = TouchInputs.Bottom;
        }
        else
        {
            currentInput = TouchInputs.None;
        }
    }

    public void MoveCharacter(Player_Controller player)
    {
        if (Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if(Physics.Raycast(ray, out hit) && hit.collider != null)
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, Time.time * .5f);//Vector3.Lerp(player.transform.position, hit.point, Time.time);
                }
            }
        }
    }

    public TouchInputs CurrentInput
    {
        get { return currentInput; }
        set { currentInput = value; }
    }
}
