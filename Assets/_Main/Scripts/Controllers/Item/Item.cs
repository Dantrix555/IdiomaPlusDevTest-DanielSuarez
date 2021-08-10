using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base collectable item class
/// </summary>
public class Item : MonoBehaviour
{
    private ItemType _type;

    /// <summary>
    /// Setup which item type will be the attached item
    /// </summary>
    /// <param name="newItemType">item type to be set</param>
    public void SetupItem(ItemType newItemType)
    {
        _type = newItemType;
    }

    /// <summary>
    /// When detect a colision with the player, destroys the object and update item amount
    /// </summary>
    /// <param name="other">detected collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            RPG_DevTest.UpdateItemAmount(_type, true);
            Destroy(gameObject);
        }
    }
}
