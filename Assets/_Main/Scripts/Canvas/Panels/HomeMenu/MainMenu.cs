using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control the UI components inside main menu panel
/// </summary>
public class MainMenu : BasePanel
{
    [SerializeField] private Button _startGameButton = default;
    [SerializeField] private Button _statisticsButton = default;
    [SerializeField] private Button _closeGameButton = default;

    private HomeMenu firstlineParent = default;

    #region Base Panel Inheritance

    public void SetupPanel(HomeMenu firstlineReference)
    {
        base.SetupPanel();

        firstlineParent = firstlineReference;

        _startGameButton.onClick.AddListener(StartGame);
        _statisticsButton.onClick.AddListener(OpenStatisticsPanel);
        _closeGameButton.onClick.AddListener(CloseGame);
    }

    public override void ResetPanel()
    {
        //
    }

    #endregion

    /// <summary>
    /// Load gameplay scene async
    /// </summary>
    private void StartGame()
    {
        RPG_DevTest.UpdateStatisticParameter(StatisticParameter.SESSIONS);
        RPG_DevTest.LoadScene(GameScene.GAMEPLAY, true);
    }

    /// <summary>
    /// Open statistics panel and close main menu panel
    /// </summary>
    private void OpenStatisticsPanel()
    {
        firstlineParent.StatisticsPanel.OpenPanel();
        ClosePanel();
    }

    /// <summary>
    /// Close the game
    /// </summary>
    private void CloseGame()
    {
        Application.Quit();
    }
}
