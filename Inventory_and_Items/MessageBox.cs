/**
 *
 * MessageBox
 * Created by Luke A Chase - chase.luke.a@gmail.com
 * 
 * -------------------------------------------------------------
 *  A user-created message box for displaying messages related
 *  to inventory and items
 * -------------------------------------------------------------
 *
 * Editor Fields:
 * Message Panel - the panel object containing the text object
 * Message Text  - the text object that will be updated
 *
 * Class Methods:
 * show(string, float) - show the specified message on screen
 *                       for the specified time (e.g. 3.0f)
 * -------------------------------------------------------------
 * Use this in conjuction with the Inventory.cs and Item.cs
 * classes; a Canvas is required with at least one panel that
 * contains a text child
 * -------------------------------------------------------------
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePanel;
    [SerializeField]
    private GameObject messageText;

    private float endTime;


    void Start()
    {
        messagePanel.SetActive(false);
    }


    public void show(string message, float displayTime)
    {
        messageText.GetComponent<Text> ().text = message;
        endTime = Time.fixedTime + displayTime;

        if (!messagePanel.activeSelf)
        {
            messagePanel.SetActive(true);
            StartCoroutine ("showMessage");
        }
    }


    public IEnumerator showMessage()
    {
//        for (float f = 0; f <= 1; f += 0.1f)
//        {
//            Vector3 t = messagePanel.transform.localScale;
//            t.y = f;
//            messagePanel.transform.localScale = t;
//            yield return new WaitForSeconds (0.01f);
//        }

        while (Time.fixedTime < endTime)
        {
            yield return new WaitForSeconds (0.5f);
        }

//        for (float f = 0; f <= 1; f += 0.1f)
//        {
//            Vector3 t = messagePanel.transform.localScale;
//            t.y = 1f - f;
//            messagePanel.transform.localScale = t;
//            yield return new WaitForSeconds (0.01f);
//        }
        messagePanel.SetActive (false);
    }

}

