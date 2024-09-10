using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Stagehand,
    SecurityGuard,
    Janitor
}

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    public void PlayerDetected (GameObject player)
    {
        if ((int)enemyType == (int)player.GetComponent<PlayerMorfing>().currentMorf)
        {
            Debug.Log("Player Detected!");
        }
        else if (player.GetComponent<PlayerMorfing>().currentMorf == MorfObjects.Default)
        {
            Debug.Log("Player Detected!");
        }
    }
}
