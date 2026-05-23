namespace Aspid.Collections.Samples.DictionaryKeyedTable
{
    public readonly struct PlayerScore
    {
        public int Score { get; }
        
        public string DisplayName { get; }

        public PlayerScore(string displayName, int score)
        {
            DisplayName = displayName;
            Score = score;
        }
    }
}
