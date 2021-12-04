using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private bool applyKnockback;

    private float currentHealth, knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private PlayerController pc;
    private GameObject enemyGO;
    private Rigidbody2D rbEnemy;
    private Animator enemyAnim;


    private void Start()
    {
        currentHealth = maxHealth;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        enemyGO = transform.gameObject;

        enemyAnim = GetComponent<Animator>();
        rbEnemy = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckKnockback();
    }

    private void Damage(float amount)
    {
        currentHealth -= amount;
        playerFacingDirection = pc.GetFacingDirection();

        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        enemyAnim.SetBool("PlayerOnLeft", playerOnLeft);
        enemyAnim.SetTrigger("Damage");

        if (applyKnockback && currentHealth > 0.0f)
        {
            //Knockback
            Knockback();
        }

        if (currentHealth <= 0.0f)
        {
            //Die
            Die();
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbEnemy.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbEnemy.velocity = new Vector2(0.0f, rbEnemy.velocity.y);
        }
    }

    private void Die()
    {
        enemyAnim.SetTrigger("Dead");
    }

    private void DeadLying()
    {
        Destroy(this.gameObject);
    }
}
