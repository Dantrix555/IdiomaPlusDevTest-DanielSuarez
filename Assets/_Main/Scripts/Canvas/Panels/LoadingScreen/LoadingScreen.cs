using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control loading screen display and loading bar
/// </summary>
public class LoadingScreen : BasePanel
{
    [SerializeField] private Text _loadingText = default;
    [SerializeField] private Slider _loadingSlider = default;

    #region Base Panel Inheritance

    public override void SetupPanel()
    {
        base.SetupPanel();
        _loadingSlider.value = 0f;
    }

    
    
    public override void ResetPanel()
    {
        _loadingSlider.value = 0f;
        ClosePanel();
    }

    #endregion

    /// <summary>
    /// Activate loading bar in screen coroutine
    /// </summary>
    /// <param name="elementsToLoad">How many async operations must load and wait</param>
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
}
