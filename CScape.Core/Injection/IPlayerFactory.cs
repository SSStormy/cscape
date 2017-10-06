﻿using System.Collections.Generic;
using CScape.Core.Game.Entities;
using CScape.Core.Game.Entities.Interface;
using JetBrains.Annotations;

namespace CScape.Core.Injection
{

    public interface IPlayerFactory
    {
        IEntitySystem EntitySystem { get; }
        IReadOnlyList<EntityHandle> All { get; }

        [CanBeNull]
        EntityHandle GetPlayer(int id);

        /// <summary>
        /// Creates a player entity.
        /// </summary>
        /// <param name="model">The player model which the new player entity will represent db sync with.</param>
        /// <param name="ctx">The connection context with which the player will net sync with</param>
        /// <returns>An <see cref="EntityHandle"/> pointing to the new player entity or null if the player list is full.</returns>
        /// <exception cref="EntityComponentNotSatisfied">One of the components of the entity is not satisfied</exception>
        [CanBeNull]
        EntityHandle Create([NotNull] IPlayerModel model, [NotNull] ISocketContext ctx);
    }
}