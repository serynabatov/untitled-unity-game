using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactions : MonoBehaviour
{
    // Start is called before the first frame update
    Animator enemyAnimator;
    int hitCounter = 0;
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCounter == 4)
        {
            hitCounter = 0;
            enemyAnimator.SetBool("Dead", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyAnimator.SetTrigger("Damaged");
        hitCounter++;
        if (hitCounter == 3)
        {
            enemyAnimator.SetBool("Dead", true);
        }
        
    }
}
