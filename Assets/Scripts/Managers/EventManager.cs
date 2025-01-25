using UnityEngine;
using System;
using System.Collections;

namespace GGJ2025
{
    /// <summary>
    /// Handles the management and invocation of custom events between objects.
    /// Provides methods for listening to events, stopping listening, and invoking events.
    /// </summary>
    public class EventManager
    {

        #region Fields

        // Singleton instance of the EventManager
        private static EventManager instance;

        // Stores event mappings using a hashtable for quick lookup
        private Hashtable eventHash = new();

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the EventManager instance
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            instance = new();
        }

        /// <summary>
        /// Generates a unique key for storing events with generic arguments
        /// </summary>
        /// <typeparam name="T">The type of the event argument</typeparam>
        /// <param name="eventType">The type of the custom event</param>
        /// <returns>A unique string key for identifying the event in the hash table</returns>
        private static string GetKey<T>(CustomEventType eventType)
        {
            Type type = typeof(T);
            string key = type.ToString() + eventType.ToString();
            return key;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Registers a listener for a generic event type
        /// </summary>
        /// <typeparam name="T">The type of event argument</typeparam>
        /// <param name="eventType">The type of the custom event to listen for</param>
        /// <param name="listener">The callback method to execute when the event is invoked</param>
        public static void StartListening<T>(CustomEventType eventType, Action<T> listener)
        {
            string key = GetKey<T>(eventType);

            if (instance.eventHash.ContainsKey(key))
            {
                // Retrieve and add the listener to an existing event
                Action<T> thisEvent = (Action<T>)instance.eventHash[key];
                thisEvent += listener;
                instance.eventHash[key] = thisEvent;
            }
            else
            {
                // Create a new event and add the listener
                Action<T> thisEvent = listener;
                instance.eventHash.Add(key, thisEvent);
            }
        }

        /// <summary>
        /// Registers a listener for an event without arguments
        /// </summary>
        /// <param name="eventType">The type of the custom event to listen for</param>
        /// <param name="listener">The callback method to execute when the event is invoked</param>
        public static void StartListening(CustomEventType eventType, Action listener)
        {
            if (instance.eventHash.ContainsKey(eventType))
            {
                // Retrieve and add the listener to an existing event
                Action thisEvent = (Action)instance.eventHash[eventType];
                thisEvent += listener;
                instance.eventHash[eventType] = thisEvent;
            }
            else
            {
                // Create a new event and add the listener
                Action thisEvent = listener;
                instance.eventHash.Add(eventType, thisEvent);
            }
        }

        /// <summary>
        /// Stops listening to a specific generic event type
        /// </summary>
        /// <typeparam name="T">The type of the event argument</typeparam>
        /// <param name="eventType">The type of the custom event</param>
        /// <param name="listener">The callback method to remove</param>
        public static void StopListening<T>(CustomEventType eventType, Action<T> listener)
        {
            string key = GetKey<T>(eventType);

            if (instance.eventHash.ContainsKey(key))
            {
                // Remove the listener from the existing event
                Action<T> thisEvent = (Action<T>)instance.eventHash[key];
                thisEvent -= listener;
                if (thisEvent == null)
                {
                    instance.eventHash.Remove(key);
                }
                else
                {
                    instance.eventHash[key] = thisEvent;
                }
            }
        }

        /// <summary>
        /// Stops listening to a specific event without arguments
        /// </summary>
        /// <param name="eventType">The type of the custom event</param>
        /// <param name="listener">The callback method to remove</param>
        public static void StopListening(CustomEventType eventType, Action listener)
        {
            if (instance.eventHash.ContainsKey(eventType))
            {
                // Remove the listener from the existing event
                Action thisEvent = (Action)instance.eventHash[eventType];
                thisEvent -= listener;
                if (thisEvent == null)
                {
                    instance.eventHash.Remove(eventType);
                }
                else
                {
                    instance.eventHash[eventType] = thisEvent;
                }
            }
        }

        /// <summary>
        /// Invokes a generic event with an argument
        /// </summary>
        /// <typeparam name="T">The type of the event argument</typeparam>
        /// <param name="eventType">The type of the custom event</param>
        /// <param name="value">The value to pass to the event listener</param>
        public static void Invoke<T>(CustomEventType eventType, T value)
        {
            string key = GetKey<T>(eventType);

            if (instance.eventHash.ContainsKey(key))
            {
                // Trigger the event with the provided argument
                Action<T> thisEvent = (Action<T>)instance.eventHash[key];
                thisEvent?.Invoke(value);
            }
        }

        /// <summary>
        /// Invokes an event without arguments
        /// </summary>
        /// <param name="eventType">The type of the custom event</param>
        public static void Invoke(CustomEventType eventType)
        {
            if (instance.eventHash.ContainsKey(eventType))
            {
                // Trigger the event without any arguments
                Action thisEvent = (Action)instance.eventHash[eventType];
                thisEvent?.Invoke();
            }
        }

        #endregion

    }
}
