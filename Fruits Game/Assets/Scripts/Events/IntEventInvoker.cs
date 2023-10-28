using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEventInvoker : MonoBehaviour
{
    #region Fields
    
    protected Dictionary<EventNames, UnityEvent<Vector2, int>> unityEvents = new Dictionary<EventNames, UnityEvent<Vector2, int>>();

    //protected bool strawberryPerm = true;

    #endregion

    #region Methods

    public void AddListener(EventNames eventName, UnityAction<Vector2, int> listener)
    {
        if(unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(listener);
        }
    }

    #endregion
}
