/*
Modified from https://learn.unity.com/project/roll-a-ball-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary <System.Type, UnityEventBase> eventDictionary;
    private static EventManager eventManager;
    public static EventManager instance {
        get { // get instance
            if (!eventManager) { // if no reference to the instance, find it
                eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

                if (!eventManager) { // log error if still no instance
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                } else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<System.Type, UnityEventBase>();
        }
    }

    // public static so that we can access these from any other script
    public static void StartListening<Tbase, T0>(UnityAction<T0> listener) where Tbase : UnityEvent<T0>, new() {
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(typeof(Tbase), out thisEvent)) {
            Tbase e = thisEvent as Tbase;
            if (e != null) {
                e.AddListener(listener);
            } else {
                Debug.LogError("EventManager.StartListening() failed. Event type " + typeof(Tbase).ToString() + " could not be accessed.");
            }
        } else {
            Tbase e = new Tbase ();
			e.AddListener (listener);
			instance.eventDictionary.Add (typeof(Tbase), e);
        }
    }
    // public static void StartListening<Tbase> (UnityAction listener) where Tbase : UnityEvent, new()
	// 	{

	// 			UnityEventBase thisEvent = null;

	// 			if (instance.eventDictionary.TryGetValue (typeof(Tbase), out thisEvent)) {
	// 					Tbase e = thisEvent as Tbase;

	// 					if (e != null)
	// 							e.AddListener (listener);
	// 					else
	// 							Debug.LogError ("EventManager.StartListening() FAILED! Event type " + typeof(Tbase).ToString() + " could not be accessed for some strange reason.");
	// 			} else {
	// 					Tbase e = new Tbase ();
	// 					e.AddListener (listener);
	// 					instance.eventDictionary.Add (typeof(Tbase), e);
	// 			}
	// 	}

    public static void StopListening<Tbase, T0> (UnityAction<T0> listener) where Tbase : UnityEvent<T0>
		{
				if (eventManager == null)
						return;
				UnityEventBase thisEvent = null;
				if (instance.eventDictionary.TryGetValue (typeof(Tbase), out thisEvent)) {
						Tbase e = thisEvent as Tbase;

						if (e != null)
								e.RemoveListener (listener);
						else
								Debug.LogError ("EventManager.StopListening() FAILED! Event type " + typeof(Tbase).ToString() + " could not be accessed for some strange reason.");
				}
		}

    public static void TriggerEvent<Tbase, T0> (T0 t0_obj) where Tbase : UnityEvent<T0>
		{
				UnityEventBase thisEvent = null;
				if (instance.eventDictionary.TryGetValue (typeof(Tbase), out thisEvent)) {

						Tbase e = thisEvent as Tbase;

						if (e != null)
								e.Invoke (t0_obj);
						else
								Debug.LogError ("EventManager.TriggerEvent() failed. Event type " + typeof(Tbase).ToString() + " could not be accessed.");
				}
		}

    // public static void TriggerEvent<Tbase> () where Tbase : UnityEvent
	// 	{
	// 			UnityEventBase thisEvent = null;
	// 			if (instance.eventDictionary.TryGetValue (typeof(Tbase), out thisEvent)) {

	// 					Tbase e = thisEvent as Tbase;

	// 					if (e != null)
	// 							e.Invoke ();
	// 					else
	// 							Debug.LogError ("EventManager.TriggerEvent() FAILED! Event type " + typeof(Tbase).ToString() + " could not be accessed for some strange reason.");
	// 			}
	// 	}
}
