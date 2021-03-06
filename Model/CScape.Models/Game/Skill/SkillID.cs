using System;

namespace CScape.Models.Game.Skill
{
    public struct SkillID : IEquatable<SkillID>
    {
        public string Name { get; }

        public byte ClientIndex { get; }

        /// <summary>
        /// The chat interface which will be displayed when this a level up event is caught for this model.
        /// </summary>
        public int LevelupInterfaceId { get; }

        public SkillID(string name, byte clientIndex, int levelupInterfaceId)
        {
            Name = name;
            ClientIndex = clientIndex;
            LevelupInterfaceId = levelupInterfaceId;
        }

        public bool Equals(SkillID other)
        {
            return string.Equals(Name, other.Name)
                   && ClientIndex == other.ClientIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SkillID && Equals((SkillID) obj);
        }

        public override string ToString() => Name;

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ClientIndex;
                return hashCode;
            }
        }
    }
}