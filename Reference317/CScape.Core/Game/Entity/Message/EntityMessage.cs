﻿using System;
using CScape.Models.Game.Entity;
using JetBrains.Annotations;

namespace CScape.Core.Game.Entity.Message
{
    public sealed class EntityMessage :IGameMessage
    {
        public int EventId { get; }

        [NotNull]
        public IEntityHandle Entity { get; }

        public static EntityMessage PlayerFollowTarget([NotNull] IEntityHandle entity)
            => new EntityMessage(entity, MessageId.NewPlayerFollowTarget);

        public static EntityMessage EnteredViewRange([NotNull] IEntityHandle entity)
            => new EntityMessage(entity, MessageId.EntityEnteredViewRange);

        public static  EntityMessage LeftViewRange([NotNull] IEntityHandle entity)
            => new EntityMessage(entity, MessageId.EntityLeftViewRange);

        public static EntityMessage NearbyEntityQueuedForDeath([NotNull] IEntityHandle entity)
            => new EntityMessage(entity, MessageId.NearbyEntityQueuedForDeath);

        private EntityMessage([NotNull] IEntityHandle entity, MessageId eventId)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            EventId = (int)eventId;
        }
    }
}
