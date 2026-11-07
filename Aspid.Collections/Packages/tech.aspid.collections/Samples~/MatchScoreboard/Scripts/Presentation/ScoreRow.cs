using System;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.MatchScoreboard.Presentation
{
    internal sealed class ScoreRow : IDisposable
    {
        private readonly Label _rank;
        private readonly Action _released;
        private bool _disposed;
        internal PlayerScore Player { get; }
        internal VisualElement Element { get; }

        internal ScoreRow(PlayerScore player, int viewId, Action released)
        {
            Player = player;
            _released = released;
            Element = new VisualElement();
            Element.AddToClassList("table-row");
            _rank = SamplePanel.Label("", "rank");
            var body = new VisualElement();
            body.AddToClassList("row-body");
            body.Add(SamplePanel.Label(player.Name, "card-title"));
            body.Add(SamplePanel.Label($"{player.Id} / View {viewId:00}", "card-detail"));
            Element.Add(_rank);
            Element.Add(body);
            Element.Add(SamplePanel.Label(player.Score.ToString(), "score"));
        }

        internal void Rank(int rank) => _rank.text = $"{rank:00}";

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Element.RemoveFromHierarchy();
            _released();
        }
    }
}
