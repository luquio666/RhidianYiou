using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ModuleInteractableSoil))]
public class ModuleInteractableSoilEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ModuleInteractableSoil myScript = (ModuleInteractableSoil)target;
        if (GUILayout.Button("NEXT GROW STAGE"))
        {
            myScript.NextStage();
        }
        if (GUILayout.Button("RESET STAGE"))
        {
            myScript.ResetStage();
        }
    }
}
#endif

[System.Serializable]
public class SoilStages
{
    public Sprite Soil;
    public Sprite Sprout;
}

public enum Sprouts
{
    CARROT,
    CORN,
    LETTUCE,
    PUMPKIN,
    TOMATO,
    WHEAT
}

public class ModuleInteractableSoil : Module
{

    public Sprouts SproutSelected;
    [Space]
    public SoilStages[] GrowStages;
    public int GrowStageIndex;
    [Space]
    public Sprite Carrot;
    public Sprite Corn;
    public Sprite Lettuce;
    public Sprite Pumpkin;
    public Sprite Tomato;
    public Sprite Wheat;
    [Space]
    public SpriteRenderer Soil;
    public SpriteRenderer Sprout;

    public void NextStage()
    {
        if (GrowStageIndex > GrowStages.Length - 1)
            GrowStageIndex = 0;
        Soil.sprite = GrowStages[GrowStageIndex].Soil;

        if (GrowStageIndex == GrowStages.Length - 1)
        {
            switch (SproutSelected)
            {
                case Sprouts.CARROT:
                    Sprout.sprite = Carrot;
                    break;
                case Sprouts.CORN:
                    Sprout.sprite = Corn;
                    break;
                case Sprouts.LETTUCE:
                    Sprout.sprite = Lettuce;
                    break;
                case Sprouts.PUMPKIN:
                    Sprout.sprite = Pumpkin;
                    break;
                case Sprouts.TOMATO:
                    Sprout.sprite = Tomato;
                    break;
                case Sprouts.WHEAT:
                    Sprout.sprite = Wheat;
                    break;
                default:
                    Sprout.sprite = GrowStages[GrowStageIndex].Sprout;
                    break;
            }
        }
        else
        {
            Sprout.sprite = GrowStages[GrowStageIndex].Sprout;
        }
        
        GrowStageIndex++;
    }

    public void ResetStage()
    {
        GrowStageIndex = 0;
        Soil.sprite = GrowStages[GrowStageIndex].Soil;
        Sprout.sprite = GrowStages[GrowStageIndex].Sprout;
    }
}
