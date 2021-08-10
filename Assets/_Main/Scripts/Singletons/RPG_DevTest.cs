using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ItemType { HEAL, BOOST }

public enum GameScene { INIT, HOME, GAMEPLAY, BATTLE }

public enum StatisticParameter { SESSIONS, WONBATTLES, LOSEBATTLES }

/// <summary>
/// Principal game singleton, use for principal functions
/// </summary>
public class RPG_DevTest : BASESingleton<RPG_DevTest>
{
    protected RPG_DevTest() { }

    [SerializeField] private LoadingScreen _loadingScreenPanel = default;

    private int _actualSceneIndex = -1;
    private List<AsyncOperation> _scenesLoading;

    /// <summary>
    /// Start game loading home scene aditively
    /// </summary>
    public static void StartGame()
    {
        SceneManager.LoadSceneAsync((int)GameScene.HOME, LoadSceneMode.Additive);
        Instance._actualSceneIndex = (int)GameScene.HOME;
        Instance._loadingScreenPanel.SetupPanel();
        Instance._loadingScreenPanel.ClosePanel();
    }

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

    /// <summary>
    /// Return actual amount of a custom item
    /// </summary>
    /// <param name="item">Item type amount to get</param>
    public static int GetItemAmount(ItemType item)
    {
        return PlayerPrefs.GetInt(item.ToString());
    }

    /// <summary>
    /// Update a custom item amount
    /// </summary>
    /// <param name="item">item type amount to update</param>
    /// <param name="addItem">Add item state</param>
    public static void UpdateItemAmount(ItemType item, bool addItem)
    {
        int actualParameterValue = PlayerPrefs.GetInt(item.ToString());
        actualParameterValue = addItem ? actualParameterValue + 1 : actualParameterValue - 1;

        if (actualParameterValue < 0)
            actualParameterValue = 0;

        PlayerPrefs.SetInt(item.ToString(), actualParameterValue);
    }

    /// <summary>
    /// Load a custom scene and control if scene must be loaded aditively or not
    /// </summary>
    /// <param name="sceneToLoad">Scene enum to load</param>
    /// <param name="loadSceneAditively">Controls if scene must be loaded aditive or not</param>
    public static void LoadScene(GameScene sceneToLoad, bool loadSceneAditively)
    {
        Instance.StartCoroutine(Instance.ActivateLoadingScreen(sceneToLoad, loadSceneAditively));
    }

    /// <summary>
    /// Coroutine for a clean loading screen transition
    /// </summary>
    /// <param name="sceneToLoad">Scene enum to load</param>
    /// <param name="loadSceneAditively">Controls if scene must be loaded aditive or not</param>
    /// <returns></returns>
    private IEnumerator ActivateLoadingScreen(GameScene sceneToLoad, bool loadSceneAditively)
    {
        Instance._loadingScreenPanel.OpenPanel();
        LoadSceneMode loadMode = loadSceneAditively ? LoadSceneMode.Additive : LoadSceneMode.Single;

        yield return new WaitForSeconds(1f);

        Instance._scenesLoading = new List<AsyncOperation>();
        Instance._scenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneToLoad, loadMode));

        if (Instance._actualSceneIndex > 0)
            Instance._scenesLoading.Add(SceneManager.UnloadSceneAsync(Instance._actualSceneIndex));

        Instance._actualSceneIndex = (int)sceneToLoad;
        Instance._loadingScreenPanel.ActivateLoadingUpdate(Instance._scenesLoading);
    }
}
