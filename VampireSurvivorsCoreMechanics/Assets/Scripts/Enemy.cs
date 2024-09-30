using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public GameObject xpOrbDrop;

    public float speed;
    public float enemyDamage = 1f;

    public float maximumHealthEnemy = 16;
    public float HealthEnemy;


    public float xpOrbDropChance = 80;
    // Start is called before the first frame update
    void Start()
    {
        HealthEnemy = maximumHealthEnemy;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>().playerIsAlive == true)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void HurtEnemy(float danoParaReceber)
    {
        HealthEnemy -= danoParaReceber;

        if (HealthEnemy <= 0)
        {
            // bro's dead lmao 
            float xpRandomNumber = Random.Range(0, 100);
            if (xpRandomNumber < xpOrbDropChance)
            {
                Instantiate(xpOrbDrop, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().HurtPlayer(enemyDamage);
        }
    }
}
