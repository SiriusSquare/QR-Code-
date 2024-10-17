using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sworddef : MonoBehaviour
{
    public GameObject projectilePrefab;
    public string playerObjectTag = "Player";
    private PlayerStatus playerStatus;
    public float attackRateDef = 1f;
    public float attackRate;
    public float projectileSpeed = 1.0f;
    private bool canShoot = true;
    private int combo;
    public float durationDef = 0.2f;
    public float duration;
    public float durationreal;
    private Animator animator;
    private float projectileRotationX;

    private Transform parentTransform;

    private void Start()
    {
        if (IsParentPlayer())
        {
            GameObject playerObject = parentTransform.gameObject;

            if (playerObject != null)
            {
                playerStatus = playerObject.GetComponent<PlayerStatus>();

                if (playerStatus != null)
                {
                    attackRate = attackRateDef * playerStatus.FinalAttackSpeed;
                    duration = durationDef / playerStatus.FinalAttackSpeed;
                }
            }
        }

        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (IsParentPlayer() && Input.GetMouseButtonDown(0) && canShoot)
        {
            if (playerStatus != null)
            {
                attackRate = attackRateDef * playerStatus.FinalAttackSpeed;
                duration = durationDef / playerStatus.FinalAttackSpeed;
            }

            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        if (animator != null)
        {
            durationreal = durationDef * 10 * playerStatus.FinalAttackSpeed;

            
            if (combo == 0)
            {
                animator.speed = durationreal;
                animator.SetTrigger("SwordSwing1tri");
                combo = 1;
            }
            else if (combo == 1)
            {
                animator.speed = durationreal;
                animator.SetTrigger("SwordSwing2tri");
                combo = 0;
            }
        }

        
        if (combo == 0)
        {
            projectileRotationX = 180.0f;
        }
        else if (combo == 1)
        {
            projectileRotationX = 0.0f;
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(projectileRotationX, 0f, 0f));
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 directionToMouse = (mousePosition - transform.position).normalized;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = directionToMouse * projectileSpeed;
        }

        yield return new WaitForSeconds(1 / attackRate);

        canShoot = true;
    }

    
    private bool IsParentPlayer()
    {
        Transform currentTransform = transform.parent;

        while (currentTransform != null)
        {
            if (currentTransform.CompareTag(playerObjectTag))
            {
                parentTransform = currentTransform;
                return true;
            }

            currentTransform = currentTransform.parent;
        }

        return false;
    }
}
