using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPanel : BasePanel
{
    [SerializeField] private Text _potionsAmountText = default;
    [SerializeField] private Text _boostersAmountText = default;
    [SerializeField] private Button _resumeButton = default;
    [SerializeField] private Button _goBackButton = default;

    private GameplayController controller = default;

    #region Base Panel Inheritance

    public void SetupPanel(GameplayController controller)
    {
        base.SetupPanel();
        this.controller = controller;
        _resumeButton.onClick.AddListener(ResumeGameplay);
        _goBackButton.onClick.AddListener(GoBackToMenu);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        _potionsAmountText.text = "x" + RPG_DevTest.GetItemAmount(ItemType.HEAL).ToString();
        _boostersAmountText.text = "x" + RPG_DevTest.GetItemAmount(ItemType.BOOST).ToString();
    }

    public override void ResetPanel()
    {
        ClosePanel();
    }

    #endregion

    /// <summary>
    /// Ends game session and load main menu
    /// </summary>
    private void GoBackToMenu()
    {
        RPG_DevTest.LoadScene(GameScene.HOME, true);
    }

    /// <summary>
    /// Close panel and resume the gameplay
    /// </summary>
    private void ResumeGameplay()
    {
        ClosePanel();
        controller.SetPauseGameState(false);
    }
}
