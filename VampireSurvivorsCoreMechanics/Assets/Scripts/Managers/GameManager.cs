using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool playerIsAlive;

    public float xpValuePerOrb = 10;
    public int playerLvl = 1;
    public float playerXp = 0;
    public float playerNeededXp;

    public Slider xpSlider;
    public Text playerLevelText;

    [SerializeField] GameObject LevelPanelShop;
    // Start is called before the first frame update
    void Start()
    {
        playerIsAlive = true;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOverFunc()
    {
        playerIsAlive = false;
        //gameOverPanel.SetActive(true);
    }

    public void GainXP()
    {
        playerXp += xpValuePerOrb;
    }


    public void CloseLevelShop()
    {
        LevelPanelShop.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenLevelShop()
    {
        LevelPanelShop.SetActive(true);
        Time.timeScale = 0;
    }



    private void FixedUpdate()
    {
        xpSlider.value = playerXp;
        xpSlider.maxValue = playerNeededXp;

        playerLevelText.text = "Level: " + playerLvl.ToString("0");

        if (playerXp >= playerNeededXp)
        {
            // ---------------------------------------
            OpenLevelShop();
            playerXp = 0;
            playerLvl++;
            playerNeededXp += playerNeededXp * .10f;
            Time.timeScale = 0;
        }
    }
}
