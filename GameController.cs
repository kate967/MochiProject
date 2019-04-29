using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerController playerController;
    public Text winText;
    public Text gameOverText;

    private void Awake()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>();

        winText.text = "";
        gameOverText.text = "";
    }

    void Update()
    {
        if(playerController.score >= 6)
        {
            //You Win!
            Debug.Log("You Win!");
            winText.text = "You Win";
        }

        if(playerController.takeIce == true && Input.GetKey(KeyCode.T))
        {
            //Win
            Debug.Log("You Win, but in the mean way");
            winText.text = "You Win, but in the mean way";
        }

        if(playerController.health <= 0)
        {
            //Died
            Debug.Log("You Died :(");
            gameOverText.text = "You Died :(";
        }

        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
