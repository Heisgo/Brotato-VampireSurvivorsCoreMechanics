using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum State { Idle, Attack }
    State state;


    [Header("Hit Detection")]
    [SerializeField] Transform hitDetectionTransform;
    [SerializeField] float hitRange;

    [Header("Attack Settings")]
    [SerializeField] float range;
    [SerializeField] int damage;
    [SerializeField] float attackDelay;
    float attackTimer;
    List<Enemy> attackedEnemies = new List<Enemy>();
    [SerializeField] LayerMask enemyLayer;

    [Header("Aiming Settings")]
    [SerializeField] float aimLerp;
    [Header("Animations")]
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }

        //AutoAim();
        //Attack();
    }

    [NaughtyAttributes.Button]
    void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;

        animator.speed = 1f / attackDelay;
    }

    private void Attacking()
    {
        Attack();
    }

    void StopAttack()
    {
        state = State.Idle;
        attackedEnemies.Clear();
    }

    private void Attack()
    {
        //throw new NotImplementedException();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy =  enemies[i].GetComponent<Enemy>();
            if (!attackedEnemies.Contains(enemy))
            {
                enemies[i].GetComponent<Enemy>().HurtEnemy(damage);
                attackedEnemies.Add(enemy);
            }

        }
    }

    void AutoAim() {
        Enemy closestEnemy = FindNearestEnemy();
        Vector2 targetUpVector = Vector3.up;
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        if (closestEnemy != null)
        { targetUpVector = (closestEnemy.transform.position - transform.position).normalized; AttackTimer(); }

        PauseWait();
    }

    private void AttackTimer()
    {
        if (attackTimer >= attackDelay) {
            attackTimer = 0;
            StartAttack();
        }
    }

    void PauseWait()
    {
        attackTimer += Time.deltaTime;
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

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitRange);
    }
}
