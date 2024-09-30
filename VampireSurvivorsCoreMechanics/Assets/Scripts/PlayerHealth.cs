using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerMaximumHealth = 3;
    public float playerAtualHealth;
    public Slider healthBar;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        playerAtualHealth = playerMaximumHealth;
        healthBar.value = playerAtualHealth;
        healthText.text = playerAtualHealth.ToString("F0") + "/" + playerMaximumHealth.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerAtualHealth;
        healthBar.maxValue = playerMaximumHealth;
    }

    public void HurtPlayer(float danoParaReceber)
    {
        playerAtualHealth -= danoParaReceber;
        healthBar.value = playerAtualHealth;
        healthText.text = playerAtualHealth.ToString("F0") + "/" + playerMaximumHealth.ToString("F0");

        if (playerAtualHealth <= 0)
        {
            FindObjectOfType<GameManager>().gameOverFunc();
            Destroy(this.gameObject);
        }
    }

    public void HealPlayer(float vidaParaReceber)
    {
        if (playerAtualHealth < playerMaximumHealth)
        {
            playerAtualHealth += vidaParaReceber;
            healthBar.value = playerAtualHealth;
            healthText.text = playerAtualHealth.ToString("F0") + "/" + playerMaximumHealth.ToString("F0");
            if (playerAtualHealth > playerMaximumHealth)
            {
                playerAtualHealth = playerMaximumHealth;
                healthText.text = playerAtualHealth.ToString("F0") + "/" + playerMaximumHealth.ToString("F0");
                healthBar.value = playerAtualHealth;
            }
        }
        else
        {
            playerAtualHealth = playerMaximumHealth;
            healthText.text = playerAtualHealth.ToString("F0") + "/" + playerMaximumHealth.ToString("F0");
            healthBar.value = playerAtualHealth;
        }
    }
    public void IncreaseMaxHealth(float vidaParaAdicionar)
    {
        playerMaximumHealth += vidaParaAdicionar;
        playerAtualHealth = playerMaximumHealth;

        healthText.text = $"{playerAtualHealth.ToString("F0")}  /  {playerMaximumHealth.ToString("F0")}";
        healthBar.maxValue = playerMaximumHealth;
        healthBar.value = playerAtualHealth;
    }
}
