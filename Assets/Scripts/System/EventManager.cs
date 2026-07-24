using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
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
