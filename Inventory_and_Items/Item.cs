/**
 *
 * Item Script
 * Created by Luke A Chase - chase.luke.a@gmail.com
 * 
 * -------------------------------------------------------------
 * Add Item to any GameObject to give it the ability to be
 * added to an Inventory; the GameObject this script is applied
 * to will need a Physics Collider component added that has
 * 'Is Trigger' checked
 * -------------------------------------------------------------
 *
 * Editor Fields:
 * Item Name - the name you'd like this item to be called; the
 *             name is what you'll be sending to Inventory
 *             from other scripts when you call either it's
 *             getItemNamed() or useItemNamed() methods
 * -------------------------------------------------------------
 * Use this in conjuction with the Inventory.cs class;
 * -------------------------------------------------------------
*/

using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string item_name;


    void Start ()
    {
        gameObject.name = item_name;
    }
    

    void OnTriggerEnter(Collider colliderObject)
    {
        print("object is named: " + gameObject.name);
        if (colliderObject.GetComponent<Inventory>() != null)
        {
            colliderObject.GetComponent<Inventory>().addItem(gameObject);
            gameObject.SetActive(false);
        }
    }

}
