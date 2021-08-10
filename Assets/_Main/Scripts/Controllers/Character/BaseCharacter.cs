using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class with the base functions of any game character
/// </summary>
public abstract class BaseCharacter : MonoBehaviour
{
    [Header("Base components")]
    [SerializeField] protected Rigidbody characterRigidbody = default;
    [SerializeField] protected Animator characterAnimator = default;

    protected bool characterCanMove = false;
    protected bool playerIsReady = false;

    /// <summary>
    /// Set custom trigger type animation
    /// </summary>
    /// <param name="triggerKey">Trigger key to animate character</param>
    public void SetCustomTriggerAnimation(string triggerKey)
    {
        characterAnimator.SetTrigger(triggerKey);
    }
    
    /// <summary>
    /// Set float value to control walk and run animations
    /// </summary>
    /// <param name="speedValue">Custom float value which triggers a walk animation state</param>
    public void SetSpeedValueAnimation(float speedValue)
    {
        characterAnimator.SetFloat("Speed", speedValue);
    }

    /// <summary>
    /// Check if a character have a rigidbody and animator, and set it's default values
    /// </summary>
    public virtual void SetupCharacter()
    {
        if (characterAnimator == null)
            characterAnimator = gameObject.AddComponent<Animator>();

        if (characterRigidbody == null)
            characterRigidbody = gameObject.AddComponent<Rigidbody>();

        SetCustomTriggerAnimation("Idle");
        SetSpeedValueAnimation(0f);
        characterRigidbody.velocity = Vector3.zero;
        characterCanMove = false;
    }

    public void StopCharacter()
    {
        characterCanMove = false;
    }

    /// <summary>
    /// Move method to be implemented by child classes
    /// </summary>
    protected abstract void MoveCharacter();
}
