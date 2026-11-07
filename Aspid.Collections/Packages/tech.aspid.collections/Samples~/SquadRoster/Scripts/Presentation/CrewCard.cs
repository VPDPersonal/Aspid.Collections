using System;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.SquadRoster.Presentation
{
    internal sealed class CrewCard : IDisposable
    {
        private readonly Action _released;
        private bool _disposed;
        internal VisualElement Element { get; }

        internal CrewCard(CrewMember member, int viewId, Action released)
        {
            _released = released;
            Element = SamplePanel.Card(member.Name.Substring(0, 1), member.Name,
                member.Role, $"View {viewId:00}");
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Element.RemoveFromHierarchy();
            _released();
        }
    }
}
