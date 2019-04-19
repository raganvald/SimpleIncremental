using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(EnemyMovement))]
[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class AIChaseAction : AIAction
{
    EnemyMovement enemyMovement;
    EnemyAttackRanged enemyAttackRanged;
    void Awake()
    {
        //enemyMovement = GetComponent<EnemyMovement>();
        //enemyAttackRanged = GetComponent<EnemyAttackRanged>();
    }

    public override void Act(AIStateController controller)
    {
        Chase(controller);
    }

    private void Chase(AIStateController controller)
    {
        if (!enemyMovement)
            enemyMovement = controller.GetComponent<EnemyMovement>();
        if (!enemyAttackRanged)
            enemyAttackRanged = controller.GetComponent<EnemyAttackRanged>();
        if (!enemyMovement.chasing)
            enemyMovement.StartChasing();
        if (!enemyAttackRanged.attacking)
            enemyAttackRanged.StartFiring();
        return;
    }
}
