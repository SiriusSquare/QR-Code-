using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMain2 : MonoBehaviour
{
    public string Tag = "enemy";
    public string playerObjectTag = "Player";
    private PlayerStatus playerStatus;
    public float lifetime = 2f;
    public float damegedef = 5.0f;
    public float damage;

    void Start()
    {
        
        GameObject playerObject = GameObject.FindWithTag(playerObjectTag);
        
        if (playerObject != null)
        {
            
            playerStatus = playerObject.GetComponent<PlayerStatus>();

            if (playerStatus != null)
            {
                
                damage = damegedef + playerStatus.FinalAttackPower;
            }
            else
            {

            }
        }
        else
        {

        }
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tag))
        {
            
            EnemyMain enemy = other.GetComponent<EnemyMain>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            
        }
        else
        {
            Debug.Log("태그 불일치: " + other.gameObject.tag);
        }
    }
}
