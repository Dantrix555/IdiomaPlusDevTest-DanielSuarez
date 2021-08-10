using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Init game controller
/// </summary>
public class Init : MonoBehaviour
{
    private void Awake()
    {
        RPG_DevTest.StartGame();
    }
}
