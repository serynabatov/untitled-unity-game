using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField]
    private float speedMod;
    [SerializeField]
    private float rotationMod;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speedMod, Space.World);
        transform.Rotate(0, 0, Time.deltaTime * -rotationMod);
    }
}
