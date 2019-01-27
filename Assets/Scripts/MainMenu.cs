using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {
	// Update is called once per frame
    /*
	void Update () {
        if (Input.touchCount == 1)
        {
            if (SceneManager.GetActiveScene().ToString() == "MainMenu" &&  Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //SceneManager.LoadScene("OverWorld");
                SwitchScenes("OverWorld");
            }
            else if(SceneManager.GetActiveScene().ToString() != "GameOver" && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //SceneManager.LoadScene("OverWorld");
                SwitchScenes("OverWorld");
            }
        }
    }*/

    public void SwitchScenes(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
