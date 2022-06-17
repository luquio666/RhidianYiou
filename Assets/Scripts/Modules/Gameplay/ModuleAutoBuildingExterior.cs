using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

[CustomEditor(typeof(ModuleAutoBuildingExterior))]
public class ModuleAutoBuildingExteriorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ModuleAutoBuildingExterior myScript = (ModuleAutoBuildingExterior)target;
        if (GUILayout.Button("CreateBuilding"))
        {
            myScript.CreateBuilding();
        }
        if (GUILayout.Button("ChangeWindows"))
        {
            myScript.ChangeWindows();
        }
        if (GUILayout.Button("ChangeDoors"))
        {
            myScript.ChangeDoor();
        }
        if (GUILayout.Button("Clear"))
        {
            myScript.Clear();
        }
        if (GUILayout.Button("CreateColliders"))
        {
            myScript.CreateColliders();
        }
    }
}

public class ModuleAutoBuildingExterior : Module
{
    public int Height = 3;
    public int Width = 3;
    public int Depth = 3;
    [Space]
    public Sprite[] Roof;
    public Sprite[] Front;
    public Sprite[] Windows;
    public Sprite[] Doors;

    private BoxCollider _col;
    private Rigidbody _rb;
    private const string BuildingLayer = "Foreground";

    public void Clear(string target = null)
    {
        var childCount = this.transform.childCount;
        for (int i = childCount-1; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(target))
            {
                var child = this.transform.GetChild(i);
                if(child.name == target)
                    DestroyImmediate(child.gameObject);
            }
            else 
            {
                var child = this.transform.GetChild(i);
                DestroyImmediate(child.gameObject);
            }
            
        }
    }

    public void CreateBuilding()
    {
        Clear();
        CreateFront();
        CreateRoof();
        CreateWindows();
        CreateDoor();

        CreateColliders();
    }

    public void CreateColliders()
    {
        _rb = _rb == null ? _rb = this.gameObject.AddComponent<Rigidbody>() : _rb;
        _col = _col == null ? _col = this.gameObject.AddComponent<BoxCollider>() : _col;

        _rb.useGravity = false;
        _rb.isKinematic = true;

        _col.size = new Vector3(Width, Depth, 1);
        _col.center = new Vector3(((float)Width - 1) / 2f, ((float)Depth - 1) / 2f, 0);
    }

    private void CreateFront()
    {
        var parent = CreateParent("Front");
        CreateStructure("Front", Front, parent, Width, Height, 0);
    }

    private void CreateRoof()
    {
        var parent = CreateParent("Roof");
        CreateStructure("Roof", Roof, parent, Width, Depth, Height);
    }

    public void ChangeWindows()
    {
        Clear("Windows");
        CreateWindows();
    }
    
    private void CreateWindows()
    {
        var parent = CreateParent("Windows");

        var width = Width;
        var height = Height - 1;

        var windowSprite = Windows[Random.Range(0, Windows.Length)];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                var position = w;
                var window = CreateSpriteRenderer(windowSprite, parent, "Window", position, h + 1);
                window.sortingOrder++;
            }
        }
    }

    public void ChangeDoor()
    {
        Clear("Door");
        CreateDoor();
    }
    
    private void CreateDoor()
    {
        var parent = CreateParent("Door");
        var doorSprite = Doors[Random.Range(0, Doors.Length)];
        if (Width % 2 == 0 && Width > 2)
        {
            var door1 = CreateSpriteRenderer(doorSprite, parent, "Door", Width / 2, 0);
            door1.transform.localScale = new Vector3(-1, 1, 1);
            var door2 = CreateSpriteRenderer(doorSprite, parent, "Door", (Width / 2) - 1, 0);
            door1.sortingOrder++;
            door2.sortingOrder++;
        }
        else
        {
            var door = CreateSpriteRenderer(doorSprite, parent, "Door", Width / 2, 0);
            door.sortingOrder++;
        } 
    }

    private void CreateStructure(string name, Sprite[] buildingSprites, Transform parent, int width, int height, int offsetY)
    {
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                bool left = false;
                bool right = false;
                bool bottom = false;
                bool top = false;
                
                if (w == 0)
                { left = true; }
                if (w == width - 1)
                { right = true; }
                if (h == 0)
                { bottom = true; }
                if (h == height - 1)
                { top = true; }

                var buildingSprite = GetSprite(buildingSprites, left, right, bottom, top);
                CreateSpriteRenderer(buildingSprite, parent, name, w, h + offsetY);
            }
        }
    }

    private Sprite GetSprite(Sprite[] buildingSprites, bool left, bool right, bool bottom, bool top)
    {
        // top left (0)
        if (top && left)
        {
            return buildingSprites[0];
        }
        // top right (2)
        else if (top && right)
        {
            return buildingSprites[2];
        }
        // bottom left (6)
        else if (bottom && left)
        {
            return buildingSprites[6];
        }
        // bottom right (8)
        else if (bottom && right)
        {
            return buildingSprites[8];
        }
        // bottom (7)
        else if (bottom)
        {
            return buildingSprites[7];
        }
        // top (1)
        else if (top)
        {
            return buildingSprites[1];
        }
        // left (3)
        else if (left)
        {
            return buildingSprites[3];
        }
        // right (5)
        else if (right)
        {
            return buildingSprites[5];
        }
        // middle (4)
        else
        {
            return buildingSprites[4];
        }
    }

    private SpriteRenderer CreateSpriteRenderer(Sprite s, Transform parent, string name, float posX, float posY)
    {
        var  go = new GameObject($"{name} ({posX}:{posY})");
        go.transform.SetParent(parent);
        go.transform.localPosition = new Vector3(posX, posY, 0);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = s;
        sr.sortingLayerName = BuildingLayer;

        //first building level always behind character
        if (posY == 0)
            sr.sortingOrder = -2; 
        else
            sr.sortingOrder = 2;

        return sr;
    }

    private Transform CreateParent(string name)
    {
        var go = new GameObject(name);
        go.transform.SetParent(this.transform);
        go.transform.localPosition = new Vector3(0, 0, 0);
        return go.transform;
    }

}