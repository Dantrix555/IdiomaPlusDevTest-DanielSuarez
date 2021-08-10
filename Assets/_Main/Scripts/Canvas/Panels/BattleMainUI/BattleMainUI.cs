using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KnightBar { PLAYER, ENEMY }

/// <summary>
/// Control battle UI, its options and displayed info
/// </summary>
public class BattleMainUI : BasePanel
{
    [SerializeField] private Slider _enemyLifeBar = default;
    [SerializeField] private Slider _playerLifeBar = default;
    [SerializeField] private Text _infoText = default;
    [SerializeField] private Button _attackButton = default;
    [SerializeField] private Button _defenseButton = default;
    [SerializeField] private Button _healButton = default;
    [SerializeField] private Button _boostButton = default;

    private BattleController battleController = default;
    
    #region Base Panel Inheritance

    public void SetupPanel(BattleController battleController)
    {
        base.SetupPanel();
        this.battleController = battleController;
        _infoText.text = "";
        UpdateKnightLifeBar(KnightBar.PLAYER, 100);
        UpdateKnightLifeBar(KnightBar.ENEMY, 100);

        _attackButton.onClick.AddListener(SetAttackAction);
        _defenseButton.onClick.AddListener(SetDefenseAction);
        _healButton.onClick.AddListener(SetHealAction);
        _boostButton.onClick.AddListener(SetBoostAction);
        SetButtonOptionActiveState(false);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        StartCoroutine(ActiveButtonsWithDelay());
    }

    public override void ResetPanel()
    {
        ClosePanel();
    }

    public override void ClosePanel()
    {
        SetButtonOptionActiveState(false);
        base.ClosePanel();
    }

    #endregion

    /// <summary>
    /// Update info text to custom text
    /// </summary>
    /// <param name="newInfo">Custom text to be displayed</param>
    public void UpdateInfoText(string newInfo)
    {
        _infoText.text = newInfo;
    }

    /// <summary>
    /// Update displayed knight life bar to a specified value
    /// </summary>
    /// <param name="knightBarToUpdate">Enum knight bar value to be updated</param>
    /// <param name="newLifeValue">Life bar updated value</param>
    public void UpdateKnightLifeBar(KnightBar knightBarToUpdate, int newLifeValue)
    {
        Slider barToUpdate = knightBarToUpdate == KnightBar.PLAYER ? _playerLifeBar : _enemyLifeBar;
        barToUpdate.value = newLifeValue;
    }

    /// <summary>
    /// Activate option buuttons with a few seconds delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActiveButtonsWithDelay()
    {
        yield return new WaitForSeconds(1f);
        SetButtonOptionActiveState(true);
    }

    /// <summary>
    /// Set a new interactable state to option buttons
    /// </summary>
    /// <param name="newActiveState">New interactable state of the buttons</param>
    public void SetButtonOptionActiveState(bool newActiveState)
    {
        _attackButton.interactable = newActiveState;
        _defenseButton.interactable = newActiveState;
        _healButton.interactable = newActiveState;
        _boostButton.interactable = newActiveState;
    }

    /// <summary>
    /// Set attack state of the player
    /// </summary>
    private void SetAttackAction()
    {
        SetButtonOptionActiveState(false);
        StartCoroutine(battleController.Attack(true));
    }

    /// <summary>
    /// Set defense state of the player
    /// </summary>
    private void SetDefenseAction()
    {
        SetButtonOptionActiveState(false);
        StartCoroutine(battleController.Defend(true));
    }

    /// <summary>
    /// Set heal state of the player
    /// </summary>
    private void SetHealAction()
    {
        SetButtonOptionActiveState(false);
        StartCoroutine(battleController.Heal(true));
    }

    /// <summary>
    /// Set boost state of the player
    /// </summary>
    private void SetBoostAction()
    {
        SetButtonOptionActiveState(false);
        StartCoroutine(battleController.Boost(true));
    }
}
