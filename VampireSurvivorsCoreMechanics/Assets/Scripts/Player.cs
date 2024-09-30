using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody2D rig;
    public GameObject projectile;
    public Transform attackPlace;

    bool isCooldown;
    bool isFacingRight = false;
    public float mainAttackCooldown;
    public float timeToMainAttack = 2.3f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        attackPlace.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        rig.AddForce(new Vector2 (horizontalMove, verticalMove).normalized * speed);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isFacingRight) {
                isFacingRight = false;
                Flip();
                attackPlace.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isFacingRight)
            {
                isFacingRight = true;
                Flip();
                attackPlace.rotation = Quaternion.Euler(0, 0, 0);
            }
        }


        if (isCooldown == false)
        {
            isCooldown = true;
            Instantiate(projectile, attackPlace.position, attackPlace.rotation);
        }

        if (isCooldown)
        {
            mainAttackCooldown += Time.deltaTime;
            if (mainAttackCooldown >= timeToMainAttack)
            {
                isCooldown = false;
                mainAttackCooldown = 0;
            }
        }

        if (timeToMainAttack < 0)
        {
            timeToMainAttack = 0.01f;
        }
    }

    private void Flip()
    {
        //isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = -theScale.x;
        transform.localScale = theScale;
    }
}
