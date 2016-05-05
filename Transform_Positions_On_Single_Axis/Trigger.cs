/**
 *
 * Trigger Script
 * Created by Luke A Chase - chase.luke.a@gmail.com
 * 
 * -------------------------------------------------------------
 * Applying this script to a game object allows choosing of the
 * layer that can trigger the events, and allows choosing
 * multiple objects and all properties assigned to them
 * -------------------------------------------------------------
 * 
 * Editor Fields:
 * Triggering Layer - the game layer (defined in GameUtility.cs
 *                    as a Layer enum) to trigger on collision
 *                    with this gameObject
 * Triggered Events - the object(s) selected, their component(s),
 *                    and the actions/events to trigger
 * 
 * -------------------------------------------------------------
 * Use this in conjunction with the GameUtility.cs class for the
 * new data types
 * -------------------------------------------------------------
*/

using System;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	public class Trigger : MonoBehaviour
	{
		[SerializeField]
		private Layer triggeringLayer;

		[Serializable]
		public class TriggerEvent : UnityEvent { }

		[SerializeField]
		private TriggerEvent triggeredEvents = new TriggerEvent();


		void Awake ()
		{
			gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		}


		void OnTriggerEnter (Collider collider)
		{
			if (collider.gameObject.layer == (int)triggeringLayer)
			{
				triggeredEvents.Invoke ();
			}
		}
	}

}
