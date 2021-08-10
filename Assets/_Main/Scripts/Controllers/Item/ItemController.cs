using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Setup the main gameplay items
/// </summary>
public class ItemController : MonoBehaviour
{
    [SerializeField] private Item[] _healItems = default;
    [SerializeField] private Item[] _boostItems = default;

    //Setup the attached item, setting a new type
    public void SetupItems()
    {
        foreach (Item healItem in _healItems)
            healItem.SetupItem(ItemType.HEAL);

        foreach (Item boostItem in _boostItems)
            boostItem.SetupItem(ItemType.BOOST);
    }
}
