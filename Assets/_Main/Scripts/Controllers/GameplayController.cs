using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control main gameplay flow and setup principal game objects of the scene
/// </summary>
public class GameplayController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player = default;
    [SerializeField] private EnemyMovement _enemy = default;
    [SerializeField] private ItemController _itemController = default;
    [SerializeField] private ItemsPanel _itemPanel = default;

    private bool canSetPause = true;

    private void Awake()
    {
        _player.SetupCharacter();
        _enemy.SetupCharacter(_player.gameObject, OnPlayerCaught);
        _itemController.SetupItems();
        _itemPanel.SetupPanel(this);
        
        canSetPause = true;
    }

    /// <summary>
    /// Method that starts battle when the player is caught by enemy
    /// </summary>
    private void OnPlayerCaught()
    {
        _player.StopCharacter();
        _enemy.StopCharacter();
        RPG_DevTest.LoadScene(GameScene.BATTLE, true);
    }

    /// <summary>
    /// Controls pause state
    /// </summary>
    /// <param name="setPause">gameplay is paused or resumed?</param>
    public void SetPauseGameState(bool setPause)
    {
        if(setPause)
        {
            _itemPanel.OpenPanel();
            _player.UpdateMovementState(false);
            _enemy.UpdateMovementState(false);
            canSetPause = false;
        }
        else
        {
            _player.UpdateMovementState(true);
            _enemy.UpdateMovementState(true);
            canSetPause = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && canSetPause)
        {
            SetPauseGameState(true);
        }
    }
}
