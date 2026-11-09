using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.CraftingQueue.Presentation
{
    // Presentation plumbing is local so this sample can be imported on its own.
    internal sealed class SamplePanel : IDisposable
    {
        private readonly List<Action> _unbind = new List<Action>();
        private readonly VisualElement _root;
        private int _sequence;

        internal SamplePanel(VisualElement root)
        {
            _root = root;
            Get<VisualElement>("log").Clear();
            var events = Get<Foldout>("events");
            events.SetValueWithoutNotify(false);
            events.text = "+ Events";
            EventCallback<ChangeEvent<bool>> changed = change =>
                events.text = change.newValue ? "- Events" : "+ Events";
            events.RegisterValueChangedCallback(changed);
            _unbind.Add(() => events.UnregisterValueChangedCallback(changed));

            var dark = true;
#if UNITY_EDITOR
            dark = UnityEditor.EditorGUIUtility.isProSkin;
#endif
            ApplyTheme(dark);
            Bind("theme", () => ApplyTheme(!Get<VisualElement>("screen").ClassListContains("theme-dark")));
        }

        private void ApplyTheme(bool dark)
        {
            Get<VisualElement>("screen").EnableInClassList("theme-dark", dark);
            var button = Get<Button>("theme");
            button.text = dark ? "Light" : "Dark";
            button.tooltip = dark ? "Switch to light theme" : "Switch to dark theme";
        }

        internal T Get<T>(string name) where T : VisualElement => _root.Q<T>(name);
        internal void Text(string name, object value) => Get<Label>(name).text = value.ToString();

        internal void Bind(string name, Action action)
        {
            var button = Get<Button>(name);
            button.clicked += action;
            _unbind.Add(() => button.clicked -= action);
        }

        internal void Search(string name, Action<string> action)
        {
            var field = Get<TextField>(name);
            EventCallback<ChangeEvent<string>> callback = e => action(e.newValue);
            field.RegisterValueChangedCallback(callback);
            _unbind.Add(() => field.UnregisterValueChangedCallback(callback));
        }

        internal void Enabled(string name, bool enabled) => Get<Button>(name).SetEnabled(enabled);

        internal void Log(string message)
        {
            var log = Get<VisualElement>("log");
            log.Insert(0, Label($"{++_sequence:00}  {message}", "log-entry"));
            while (log.childCount > 3) log.RemoveAt(log.childCount - 1);
        }

        internal static Label Label(string text, string className)
        {
            var label = new Label(text);
            label.AddToClassList(className);
            return label;
        }

        internal static VisualElement Card(string symbol, string title, string detail, string tail)
        {
            var card = new VisualElement();
            card.AddToClassList("card");
            var top = new VisualElement();
            top.AddToClassList("card-top");
            top.Add(Label(symbol, "avatar"));
            top.Add(Label(title, "card-title"));
            card.Add(top);
            card.Add(Label(detail, "card-detail"));
            card.Add(Label(tail, "card-tail"));
            return card;
        }

        internal static void Empty(VisualElement parent, string message)
        {
            if (parent.childCount == 0) parent.Add(Label(message, "empty"));
        }

        public void Dispose()
        {
            foreach (var unbind in _unbind) unbind();
            _unbind.Clear();
        }
    }
}
