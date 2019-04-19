using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(EnemyTargeting))]
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class AILookDecision : AIDecision
{
    EnemyTargeting enemyTargeting;
    public void Awake()
    {
        //enemyTargeting = GetComponent<EnemyTargeting>();
    }

    public override bool Decide(AIStateController controller)
    {
        if (!enemyTargeting)
            enemyTargeting = controller.GetComponent<EnemyTargeting>();
        bool targetVisible = false;
        if (enemyTargeting.target)
            targetVisible = true;
        return targetVisible;
    }
}