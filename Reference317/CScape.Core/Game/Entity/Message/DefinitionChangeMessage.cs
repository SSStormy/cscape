using CScape.Models.Game.Entity;

namespace CScape.Core.Game.Entity.Message
{
    public sealed class DefinitionChangeMessage : IGameMessage
    {
        public int EventId => (int)MessageId.DefinitionChange;
        public short Definition { get; }

        public DefinitionChangeMessage(short def)
        {
            Definition = def;
        }
    }
}