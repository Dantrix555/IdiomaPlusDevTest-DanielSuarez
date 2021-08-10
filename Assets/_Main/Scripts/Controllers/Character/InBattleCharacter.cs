using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InBattleAction { Attack, Hit, Defeat, Win, Idle, Heal, Boost, Defend }

/// <summary>
/// Basic in battle character controller
/// </summary>
public class InBattleCharacter : BaseCharacter
{
    private int _characterLife = 100;
    public int CharacterLife => _characterLife;

    private bool _isDefending = false;
    public bool IsDefending { get => _isDefending; set => _isDefending = value; }

    private int _potions = 1;
    private bool _canBoost = true;
    private bool _hasBoost = false;

    #region Base Character Inheritance

    public override void SetupCharacter()
    {
        base.SetupCharacter();

        _characterLife = 100;
        _isDefending = false;

        _potions = 1;
        _canBoost = true;
        _hasBoost = false;
    }

    protected override void MoveCharacter()
    {
        //
    }

    #endregion

    /// <summary>
    /// Set character spedific action as a trigger
    /// </summary>
    /// <param name="action">Trigger as action</param>
    public void SetCharacterAction(InBattleAction action)
    {
        SetCustomTriggerAnimation(action.ToString());
    }

    /// <summary>
    /// Calculate damage value with base 10 + random between 0 and 3 + boost if character has
    /// </summary>
    /// <returns>Total damage to set to an enemy</returns>
    public int GetDamageValue()
    {
        int damage = 10 + Random.Range(0, 3);

        if(_isDefending)
        {
            damage -= Random.Range(0, 7);
            _isDefending = false;
        }

        if(_hasBoost)
        {
            damage += 5;
            _hasBoost = false;
        }

        return damage;
    }

    /// <summary>
    /// Set damage value to character
    /// </summary>
    /// <param name="damage">Damage points to subtract to life points</param>
    public void SetDamage(int damage)
    {
        _characterLife -= damage;

        if (_characterLife <= 0)
            _characterLife = 0;
    }

    /// <summary>
    /// Check if character can boost it's damage
    /// </summary>
    /// <returns>Character boost posibility</returns>
    public bool CanBoost()
    {
        if(_canBoost)
        {
            _canBoost = !_canBoost;
            _hasBoost = true;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check if character has potions
    /// </summary>
    /// <returns>Character heal posibility</returns>
    public bool CanBeHealed()
    {
        if(_potions > 0)
        {
            _potions--;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Heal 20 life points of the character
    /// </summary>
    public void HealCharacter()
    {
        _characterLife += 20;

        if (_characterLife > 100)
            _characterLife = 100;
    }
}
