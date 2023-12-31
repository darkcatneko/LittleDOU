using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using BehaviorDesigner.Runtime;
using System;
using UniRx;
using Unity.Mathematics;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;

public enum EnemyStatus
{
    ENEMY_IDLE,
    ENEMY_MOVEMENT,
    ENEMY_FIGHT
}
public class Cho_EnemyAI : MonoBehaviour
{
    public EnemyStatus EnemyCurrentStatus;

    public Color EnemyBodyColor;

    [SerializeField]
    private bool moveTrigger;
    [SerializeField]
    private bool idleTrigger;
    [SerializeField]
    private bool fightTrigger;

    private Material enemyBodyMat;
    private Material enemyPantMat;

   

    [SerializeField,Tooltip("設置<該移動了>的時間")]
    private int timeToMoveValue = 25;
    [SerializeField, Tooltip("設置<該移動了>時間的閾值")]
    private int timeToMoveThreshold = 5;
    [SerializeField, Tooltip("設置<該發呆了>的時間")]
    private int timeToDazeValue = 35;
    [SerializeField, Tooltip("設置<該發呆了>時間的閾值")]
    private int timeToDazeThreshold = 5;

    [SerializeField]
    private BehaviorTree behaviorTree;
    [SerializeField]
    private ExternalBehavior[] externalBehaviorTrees;
    private int index;

    

    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();

        moveTrigger = false;
        idleTrigger = false;
        fightTrigger = false;

        enemyBodyMat = transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().materials[0];
        enemyPantMat = transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().materials[0];

        EnemyCurrentStatus = EnemyStatus.ENEMY_IDLE;

    }

    void Update()
    {
        changeEnemyBehaviorState();
        //Debug.Log((SharedColor)behaviorTree.GetVariable("EnemyBodyColor"));
        //var enemyBodyColor = (SharedColor)GlobalVariables.Instance.GetVariable("EnemyBodyColor");
        var enemyBodyColor = (SharedColor)behaviorTree.GetVariable("EnemyBodyColor");
        if(enemyBodyColor != null )
        {
            enemyBodyMat.color = enemyBodyColor.Value;
            enemyPantMat.color = enemyBodyColor.Value;
        }
        
    }

    //切換敵人行為模式
    void changeEnemyBehaviorState()
    {
        switch (EnemyCurrentStatus)
        {
            case EnemyStatus.ENEMY_IDLE:
                if (!idleTrigger)
                {

                    behaviorTree.DisableBehavior();
                    behaviorTree.ExternalBehavior = externalBehaviorTrees[0];
                    behaviorTree.EnableBehavior();
                    idleTrigger = true;
                }
                if (!moveTrigger)
                {
                    //經過一定時間後角色會進入MOVEMENT狀態
                    Observable.Timer(TimeSpan.FromSeconds(Random.Range(timeToMoveValue - timeToMoveThreshold, timeToMoveValue + timeToMoveThreshold))).Subscribe(_ =>
                    {
                        behaviorTree.DisableBehavior();
                        behaviorTree.ExternalBehavior = externalBehaviorTrees[1];
                        behaviorTree.EnableBehavior();

                        EnemyCurrentStatus = EnemyStatus.ENEMY_MOVEMENT;
                    }).AddTo(this);

                    moveTrigger = true;
                }
                break;
            case EnemyStatus.ENEMY_MOVEMENT:
                if (moveTrigger)
                {
                    //經過一定時間後角色會回到IDLE狀態
                    Observable.Timer(TimeSpan.FromSeconds(Random.Range(timeToDazeValue - timeToDazeThreshold, timeToDazeValue + timeToDazeThreshold))).Subscribe(_ =>
                    {
                        idleTrigger = false;

                        EnemyCurrentStatus = EnemyStatus.ENEMY_IDLE;
                    }).AddTo(this);

                    moveTrigger = false;
                }
                break;
            case EnemyStatus.ENEMY_FIGHT:
                break;
            default:
                break;
        }
    }
    //public void OnGUI()
    //{
    //    // Assign the next External Behavior Tree within the simplified pool.
    //    if (GUILayout.Button("Assign"))
    //    {
    //        behaviorTree.DisableBehavior();
    //        behaviorTree.ExternalBehavior = externalBehaviorTrees[index];
    //        behaviorTree.EnableBehavior();
    //        index = (index + 1) % externalBehaviorTrees.Length;
    //    }
    //}
}
