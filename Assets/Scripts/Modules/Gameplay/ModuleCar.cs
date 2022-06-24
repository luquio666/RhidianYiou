using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ModuleCar : MonoBehaviour
{
    public SpriteRenderer Car;
    public BoxCollider CarCollider;
    public Sprite Up, Down, Left, Right;
    [Space]
    public float MinMoveThreshold = 0f;

    private Vector3 _lastPosition;

    private void Awake()
    {
        _lastPosition = this.transform.position;
        Car = this.GetComponent<SpriteRenderer>();
        CarCollider = this.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Car == null || CarCollider == null)
            return;

        if (this.transform.position != _lastPosition)
        {
            ApplySpriteDirection();
            _lastPosition = this.transform.position;
        }
    }

    private void ApplySpriteDirection()
    {
        var xmove = this.transform.position.x - _lastPosition.x;
        var ymove = this.transform.position.y - _lastPosition.y;

        var xmoveNorm = Mathf.Abs(xmove);
        var ymoveNorm = Mathf.Abs(ymove);

        // horizontal movement
        if (xmoveNorm > ymoveNorm && xmoveNorm > MinMoveThreshold)
        {
            CarCollider.center = new Vector3(0, -.25f, 0);
            CarCollider.size = new Vector3(3f, 1.5f, 1f);

            if (xmove > 0)
            {
                Car.sprite = Right;
            }
            else
            {
                Car.sprite = Left;
            }
        }

        // vertical movement
        else if (xmoveNorm < ymoveNorm && ymoveNorm > MinMoveThreshold)
        {
            CarCollider.center = new Vector3(0, 0, 0);
            CarCollider.size = new Vector3(2f, 3f, 1f);

            if (ymove > 0)
            {
                Car.sprite = Up;
            }
            else
            {
                Car.sprite = Down;
            }
        }
        else 
        {
            //nothing yet
        }
    }
}
