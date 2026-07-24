using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Event manager class that allows to register and trigger events in the game. It uses a singleton pattern to ensure that there is only one instance of the event manager in the game. The events are defined in an enum called Events, and can be triggered by calling the TriggerEvent method with the corresponding event name.
    /// Listeners can register to events by calling the StartListening method with the event name and a callback action, and can unregister by calling the StopListening method with the same parameters.
    /// 
    /// To add your own events, simply add them to the Events enum and use the TriggerEvent method to trigger them. Listeners can register to these events by calling the StartListening method with the event name and a callback action, and can unregister by calling the StopListening method with the same parameters.
    /// </summary>
    public class EventManager
    {
        public enum Events
        {
            None,
            OnStartDay,
            OnStartNight
        }


        private Dictionary<Events, Action> eventDictionnary;

        private static EventManager _instance;

        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventManager();
                    _instance.Init();
                }
                return _instance;
            }
        }
        public void Init()
        {
            eventDictionnary = new Dictionary<Events, Action>();
        }
        public static void StartListening(Events eventName, Action action)
        {
            if (Instance.eventDictionnary.TryGetValue(eventName, out Action eventToListen))
            {
                eventToListen += action;
                Instance.eventDictionnary[eventName] = eventToListen;
            }
            else
            {
                eventToListen += action;
                Instance.eventDictionnary.Add(eventName, action);
            }
        }
        public static void StopListening(Events eventName, Action action)
        {
            if (Instance.eventDictionnary.TryGetValue(eventName, out Action eventToStopListen))
            {
                eventToStopListen -= action;
                Instance.eventDictionnary[eventName] = eventToStopListen;
                if (Instance.eventDictionnary[eventName] == null)
                {
                    Instance.eventDictionnary.Remove(eventName);

                }
            }
        }

        public static void TriggerEvent(Events eventName)
        {
            Action eventToTrigger;
            if (Instance.eventDictionnary.TryGetValue(eventName, out eventToTrigger))
            {
                eventToTrigger?.Invoke();
            }
        }



    }
}
