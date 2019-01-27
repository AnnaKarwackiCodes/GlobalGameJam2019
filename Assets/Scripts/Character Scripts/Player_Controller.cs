using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : Character_Controller {
    private float myTime;
    public float StartTime;
    public Text debugText;
    public GameObject Final;

    private void Update()
    {
        if (Final != null && Vector3.Distance(transform.position, Final.transform.position) < 10)
        {
            SceneManager.LoadScene("FinalBattle");
        }
    }
}
