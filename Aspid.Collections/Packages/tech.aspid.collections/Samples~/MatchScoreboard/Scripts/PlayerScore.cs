namespace Aspid.Collections.Samples.MatchScoreboard
{
    internal sealed class PlayerScore
    {
        internal string Id { get; }
        internal string Name { get; }
        internal int Score { get; }

        internal PlayerScore(string id, string name, int score)
        {
            Id = id;
            Name = name;
            Score = score;
        }

        internal PlayerScore Add(int points) => new PlayerScore(Id, Name, Score + points);
    }
}
