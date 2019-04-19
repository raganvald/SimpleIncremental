using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTargeting))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttackRanged))]
[RequireComponent(typeof(EnemyAttackMelee))]
public class EnemyAI : MonoBehaviour
{
    EnemyTargeting enemyTargeting;
    EnemyMovement enemyMovement;
    EnemyAttackRanged enemyAttackRanged;
    EnemyAttackMelee enemyAttackMelee;
    public int closestRange = 1;
    public int rangedWeaponDistanceMax;
    public int rangedWeaponDistanceMin;
    public int meleeWeaponDistanceMax;
    private float playerDistanceX;
    private float playerDistanceY;

    bool targetAcquired = false;
    bool chasePlayer = false;
    enum MovementState { none, patrol, chasing}
    enum AttackState { none, ranged, melee };
    MovementState currentMovement = MovementState.none;
    AttackState currentAttack = AttackState.none;

    void Awake()
    {
        enemyTargeting = GetComponent<EnemyTargeting>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttackRanged = GetComponent<EnemyAttackRanged>();
        enemyAttackMelee = GetComponent<EnemyAttackMelee>();
    }

    private void Start()
    {
        enemyTargeting.OnNewTargetAcquired += TargetAcquired;
        enemyTargeting.OnTargetLost += TargetLost;
    }

    public void TargetAcquired()
    {
        targetAcquired = true;
        updateTargetDistance();
    }

    public void TargetLost()
    {
        targetAcquired = false;
    }

    private void updateTargetDistance()
    {
        if (enemyTargeting && enemyTargeting.target)
        { 
            playerDistanceX = Mathf.Abs(enemyTargeting.target.position.x - transform.position.x);
            playerDistanceY = Mathf.Abs(enemyTargeting.target.position.y - transform.position.y);
        }
    }

    public void Update()
    {
        updateTargetDistance();
        if (targetAcquired)
        {
            //Handle Enemy Movement
            if (chasePlayer == false)
            {
                if (playerDistanceX > closestRange)
                {
                    Debug.Log("Chase Player");
                    chasePlayer = true;
                    enemyMovement.StartChasing();
                }
            } else
            {
                if (playerDistanceX < closestRange)
                {
                    Debug.Log("Stop Chasing Player");
                    chasePlayer = false;
                    enemyMovement.StopChasing();
                }
            }
            //Handle Enemy Attack
            if (playerDistanceX < rangedWeaponDistanceMax && playerDistanceX > rangedWeaponDistanceMin)
            {
                currentAttack = AttackState.ranged;
            }
            else if (playerDistanceX < meleeWeaponDistanceMax)
            {
                currentAttack = AttackState.melee;
            }

            if (currentAttack == AttackState.ranged)
            {
                Debug.Log("AttackRanged");
                enemyAttackRanged.StartFiring();
            }    
            else if (currentAttack == AttackState.melee)
            {
                Debug.Log("AttackMelee");
                enemyAttackMelee.StartAttacking();
            }

        } 
        //Patrol

    }

}
