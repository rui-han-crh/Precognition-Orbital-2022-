using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CombatSystem.Entities;
using Facades;
using Managers;

public class InformationUIManager : MonoBehaviour
{
    private static readonly int ONE_HUNDRED_PERCENT = 100;
    private static readonly string TWO_DECIMAL_PLACES = "n2";

    private static InformationUIManager instance;

    public static InformationUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InformationUIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private CommandButtonsBehaviour commandButtonsBehaviour;

    [SerializeField]
    private CharacterStatsUIBehaviour opponentUIBehaviour;

    [SerializeField]
    private CharacterStatsUIBehaviour actingUnitUIBehaviour;

    public TMP_Text TimeNeededText => commandButtonsBehaviour.TimeText;
    public TMP_Text ActionPointText => commandButtonsBehaviour.ActionPointText;

    [SerializeField]
    private TMP_Text resultantDamageText;

    [SerializeField]
    private TMP_Text chanceToHitText;


    private void Awake()
    {
        opponentUIBehaviour.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        TurnAllUIOff();
    }

    public void SetOpponentDetails(UnitOld unit)
    {
        opponentUIBehaviour.SetName(unit.Name);
        opponentUIBehaviour.SetAvatar(unit.CharacterHeadAvatar);
        opponentUIBehaviour.SetUnitStats(unit);
    }

    public void SetOpponentDetails(Unit unit)
    {
        opponentUIBehaviour.SetName(unit.Name);
        opponentUIBehaviour.SetAvatar(UnitManager.Instance[unit.Identity].CharacterAvatar);
        opponentUIBehaviour.SetUnitStats(unit);
    }

    public void SetCharacterDetails(Unit unit)
    {
        actingUnitUIBehaviour.SetName(unit.Name);
        actingUnitUIBehaviour.SetAvatar(UnitManager.Instance[unit.Identity].CharacterAvatar);
        actingUnitUIBehaviour.SetUnitStats(unit);
    }

    public void SetResultantDamageDealt(int resultantDamage)
    {
        resultantDamageText.text = resultantDamage.ToString();
    }

    public void SetChanceToHitText(float chanceToHit)
    {
        chanceToHitText.text = (chanceToHit * ONE_HUNDRED_PERCENT).ToString(TWO_DECIMAL_PLACES);
    }

    public void WaitButtonActivated()
    {
        TurnAllUIOff();
        commandButtonsBehaviour.SetButtonActive(GameManagerOld.Command.Wait, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.Time, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.ActionPoints, true);
    }

    public void MoveButtonActivated()
    {
        TurnAllUIOff();
        commandButtonsBehaviour.SetButtonActive(GameManagerOld.Command.Movement, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.Time, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.ActionPoints, true);
    }

    public void CombatButtonActivated()
    {
        TurnAllUIOff();
        commandButtonsBehaviour.SetButtonActive(GameManagerOld.Command.Attack, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.Time, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.ActionPoints, true);
    }

    public void OverwatchButtonActivated()
    {
        TurnAllUIOff();

        TimeNeededText.text = (OverwatchRequestOld.TIME_CONSUMED).ToString();

        commandButtonsBehaviour.SetButtonActive(GameManagerOld.Command.Overwatch, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.Time, true);
        commandButtonsBehaviour.SetPromptActive(CommandButtonsBehaviour.Prompt.ActionPoints, true);
    }

    public void TurnAllUIOff()
    {
        SetAllTextToDefault();
        commandButtonsBehaviour.DisableAllUI();
    }

    public void SetAllTextToDefault()
    {
        commandButtonsBehaviour.SetPromptTextToDefault();
    }

    public void SetTimeAndAPRequiredText(int timeNeeded, int apNeeded)
    {
        commandButtonsBehaviour.TimeText.text = timeNeeded.ToString();
        commandButtonsBehaviour.ActionPointText.text = apNeeded.ToString();
    }

    public void SetTimeAndAPRequiredText(int apNeeded)
    {
        commandButtonsBehaviour.TimeText.text = Mathf.CeilToInt((float)apNeeded / GameManagerOld.Instance.CurrentUnit.Speed).ToString();
        commandButtonsBehaviour.ActionPointText.text = apNeeded.ToString();
    }
}
