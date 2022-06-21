using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModuleGameplayUI : Module
{
    public List<Text> SelectedDescription;
    public GameObject MoreOptionsHighlight;
    public GameObject InventoryHighlight;
    public GameObject InteractHighlight;

    private Coroutine _hideLabelCo;

    private void OnEnable()
    {
        GameEvents.OnPlayerActionSelected += PlayerActionSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerActionSelected -= PlayerActionSelected;
    }

    private void PlayerActionSelected(PlayerAction pa)
    {
        UnselectAll();
        switch (pa)
        {
            case PlayerAction.MORE_OPTIONS:
                MoreOptionsHighlight.SetActive(true);
                break;
            case PlayerAction.INVENTORY:
                InventoryHighlight.SetActive(true);
                break;
            case PlayerAction.INTERACT:
                InteractHighlight.SetActive(true);
                break;
            default:
                break;
        }

        string description = pa.ToString().Replace('_', ' ').ToLower();
        SelectedDescription.ForEach(x => { x.gameObject.SetActive(true); x.text = description; });

        if (_hideLabelCo != null)
            StopCoroutine(_hideLabelCo);
        _hideLabelCo = StartCoroutine(HideLabelCo());
    }

    IEnumerator HideLabelCo()
    {
        yield return new WaitForSeconds(1f);
        SelectedDescription.ForEach(x => x.gameObject.SetActive(false));
    }

    private void UnselectAll()
    {
        MoreOptionsHighlight.SetActive(false);
        InventoryHighlight.SetActive(false);
        InteractHighlight.SetActive(false);
    }

}
