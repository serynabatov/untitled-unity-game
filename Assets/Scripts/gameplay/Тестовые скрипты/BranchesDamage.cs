using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchesDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private CapsuleCollider2D capsuleCollider2D;
    void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Damage(float[] attackDetails)
    {
        Destroy(gameObject);
    }
}
