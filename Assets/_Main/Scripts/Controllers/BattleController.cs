using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls turn based battle system
/// </summary>
public class BattleController : MonoBehaviour
{
    [SerializeField] private BattleMainUI _battleUIPanel = default;
    [SerializeField] private InBattleCharacter _player = default;
    [SerializeField] private InBattleCharacter _enemy = default;

    private bool _playerHasChose = false;
    private bool _enemyHasChose = true;

    private void Awake()
    {
        _battleUIPanel.SetupPanel(this);
        _player.SetupCharacter();
        _enemy.SetupCharacter();
        _battleUIPanel.OpenPanel();

        _playerHasChose = false;
        _enemyHasChose = true;

        StartCoroutine(Battle());
    }

    /// <summary>
    /// Control the flow of the battle and set the winner character when it ends
    /// </summary>
    /// <returns></returns>
    private IEnumerator Battle()
    {
        while(true)
        {
            _battleUIPanel.SetButtonOptionActiveState(true);
            _battleUIPanel.UpdateInfoText("True Knight's turn");
            yield return new WaitUntil(() => _playerHasChose);
            if (_enemy.CharacterLife <= 0) 
                break;

            yield return new WaitForSeconds(1f);

            _battleUIPanel.UpdateInfoText("Fake Knight's turn");
            SetEnemyNextAction();
            yield return new WaitUntil(() => _enemyHasChose);
            if (_player.CharacterLife <= 0)
                break;

            yield return new WaitForSeconds(1f);
        }

        SelectWinner();

        yield return new WaitForSeconds(3f);
        RPG_DevTest.LoadScene(GameScene.HOME, true);
    }

    #region Character possible actions

    /// <summary>
    /// Set attack state of any of the character in the scene
    /// </summary>
    /// <param name="isPlayerAttack">Set if is player attack</param>
    /// <returns></returns>
    public IEnumerator Attack(bool isPlayerAttack)
    {
        InBattleCharacter damagerCharacter = isPlayerAttack ? _player : _enemy;
        InBattleCharacter damagedCharacter = isPlayerAttack ? _enemy : _player;

        damagerCharacter.SetCharacterAction(InBattleAction.Attack);
        yield return new WaitForSeconds(0.5f);
        damagedCharacter.SetCharacterAction(InBattleAction.Hit);
        int damageValue = damagerCharacter.GetDamageValue();

        string damagedName = damagedCharacter == _player ? "True Knight" : "Fake Knight";
        _battleUIPanel.UpdateInfoText(damagedName + " received " + damageValue + " of damage points");
        damagedCharacter.SetDamage(damageValue);

        KnightBar damagedKnight = damagedCharacter == _player ? KnightBar.PLAYER : KnightBar.ENEMY;
        _battleUIPanel.UpdateKnightLifeBar(damagedKnight, damagedCharacter.CharacterLife);
        yield return new WaitForSeconds(1f);

        if (damagerCharacter == _player)
        {
            _playerHasChose = true;
            _enemyHasChose = false;
        }
        else
        {
            _enemyHasChose = true;
            _playerHasChose = false;
        }
    }

    /// <summary>
    /// Set defend state of any of the character in the scene
    /// </summary>
    /// <param name="isPlayerDefense">Set if is player defending himself</param>
    /// <returns></returns>
    public IEnumerator Defend(bool isPlayerDefense)
    {
        InBattleCharacter defendingCharacter = isPlayerDefense ? _player : _enemy;
        defendingCharacter.SetCharacterAction(InBattleAction.Defend);
        defendingCharacter.IsDefending = true;
        yield return new WaitForSeconds(0.5f);
        string defendingName = defendingCharacter == _player ? "True Knight" : "Fake Knight";
        _battleUIPanel.UpdateInfoText(defendingName + " is defending himself against his foe next attack");
        yield return new WaitForSeconds(1f);

        if (defendingCharacter == _player)
        {
            _playerHasChose = true;
            _enemyHasChose = false;
        }
        else
        {
            _enemyHasChose = true;
            _playerHasChose = false;
        }
    }

    /// <summary>
    /// Set heal state of any of the character in the scene
    /// </summary>
    /// <param name="isPlayerHealing">Set if is player healing himself</param>
    /// <returns></returns>
    public IEnumerator Heal(bool isPlayerHealing)
    {
        InBattleCharacter healingCharacter = isPlayerHealing ? _player : _enemy;
        bool canHeal = healingCharacter.CanBeHealed();
        string healingName = healingCharacter == _player ? "True Knight" : "Fake Knight";

        if(canHeal)
        {
            healingCharacter.SetCharacterAction(InBattleAction.Heal);
            healingCharacter.HealCharacter();
            yield return new WaitForSeconds(0.5f);

            _battleUIPanel.UpdateInfoText(healingName + " it's healing himself");

            KnightBar healedKnight = healingCharacter == _player ? KnightBar.PLAYER : KnightBar.ENEMY;
            _battleUIPanel.UpdateKnightLifeBar(healedKnight, healingCharacter.CharacterLife);
            yield return new WaitForSeconds(1f);

            if (healingCharacter == _player)
            {
                _playerHasChose = true;
                _enemyHasChose = false;
            }
            else
            {
                _enemyHasChose = true;
                _playerHasChose = false;
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _battleUIPanel.UpdateInfoText(healingName + " can't heal himself");
            yield return new WaitForSeconds(1f);

            if (healingCharacter == _player)
                _battleUIPanel.SetButtonOptionActiveState(true);
            else
            {
                SetEnemyNextAction();
            }
        }
    }
    /// <summary>
    /// Set boost state of any of the character in the scene
    /// </summary>
    /// <param name="playerIsBoosting">Set if is player healing himself</param>
    /// <returns></returns>
    public IEnumerator Boost(bool playerIsBoosting)
    {
        InBattleCharacter boostingCharacter = playerIsBoosting ? _player : _enemy;
        bool canBoost = boostingCharacter.CanBoost();
        string boostingName = boostingCharacter == _player ? "True Knight" : "Fake Knight";

        if(canBoost)
        {
            boostingCharacter.SetCharacterAction(InBattleAction.Boost);
            yield return new WaitForSeconds(0.5f);

            _battleUIPanel.UpdateInfoText(boostingName + " it's boosting himself");
            yield return new WaitForSeconds(1f);

            if (boostingCharacter == _player)
            {
                _playerHasChose = true;
                _enemyHasChose = false;
            }
            else
            {
                _enemyHasChose = true;
                _playerHasChose = false;
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _battleUIPanel.UpdateInfoText(boostingName + " can't boost more than one time");
            yield return new WaitForSeconds(1f);

            if (boostingCharacter == _player)
                _battleUIPanel.SetButtonOptionActiveState(true);
            else
            {
                SetEnemyNextAction();
            }
        }
    }

    #endregion

    /// <summary>
    /// Select next enemy's action while he is alive
    /// </summary>
    private void SetEnemyNextAction()
    {
        int nextActionRange = Random.Range(0, 100);

        if (nextActionRange < 10)
            StartCoroutine(Heal(false));
        else if (nextActionRange >= 10 && nextActionRange < 50)
            StartCoroutine(Attack(false));
        else if (nextActionRange >= 50 && nextActionRange < 90)
            StartCoroutine(Defend(false));
        else
            StartCoroutine(Boost(false));
    }

    /// <summary>
    /// Select the alive character as the winner
    /// </summary>
    private void SelectWinner()
    {
        //TODO: Update singleton variables for win/lose games

        if(_player.CharacterLife > 0)
        {
            _player.SetCharacterAction(InBattleAction.Win);
            _enemy.SetCharacterAction(InBattleAction.Defeat);
            RPG_DevTest.UpdateStatisticParameter(StatisticParameter.WONBATTLES);
            _battleUIPanel.UpdateInfoText("True Knight wins");
        }
        else
        {
            _player.SetCharacterAction(InBattleAction.Defeat);
            _enemy.SetCharacterAction(InBattleAction.Win);
            RPG_DevTest.UpdateStatisticParameter(StatisticParameter.LOSEBATTLES);
            _battleUIPanel.UpdateInfoText("Fake Knight wins");
        }
    }
}
