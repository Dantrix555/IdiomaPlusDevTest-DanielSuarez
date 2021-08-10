using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control main gameplay flow
/// </summary>
public class GameplayController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player = default;
    [SerializeField] private EnemyMovement _enemy = default;

    private void Awake()
    {
        _player.SetupCharacter();
        _enemy.SetupCharacter(_player.gameObject, OnPlayerCaught);
    }

    private void OnPlayerCaught()
    {
        _player.StopCharacter();
        _enemy.StopCharacter();
        RPG_DevTest.LoadScene(GameScene.BATTLE, true);
    }
}
