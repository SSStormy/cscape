using System;
using CScape.Core.Game.World;
using CScape.Core.Injection;
using JetBrains.Annotations;

namespace CScape.Core.Game.Entity
{
    public class GroundItem : WorldEntity
    {
        public bool NeedsAmountUpdate { get; private set; }
        public bool JustWentPublic { get; private set; }


        public int ItemId { get; }
        public int ItemAmount { get; private set; }
        public Player DroppedBy { get; }

        /// <summary>
        /// How many ms need to pass after the creation of the item in order for it to become public.
        /// </summary>
        public long BecomesPublicAfterMs { get; } = 60 * 2 * 1000; // 2 minutes

        /// <summary>
        /// How many milliseconds need to pass for the item to despawn.
        /// </summary>
        public long DespawnsAfterMs { get; } = 60 * 6 * 1000; // 6 minutes

        /// <summary>
        /// Whether this item can be seen by everybody, not just by the player who dropped it.
        /// </summary>
        public bool IsPublic { get; private set; }

        public GroundItem(
            [NotNull] IServiceProvider services, 
            (int id, int amount) item,
            IPosition pos, Player droppedBy, PlaneOfExistance poe = null) 
            : base(services)
        {
            ItemId = item.id;
            ItemAmount = item.amount;

            DroppedBy = droppedBy;

            var t = new ServerTransform(this, pos, poe);
            Transform = t;

        }

        private long _droppedForMs;

        public override void Update(IMainLoop loop)
        {
            // reset update flags
            JustWentPublic = false;
            NeedsAmountUpdate = false;

            // accumulate alive time
            _droppedForMs += loop.DeltaTime + loop.ElapsedMilliseconds;

            // handle the item going public
            if (!IsPublic)
            {
                if (_droppedForMs >= BecomesPublicAfterMs)
                {
                    IsPublic = true;
                    JustWentPublic = true;
                }
                    
            }

            // handle despawning
            if (DespawnsAfterMs >= _droppedForMs)
                // keep the item in the update loop for 1 more tick 
                // after being destroyed so that ground item sync machines can
                // see that this item needs to be removed.
                Destroy(); 

        }

        public void UpdateAmount(int newAmount)
        {
            if (ItemAmount == newAmount) return;
            if (0 >= newAmount) return;

            ItemAmount = newAmount;
            NeedsAmountUpdate = true;
        }
    }
}