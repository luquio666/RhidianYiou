using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ModuleMovement))]
public class ModuleMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ModuleMovement myScript = (ModuleMovement)target;
        if (GUILayout.Button("UPDATE SPRITE RENDERER"))
        {
            myScript.UpdateSpriteRenderer();
        }
    }
}
#endif

public class ModuleMovement : Module
{
    public SpriteRenderer Sr;
    public Sprite[] AnimSprites;
    [Space]
    public float MoveStepTimer = .25f;

    private Sprite[] _backSeq = new Sprite[4];
    private Sprite[] _fowardSeq = new Sprite[4];
    private Sprite[] _rightSeq = new Sprite[4];
    private Sprite[] _leftSeq = new Sprite[4];
    private Vector3 _lastDirection;

    Vector3 _targetMovement;
    int _animIndex;
    float _moveTimer;
    

    private void Awake()
    {
        ConfigSpriteSequences();
        _animIndex = 4;
        _lastDirection = Vector3.left;
    }

    private void ConfigSpriteSequences()
    {
        int index = 0;
        for (int i = 0; i < _backSeq.Length; i++)
        {
            if (i == _backSeq.Length - 1)
            {
                _backSeq[i] = AnimSprites[index - 2];
            }
            else
            {
                _backSeq[i] = AnimSprites[index];
                index++;
            }
        }

        for (int i = 0; i < _fowardSeq.Length; i++)
        {
            if (i == _fowardSeq.Length - 1)
            {
                _fowardSeq[i] = AnimSprites[index - 2];
            }
            else
            {
                _fowardSeq[i] = AnimSprites[index];
                index++;
            }
        }

        for (int i = 0; i < _rightSeq.Length; i++)
        {
            if (i == _rightSeq.Length - 1)
            {
                _rightSeq[i] = AnimSprites[index - 2];
            }
            else
            {
                _rightSeq[i] = AnimSprites[index];
                index++;
            }
        }

        for (int i = 0; i < _leftSeq.Length; i++)
        {
            if (i == _leftSeq.Length - 1)
            {
                _leftSeq[i] = AnimSprites[index - 2];
            }
            else
            {
                _leftSeq[i] = AnimSprites[index];
                index++;
            }
        }
    }

    public void SetTargetMovement(Vector3 dir, bool applyMovement = true)
    {
        if (_animIndex < 4)
            return;
        
        if (CanMoveInDirection(dir, true) && applyMovement)
        {
            _animIndex = 0;
            _targetMovement = dir;
            _lastDirection = dir;
        }
        else 
        {
            var directionSprites = GetSequenceByTarget(dir);
            Sr.sprite = directionSprites[^1];
            _lastDirection = dir;
        }
    }

    public void UpdateSpriteRenderer()
    {
        Sr.sprite = AnimSprites[1];
    }

    private void FixedUpdate()
    {
        if (_animIndex >= 4)
            return;

        if (_moveTimer > 0)
        {
            _moveTimer -= Time.deltaTime;
        }
        else
        {
            _lastDirection = _targetMovement;
            _moveTimer = MoveStepTimer;
            this.transform.position += _targetMovement / 4;
            Sr.sprite = GetSequenceByTarget(_targetMovement)[_animIndex];
            _animIndex++;
            if (_animIndex == 4)
            {
                if (this.name == "Player")
                    GameEvents.QuestEvent_ReackPosition(new Vector2(this.transform.position.x, this.transform.position.y));
                _savedSlot.SetActive(false);
            }
        }
    }

    public Vector3[] GellAllAvailableDirections()
    {
        List<Vector3> result = new List<Vector3>();
        if (CanMoveInDirection(Vector3.up))
            result.Add(Vector3.up);
        if (CanMoveInDirection(Vector3.down))
            result.Add(Vector3.down);
        if (CanMoveInDirection(Vector3.left))
            result.Add(Vector3.left);
        if (CanMoveInDirection(Vector3.right))
            result.Add(Vector3.right);
        return result.ToArray();
    }

    GameObject _savedSlot;

    public bool CanMoveInDirection(Vector3 direction, bool saveSlot = false)
    {
        bool result = Physics.Linecast(transform.position, transform.position + direction) == false;
        if (result == true && saveSlot)
        {
            if (_savedSlot == null)
            {
                _savedSlot = new GameObject(this.gameObject.name + "(savedSlot)");
                _savedSlot.AddComponent<BoxCollider>();
            }
            _savedSlot.SetActive(true);
            _savedSlot.transform.position = this.transform.position + direction;
        }
        return result;
    }

    public ModuleInteractable GetInteractable()
    {
        var hits = Physics.RaycastAll(transform.position, _lastDirection, 1f);
        foreach (var hit in hits)
        {
            var interactable = hit.transform.GetComponent<ModuleInteractable>();

            if (interactable != null && hit.distance < 1f)
            {
                return interactable;
            }
        }
        return null;
    }

    private Sprite[] GetSequenceByTarget(Vector3 target)
    {
        if (target == Vector3.up)
            return _fowardSeq;
        else if (target == Vector3.down)
            return _backSeq;
        else if (target == Vector3.right)
            return _rightSeq;
        else
            return _leftSeq;
    }

    private void OnDrawGizmos()
    {
        if (_lastDirection == Vector3.zero)
            _lastDirection = Vector3.left;
        Gizmos.color = Color.green;

        // Draw a line and sphere to the position character is facing
        Gizmos.DrawLine(transform.position, transform.position + _lastDirection);
        Gizmos.DrawWireSphere(transform.position + _lastDirection, 0.25f);
    }
}
