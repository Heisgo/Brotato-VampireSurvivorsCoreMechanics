using UnityEditor;
using UnityEngine;
using TMPro;
using System;
public class Enemy : MonoBehaviour
{
    [Header("GameObject References")]
    GameObject player;
    [SerializeField] GameObject xpOrbDrop;

    [Header("Movement & Damage")]
    [SerializeField] float speed;
    public float enemyDamage = 1f;

    [Header("Health")]
    public float maximumHealthEnemy = 16;
    public float HealthEnemy;

    [Header("XP Orb")]
    [SerializeField] float xpOrbDropChance = 80;
    [Header("Actions")]
    public static Action<int, Vector2> onDamageTaken;
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

    public void HurtEnemy(int danoParaReceber)
    {
        onDamageTaken?.Invoke(danoParaReceber, transform.position);
        HealthEnemy -= danoParaReceber;



        if (HealthEnemy <= 0)
        {
            // bro's dead lmao 
            float xpRandomNumber = UnityEngine.Random.Range(0, 100);
            if (xpRandomNumber < xpOrbDropChance)
            {
                Instantiate(xpOrbDrop, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
    }
}
