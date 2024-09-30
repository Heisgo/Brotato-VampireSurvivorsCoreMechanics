using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Pos;
    public GameObject enemy;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            int x = Random.Range(0, 4);
            Instantiate(enemy, Pos[x].transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
