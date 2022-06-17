using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ModuleFollowPosition : Module
{
    public Transform TargetToFollow;
    public Vector2 Offset;

    void Update()
    {
        if (TargetToFollow == null) return;

        var offset = new Vector3(Offset.x, Offset.y, this.transform.position.z);
        this.transform.position = TargetToFollow.position + offset;
    }
}
