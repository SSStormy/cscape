﻿using System;
using System.Diagnostics;
using CScape.Core.Game.Entity.Component;
using CScape.Models.Extensions;
using CScape.Models.Game.Entity;
using CScape.Models.Game.Entity.Component;
using CScape.Models.Game.Item;
using JetBrains.Annotations;

namespace CScape.Core.Game.Entity.Factory
{
    public sealed class GroundItemFactory : IGroundItemFactory
    {
        public IEntitySystem System { get; }

        public GroundItemFactory([NotNull] IServiceProvider services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            System = services.ThrowOrGet<IEntitySystem>();
        }

        public IEntityHandle CreatePlayerDrop(ItemStack stack, IPlayerComponent player, string name)
        {
            var handle = System.Create($"Player dropped item: {name} ({stack.Id.Name}: {stack.Amount}");
            var ent = handle.Get();

            var item = new PlayerDroppedItemComponent(ent, stack, null, player.Username);
            ent.Components.Add(new VisionComponent(ent));
            ent.Components.Add(new MarkedForDeathBroadcasterComponent(ent));
            ent.Components.Add<IGroundItemComponent>(item);
            ent.Components.Add<IVisionResolver>(item);

            return handle;
        }

        public IEntityHandle Create(ItemStack stack, string name)
        {
            var handle = System.Create($"Ground item: {name} ({stack.Id.Name}: {stack.Amount}");
            var ent = handle.Get();

            ent.Components.Add(new VisionComponent(ent));
            ent.Components.Add(new MarkedForDeathBroadcasterComponent(ent));
            ent.Components.Add<IGroundItemComponent>(new GroundItemComponent(ent, stack, null));
            
            var status = ent.AreComponentRequirementsSatisfied(out var msg);
            Debug.Assert(status, msg);

            return handle;
        }
    }
}