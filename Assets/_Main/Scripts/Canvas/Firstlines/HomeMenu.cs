using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the UI flow in the main menu scene
/// </summary>
public class HomeMenu : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenuPanel = default;
    [SerializeField] private StatisticsMenu _statisticsPanel = default;

    public MainMenu MainMenuPanel => _mainMenuPanel;
    public StatisticsMenu StatisticsPanel => _statisticsPanel;

    private void Awake()
    {
        _mainMenuPanel.SetupPanel(this);
        _statisticsPanel.SetupPanel(this);
        _mainMenuPanel.OpenPanel();
        _statisticsPanel.ClosePanel();
    }
}
