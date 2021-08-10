# IdiomaPlusDevTest-DanielSuarez
This repo contains an RPG project made for a Dev Test, in this case for the company IdiomaPlus. 

## What's this repo
This test is basicaly an JRPG simple prototype, including turn based combat system. In the overworld player can explore the scenario and collect two variety of items, potions and booster for combats, when the player collides with an enemy an encounter starts, in the encounter there are 4 posible commands to choose, attack, defend, heal and boost (the same options are available for the enemy). You can heal or boost yourself according the colected potions and boosters, the potions restore 20 life points and booster increase your damage by 5 points.

## What I've made in this test

* I've created an auto save system for data like, won or lose combats
 
 ```C#
 using System;
 using UnityEngine;

 public enum StatisticParameter { SESSIONS, WONBATTLES, LOSEBATTLES }

 public class MySingleton : MonoBehaviour
 {
    /// <summary>
    /// Return actual value of a custom statistic parameter
    /// </summary>
    /// <param name="parameter">statistic value to get</param>
    public static int GetStatisticParameter(StatisticParameter parameter)
    {
        return PlayerPrefs.GetInt(parameter.ToString());
    }

    /// <summary>
    /// Update a custom statistic value
    /// </summary>
    /// <param name="parameter">statistic value to update</param>
    public static void UpdateStatisticParameter(StatisticParameter parameter)
    {
        int actualParameterValue = PlayerPrefs.GetInt(parameter.ToString());

        PlayerPrefs.SetInt(parameter.ToString(), actualParameterValue + 1);
    }
 }
 
```

* I've created a loading screen system with clean transitions, here is an example

```C#
    /// <summary>
    /// Activate loading bar in screen coroutine
    /// </summary>
    /// <param name="elementsToLoad">How many scenes needs to load asyncronicaly</param>
    public void ActivateLoadingUpdate(List<AsyncOperation> elementsToLoad)
    {
        StartCoroutine(UpdateLoadingValue(elementsToLoad));
    }

    /// <summary>
    /// Update loading scene value
    /// </summary>
    /// <param name="elementsToLoad">How many async operations must load and wait</param>
    /// <returns></returns>
    private IEnumerator UpdateLoadingValue(List<AsyncOperation> elementsToLoad)
    {
        float totalProgressValue;
        for(int i = 0; i < elementsToLoad.Count; i++)
        {
            while (!elementsToLoad[i].isDone)
            {
                totalProgressValue = 0;

                foreach (AsyncOperation operations in elementsToLoad)
                    totalProgressValue += operations.progress;

                totalProgressValue = (totalProgressValue / elementsToLoad.Count) * 100f;
                _loadingSlider.value = Mathf.RoundToInt(totalProgressValue);

                yield return null;
            }
        }
        _loadingSlider.value = 100;

        yield return new WaitForSeconds(1f);

        _loadingSlider.value = 0f;
        ClosePanel();
    }
```

* I've implemented abstractions for classes like panels or characters, below an example:

```C#
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

    /// <summary>
    /// Disable player movement to avoid visual weird moves
    /// </summary>
    public void StopCharacter()
    {
        UpdateMovementState(false);
    }

    /// <summary>
    /// Set movement state according to desired
    /// </summary>
    /// <param name="canMove">Determine if character can move or not</param>
    public void UpdateMovementState(bool canMove)
    {
        characterCanMove = canMove;
    }

    /// <summary>
    /// Move method to be implemented by child classes
    /// </summary>
    protected abstract void MoveCharacter();
}
```

* I've implemented things like an inventory and combat system, and more stuff you can checkout in the repo.

This is my first time doing an JRPG like project, meaning probably, that project need to be improved, if you think so, don't worry about forking my project or just to share your solution ;)

The project uses external resources for animation and 3D models, if you're interested, please don't hesitate to checkout this pages, this resources are great, please remember to credit to original author of this cool assets:

[Mixamo Character Model and Anim movement](https://www.mixamo.com/#/?page=2&query=&type=Character/ "Mixamo Character Model and Anim movement").
