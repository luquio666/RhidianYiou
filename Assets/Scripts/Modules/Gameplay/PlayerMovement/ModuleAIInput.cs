using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIBehaviour
{
    RANDOM,
    NONE
}

public class ModuleAIInput : Module
{
    public AIBehaviour Behaviour;
    public ModuleMovement MoveMod;

    private float _moveTime;
    private float _moveTimeLeft;

    private void Awake()
    {
        if (MoveMod == null)
            MoveMod = this.GetComponent<ModuleMovement>();
        _moveTime = MoveMod.MoveStepTimer * 4 * Random.Range(0.001f, 0.1f);
        if (Behaviour == AIBehaviour.NONE)
            _moveTime *= 10;
    }

    private void Update()
    {
        if (_moveTimeLeft > 0)
        {
            _moveTimeLeft -= Time.deltaTime;
            return;
        }
        else
        {
            _moveTimeLeft = _moveTime;
        }

        switch (Behaviour)
        {
            case AIBehaviour.RANDOM:
                BehaviourRandom();
                break;
            case AIBehaviour.NONE:
                BehaviourNone();
                break;
            default:
                return;
        }
    }

    private void BehaviourRandom()
    {
        var availableDirection = MoveMod.GellAllAvailableDirections();
        if (availableDirection.Length == 0) return;

        var randomDirection = GetRandomDirection(availableDirection);
        MoveMod.SetTargetMovement(randomDirection);
    }

    private void BehaviourNone()
    {
        var availableDirection = MoveMod.GellAllAvailableDirections();
        if (availableDirection.Length == 0) return;

        var randomDirection = GetRandomDirection(availableDirection);
        MoveMod.SetTargetMovement(randomDirection, false);
    }

    private Vector3 GetRandomDirection(Vector3[] availableDirections)
    {
        return availableDirections[Random.Range(0, availableDirections.Length)];
    }

}
