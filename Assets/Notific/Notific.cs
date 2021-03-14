// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// public enum NotificType
// {
//     // When new yarn line/phrase starts
//     YarnLineStart,
//     // When dialogue stops at the end
//     YarnDialogueEnd,
//     // When writer requests spawning new character
//     CharacterSpawnRequested,
//     // When writer requests changing emotion 
//     CharacterSetEmotionRequested,
//     // When writer asks to remove character
//     CharacterRemoveRequested,
//     // When character should change position
//     CharacterMoveRequested,
//     // When we need to change background
//     BackgroundChangeRequested,
//     
//     DialogueContainerShowRequested,
//     DialogueContainerHideRequested,
//     
//     DialogueNextPhraseRequested
// }
//
// // Notific - simplest library ever for sending "notifications" to other objects with some arguments
// public static class Notific
// {
//     // Notification Type and its Methods
//     private static readonly Dictionary<NotificType, List<Action<string[]>>> Subscribers =
//         new Dictionary<NotificType, List<Action<string[]>>>();
//     
//     public static void Subscribe(NotificType type, Action<string[]> action)
//     {
//         if (Subscribers.ContainsKey(type))
//         {
//             // Add new method to already subscribed type
//             Subscribers[type].Add(action);
//             return;
//         }
//
//         // ... or add new action list
//         Subscribers.Add(type, new List<Action<string[]>>() {action});
//     }
//
//     public static void Unsubscribe(NotificType type, Action<string[]> action)
//     {
//         if (!Subscribers.TryGetValue(type, out var actions))
//         {
//             Debug.LogError($"Can't find Notifcation for {type.ToString()}");
//             return;
//         }
//         
//         actions.Remove(action);
//         Debug.Log($"Unsubscribed some action from {type.ToString()}");
//     }
//
//     public static void Send(NotificType type, string[] args)
//     {
//         if (!Subscribers.TryGetValue(type, out var actions))
//         {
//             return;
//         }
//
//         foreach (var action in actions)
//         {
//             action(args);
//         }
//     }
//
//     public static void Send(NotificType type)
//     {
//         Send(type, new string[] {});
//     }
//
//     public static void Reset()
//     {
//         Subscribers.Clear();
//     }
// }
