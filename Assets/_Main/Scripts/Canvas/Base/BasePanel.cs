using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base abstract panel class to override
/// </summary>
public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup _panelCanvasGroup = default;

    [HideInInspector] public bool panelIsOpen = false;

    /// <summary>
    /// Setup canvas group reference, it can be overriden to set aditional and specific setups
    /// </summary>
    public virtual void SetupPanel()
    {
        SetupCanvasGroup();
    }

    /// <summary>
    /// Method to implement in child objects to be called every time the simulator is reset
    /// </summary>
    public abstract void ResetPanel();

    /// <summary>
    /// Force the panel close in case the panel isn't closed
    /// </summary>
    public virtual void ClosePanel()
    {
        if (gameObject.activeInHierarchy)
        {
            panelIsOpen = false;
            SetPanelActive(false);
        }
    }

    /// <summary>
    /// Open panel and set as visible and interactable
    /// </summary>
    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
        SetPanelActive(true);
        panelIsOpen = true;
    }

    /// <summary>
    /// Set gameObject panel activation state
    /// </summary>
    /// <param name="state">Panel must be active or inactive</param>
    protected void SetPanelActive(bool state)
    {
        StartCoroutine(SetPanelVisibility(state));
    }

    /// <summary>
    /// Set an invisible and non-interactable canvas group
    /// </summary>
    private void SetupCanvasGroup()
    {
        _panelCanvasGroup = CheckCanvasGroup();
        _panelCanvasGroup.alpha = 0;
        _panelCanvasGroup.blocksRaycasts = false;
        _panelCanvasGroup.interactable = false;
    }

    /// <summary>
    /// Check if object has a canvas group, if not add a new canvas group component
    /// </summary>
    /// <returns>Return added or got canvas group component</returns>
    private CanvasGroup CheckCanvasGroup()
    {
        CanvasGroup gotCanvasGroup = GetComponent<CanvasGroup>();
        if (gotCanvasGroup == null)
        {
            gotCanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        return gotCanvasGroup;
    }

    /// <summary>
    /// Activate or deactivate the panel visibility and interactions
    /// </summary>
    /// <param name="state">activate or deactivate panel</param>
    /// <returns></returns>
    private IEnumerator SetPanelVisibility(bool state)
    {
        if (state)
        {
            _panelCanvasGroup.blocksRaycasts = state;

            for (float i = 0; i <= 1; i += 0.05f)
            {
                _panelCanvasGroup.alpha = i;
                yield return new WaitForSeconds(0.01f);
            }
            _panelCanvasGroup.alpha = 1;
            _panelCanvasGroup.interactable = state;
        }
        else
        {
            _panelCanvasGroup.interactable = state;

            for (float i = 1; i >= 0; i -= 0.05f)
            {
                _panelCanvasGroup.alpha = i;
                yield return new WaitForSeconds(0.01f);
            }
            _panelCanvasGroup.alpha = 0;
            _panelCanvasGroup.blocksRaycasts = state;
            gameObject.SetActive(state);
        }
    }
}
