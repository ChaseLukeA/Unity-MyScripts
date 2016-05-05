/**
 *
 * Inventory Script
 * Created by Luke A Chase - chase.luke.a@gmail.com
 * 
 * -------------------------------------------------------------
 * Add Inventory to your player character to give the ability
 * to store game objects in a user-named inventory of a user-
 * defined maximum capacity
 * -------------------------------------------------------------
 *
 * Editor Fields:
 * Maximum Capacity        - the maximum number of items that
 *                           can be held in the inventory
 * Inventory Friendly Name - user-defined name to be used in
 *                           messageBox messages; use lowercase
 *
 * Class Methods:
 * addItem(GameObject)     - adds the passed-in GameObject to
 *                           the Inventory as long as there is
 *                           enough capacity for an item
 * getItemNamed(string)    - returns the first GameObject
 *                           stored in Inventory that matches
 *                           the specified name (Item.name)
 * useItemNamed(string)    - removes the item from inventory
 * -------------------------------------------------------------
 * Use this in conjuction with the Item.cs and MessageBox.cs
 * classes; GameObjects with the Item script applied and a
 * name given them can then be accessed here via other scripts
 * -------------------------------------------------------------
*/

using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int maximum_capacity;
    private int maximumCapacity;

    [SerializeField]
    private string inventory_friendly_name;
    private string inventoryName;
    private string DEFAULT_NAME = "inventory";

    private GameObject[] inventory;  //  the actual array of game objects in inventory
    private string[] items;  //  for inventory item names, quick inventory searches, & counts

    private MessageBox messageBox;


    void Start()
    {
        maximumCapacity = maximum_capacity;
        inventoryName = inventory_friendly_name == "" ? DEFAULT_NAME : inventory_friendly_name;

        inventory = new GameObject[maximumCapacity];
        items = new string[maximumCapacity];

        messageBox = GameObject.Find("HUD").GetComponentInChildren<MessageBox>();
    }


    public void addItem(GameObject item)
    {
        if (numberOfItems() < maximumCapacity)
        {
            for (int i = 0; i < maximumCapacity; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    items[i] = item.name;

                    int total = numberOfItems();

                    messageBox.show
                    (
                        "The " + inventory[i].name + " was added. You now have " + total +
                        " item" + (total > 1 ? "s" : "") + " in your " + inventoryName + ".",
                        4.0f
                    );

                    return;
                }
            }
        }
        else
        {
            messageBox.show
            (
                "Your " + inventoryName + " can only hold " + maximumCapacity + " item" +
                (maximumCapacity > 1 ? "s" : "") + ". You have no more room in your " + inventoryName + "!",
                4.0f
            );
        }
    }


    public GameObject getItemNamed(string itemName)
    {
        for (int i = 0; i < maximumCapacity; i++)
        {
            if (items[i].Contains(itemName))
            {
                GameObject selected = inventory[i];
                selected.SetActive(true);
                return selected;
            }
        }
        return null;
    }


    public void useItemNamed(string itemName)
    {
        for (int i = 0; i < maximumCapacity; i++)
        {
            if (items[i].Contains(itemName))
            {
                messageBox.show("The " + inventory[i].name + " has been used.", 4.0f);
                inventory[i] = null;
                items[i] = null;
            }
        }
        
    }


    private int numberOfItems()
    {
        int counter = 0;

        for (int i = 0; i < maximumCapacity; i++)
        {
            if (items[i] != null)
            {
                counter++;
            }
        }
        return counter;
    }

}
