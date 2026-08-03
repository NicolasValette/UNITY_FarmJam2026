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
            OnStartNight,
            OnHarvest,
            OnPlant,
            OnAddToBlender,
            OnBlend,
            OnSporeSelection,
            OnUIMenuOpen,
            OnUIMenuClose,
            OnMushroomDecay,
            OnScienceCollected,
            OnMouseOver,
            OnMouseStay,
            OnMouseExit
        }


        private Dictionary<Events, Delegate> _eventDictionary = new Dictionary<Events, Delegate>();

        private static EventManager _instance;

        public static EventManager Instance => _instance ??= new EventManager();
    
        #region WITHOUT PARAMETERS
        public static void StartListening(Events eventName, Action action)
        {
            if (Instance._eventDictionary.TryGetValue(eventName, out Delegate eventToListen))
            {
                Instance._eventDictionary[eventName] = Delegate.Combine(eventToListen, action);
            }
            else
            {
                Instance._eventDictionary.Add(eventName, action);
            }
        }
        public static void StopListening(Events eventName, Action action)
        {
            if (Instance._eventDictionary.TryGetValue(eventName, out Delegate eventToStopListen))
            {

                Delegate currentDel = Delegate.Remove(eventToStopListen, action);
                if (currentDel == null) Instance._eventDictionary.Remove(eventName);
                else Instance._eventDictionary[eventName] = currentDel;
            }
        }
        public static void TriggerEvent(Events eventName)
        {
            Delegate eventToTrigger;
            if (Instance._eventDictionary.TryGetValue(eventName, out eventToTrigger))
            {
                (eventToTrigger as Action)?.Invoke();
            }
        }
        #endregion

        #region WITH PARAMETERS
        public static void StartListening<T>(Events eventName, Action<T> action)
        {
            if (Instance._eventDictionary.TryGetValue(eventName, out Delegate eventToListen))
            {
                Instance._eventDictionary[eventName] = Delegate.Combine(eventToListen, action);
            }
            else
            {
                Instance._eventDictionary.Add(eventName, action);
            }
        }
        public static void StopListening<T>(Events eventName, Action<T> action)
        {
            if (Instance._eventDictionary.TryGetValue(eventName, out Delegate eventToStopListen))
            {

                Delegate currentDel = Delegate.Remove(eventToStopListen, action);
                if (currentDel == null) Instance._eventDictionary.Remove(eventName);
                else Instance._eventDictionary[eventName] = currentDel;
            }
        }
        public static void TriggerEvent<T>(Events eventName, T parameter)
        {
            Delegate eventToTrigger;
            if (Instance._eventDictionary.TryGetValue(eventName, out eventToTrigger))
            {
                (eventToTrigger as Action<T>)?.Invoke(parameter);
            }
        }
        #endregion


    }
}
