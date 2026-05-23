// Sample 01 — Basic Change Notifications
//
// Walks through every shape of NotifyCollectionChangedEventArgs<T> emitted
// by ObservableList<T>: single-item Add/Remove/Replace/Move, batch Add/Remove,
// and Reset (from Clear). The receiver is one handler that demonstrates how
// to dispatch on Action and on IsSingleItem.

using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

namespace Aspid.Collections.Samples.BasicChangeNotifications
{
    public sealed class BasicChangeNotificationsSample : MonoBehaviour
    {
        private ObservableList<string> _list;

        private void Start()
        {
            _list = new ObservableList<string>();
            _list.CollectionChanged += OnChanged;

            // Single-item Add -> Action=Add, IsSingleItem=true, NewItem set
            _list.Add("apple");
            _list.Add("banana");

            // Batch Add -> Action=Add, IsSingleItem=false, NewItems set
            _list.AddRange("cherry", "date", "elderberry");

            // Replace -> Action=Replace, IsSingleItem=true, OldItem + NewItem
            _list[0] = "apricot";

            // Move -> Action=Move, IsSingleItem=true, Old/NewStartingIndex
            _list.Move(0, 2);

            // Single Remove -> Action=Remove, IsSingleItem=true, OldItem
            _list.RemoveAt(1);

            // Batch Remove -> Action=Remove, IsSingleItem=false, OldItems
            _list.RemoveRange(0, 2);

            // Reset -> Action=Reset. No items carried in the args by design;
            // subscribers must re-read the collection.
            _list.Clear();
        }

        private void OnDestroy()
        {
            // Dispose() of an ObservableList<T> drops all subscribers AND clears the items
            // (Clear() raises one final Reset event). If you only want to detach this one
            // handler, use `_list.CollectionChanged -= OnChanged` instead.
            _list?.Dispose();
        }

        private static void OnChanged(INotifyCollectionChangedEventArgs<string> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (args.IsSingleItem) Debug.Log($"[+] Added \"{args.NewItem}\" at {args.NewStartingIndex}");
                    else Debug.Log($"[+] Added batch {Format(args.NewItems)} starting at {args.NewStartingIndex}");
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (args.IsSingleItem) Debug.Log($"[-] Removed \"{args.OldItem}\" at {args.OldStartingIndex}");
                    else Debug.Log($"[-] Removed batch {Format(args.OldItems)} starting at {args.OldStartingIndex}");
                    break;

                case NotifyCollectionChangedAction.Replace:
                    // ObservableList<T> only emits single-item Replace today, so a batch
                    // branch is not required here. Keep it explicit for safety.
                    Debug.Log($"[~] Replaced \"{args.OldItem}\" -> \"{args.NewItem}\" at {args.NewStartingIndex}");
                    break;

                case NotifyCollectionChangedAction.Move:
                    Debug.Log($"[>] Moved \"{args.NewItem}\" {args.OldStartingIndex} -> {args.NewStartingIndex}");
                    break;

                case NotifyCollectionChangedAction.Reset:
                    // Reset carries no payload — neither OldItems nor NewItems is populated.
                    // Treat it as "re-read the whole collection".
                    Debug.Log("[*] Reset");
                    break;
            }
        }

        private static string Format(IReadOnlyList<string> items)
        {
            if (items is null || items.Count == 0) return "[]";
            return "[" + string.Join(", ", items) + "]";
        }
    }
}
