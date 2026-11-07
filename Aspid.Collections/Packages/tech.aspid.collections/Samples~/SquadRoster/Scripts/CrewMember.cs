namespace Aspid.Collections.Samples.SquadRoster
{
    internal sealed class CrewMember
    {
        internal string Name { get; }
        internal string Role { get; }
        internal int Id { get; }

        internal CrewMember(int id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
