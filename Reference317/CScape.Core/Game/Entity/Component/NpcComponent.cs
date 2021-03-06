using System;
using CScape.Core.Game.Entity.Message;
using CScape.Models.Game.Entity;
using CScape.Models.Game.Entity.Component;
using JetBrains.Annotations;

namespace CScape.Core.Game.Entity.Component
{
    public class NpcComponent : EntityComponent, INpcComponent
    {
        private readonly Action<NpcComponent> _destroyCallback;
        public override int Priority => (int)ComponentPriority.Npc;

        public short DefinitionId { get; private set; }
        public short InstanceId { get; }

        public NpcComponent(
            IEntity parent,
            short defId,
            short npcId,
            [NotNull] Action<NpcComponent> destroyCallback)
            :base(parent)
        {
            _destroyCallback = destroyCallback;
            DefinitionId = defId;
            InstanceId = npcId;
        }

        public void ChangeDefinitionId(short newId)
        {
            DefinitionId = newId;
            Parent.SendMessage(new DefinitionChangeMessage(newId));
        }

        public override void ReceiveMessage(IGameMessage msg)
        {
            switch (msg.EventId)
            {
                case (int)MessageId.QueuedForDeath:
                {
                    _destroyCallback(this);
                    break;
                }
            }
        }

        public override string ToString() 
            => $"Npc {InstanceId} def-id {DefinitionId}";
    }
}