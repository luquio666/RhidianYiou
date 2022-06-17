using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePlayerInput : Module
{
    public ModuleMovement MoveMod;

    private void Awake()
    {
        if (MoveMod == null)
            MoveMod = this.GetComponent<ModuleMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MoveMod.SetTargetMovement(Vector3.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveMod.SetTargetMovement(Vector3.down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveMod.SetTargetMovement(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveMod.SetTargetMovement(Vector3.right);
        }
    }
}