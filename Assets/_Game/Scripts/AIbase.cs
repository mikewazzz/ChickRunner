using System.Collections;
using System.Collections.Generic;
using _Game.Prefabs.Resources.Levels.Base;
using Amsterdam.Managers;
using UnityEngine;
using UnityEngine.AI;

public class AIbase : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform finishTarget;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (LevelManager.Instance.CurrentLevel.state   == Level.State.Started)
        {
            _agent.SetDestination(finishTarget.position);
        }
    }
}
