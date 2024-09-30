using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //[SerializeField] Transform closestEnemy;
    [SerializeField] float range;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float aimLerp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemy();
    }

    void AutoAim() {
        Enemy closestEnemy = FindNearestEnemy();
        Vector2 targetUpVector = Vector3.up;
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        if (closestEnemy != null)
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
    }
    Enemy FindNearestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        float minimumDistance = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            float distanceBetweenEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceBetweenEnemy < minimumDistance)
            {
                closestEnemy = enemyChecked;
                minimumDistance = distanceBetweenEnemy;
            }
        }
        return closestEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
