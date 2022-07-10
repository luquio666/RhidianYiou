using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModuleGameplayUI : Module
{
    public GameObject NormalInfoParent;
    public GameObject QuestInfoParent;
    [Space]
    public Text RealTime;
    public Text PlayerPos;
    public Text QuestInfo;
    [Space]
    public List<Text> SelectedDescription;
    public GameObject MoreOptionsHighlight;
    public GameObject InventoryHighlight;
    public GameObject InteractHighlight;

    private Coroutine _hideLabelCo;

    private void OnEnable()
    {
        GameEvents.OnPlayerActionSelected += PlayerActionSelected;
        GameEvents.OnQuestEvent_ReackPosition += PlayerNewPosition;
        GameEvents.OnSwapTopInfo += SwapTopInfo;
        GameEvents.OnSetQuestInfo += SetQuestInfo;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerActionSelected -= PlayerActionSelected;
        GameEvents.OnQuestEvent_ReackPosition -= PlayerNewPosition;
        GameEvents.OnSwapTopInfo -= SwapTopInfo;
        GameEvents.OnSetQuestInfo -= SetQuestInfo;
    }

    private void Update()
    {
        DateTime currentTime = System.DateTime.Now;
        RealTime.text = $"{currentTime.Hour:00}:{currentTime.Minute:00}";
    }

    private void SetQuestInfo(string info)
    {
        QuestInfo.text = info;
        QuestInfoParent.SetActive(true);
        NormalInfoParent.SetActive(false);
    }

    private void SwapTopInfo()
    {
        bool questInfoActive = QuestInfoParent.activeInHierarchy;

        QuestInfoParent.SetActive(!questInfoActive);
        NormalInfoParent.SetActive(questInfoActive);
    }

    private void PlayerNewPosition(Vector2 pos)
    {
        PlayerPos.text = $"n:{pos.y}\nw:{pos.x}";
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
