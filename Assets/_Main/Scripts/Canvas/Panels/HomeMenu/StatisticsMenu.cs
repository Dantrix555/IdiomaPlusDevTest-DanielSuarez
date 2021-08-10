using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control the UI components inside statistics panel
/// </summary>
public class StatisticsMenu : BasePanel
{
    [SerializeField] private Text _gameSessionsText = default;
    [SerializeField] private Text _wonBattlesText = default;
    [SerializeField] private Text _loseBattlesText = default;
    [SerializeField] private Button _goBackButton = default;

    private HomeMenu firstlineParent = default;

    #region Base Panel Inheritance

    public void SetupPanel(HomeMenu firstlineReference)
    {
        base.SetupPanel();

        firstlineParent = firstlineReference;

        _gameSessionsText.text = "";
        _wonBattlesText.text = "";
        _loseBattlesText.text = "";
        _goBackButton.onClick.AddListener(GoBackMethod);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        _gameSessionsText.text = "<b>Game Sessions: </b>" + RPG_DevTest.GetStatisticParameter(StatisticParameter.SESSIONS).ToString();
        _wonBattlesText.text = "<b>Won battles: </b>" + RPG_DevTest.GetStatisticParameter(StatisticParameter.WONBATTLES).ToString();
        _loseBattlesText.text = "<b>Lost battles: </b>" + RPG_DevTest.GetStatisticParameter(StatisticParameter.LOSEBATTLES).ToString();
    }

    public override void ResetPanel()
    {
        ClosePanel();
    }

    #endregion

    /// <summary>
    /// Return to main menu panel and close statistics panel
    /// </summary>
    private void GoBackMethod()
    {
        firstlineParent.MainMenuPanel.OpenPanel();
        ClosePanel();
    }
}
