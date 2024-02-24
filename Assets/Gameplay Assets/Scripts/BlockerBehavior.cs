using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _blocker;
    void Start()
    {
        DisableBlocker();
    }

    private void DisableBlocker()
    {
        int blockStatus = PlayerPrefs.GetInt(GameManager.LastWaterSceneStatus, 0);
        if (blockStatus == 1)
        {
            Destroy(_blocker);
            Destroy(gameObject);
        }
    }

}
