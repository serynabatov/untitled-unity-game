using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float startPoint;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float proportion;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PrototypeHero");
        startPoint = -player.transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset.x = (player.transform.position.x + startPoint) * proportion;
        transform.localPosition = offset;
    }
}
