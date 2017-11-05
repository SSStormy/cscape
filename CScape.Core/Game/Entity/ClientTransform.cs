using System;
using CScape.Core.Game.World;
using JetBrains.Annotations;

namespace CScape.Core.Game.Entity
{
    public class ClientTransform : ServerTransform , IClientTransform
    {
        public static ClientTransform Create(
            IWorldEntity forEntity,
            IPosition pos,
            [CanBeNull] PlaneOfExistence poe = null)
        {
            var transform = new ClientTransform(forEntity);

            transform.Initialize(pos.X, pos.Y, pos.Z, poe ?? forEntity.Server.Overworld);

            return transform;
        }

        private (int x, int y) _local;
        private (int x, int y) _clientRegion;
        public const int MinRegionBorder = 16;
        public const int MaxRegionBorder = 88;

        public (int x, int y) Base { get; private set; }

        public (int x, int y) ClientRegion => _clientRegion;
        public (int x, int y) Local => _local;

        public ClientTransform(
            [NotNull] IWorldEntity entity) : base(entity)
        {
        }

        protected void Initialize(int x, int y, byte z, [NotNull] PlaneOfExistence poe)
        {
            if (poe == null) throw new ArgumentNullException(nameof(poe));

            X = x;
            Y = y;
            Z = z;

            SwitchPoE(poe);
            Teleport(x, y, z);
        }

        protected override void InternalSetPosition(int x, int y, byte z)
        {
            _clientRegion = ((X >> 3) - 6, (Y >> 3) - 6);
            _local = (X - (8 * ClientRegion.x), Y - (8 * ClientRegion.y));

            Recalc();
        }

        protected override void InternalMove(sbyte dx, sbyte dy)
        {
            _local.x += dx;
            _local.y += dy;

            Recalc();
        }

        private void Recalc()
        {
            // update locals and client region
            if (Local.x < MinRegionBorder)
            {
                _local.x += 32;
                _clientRegion.x -= 4;
            }
            else if (Local.x >= MaxRegionBorder)
            {
                _local.x -= 32;
                _clientRegion.x += 4;
            }

            if (Local.y < MinRegionBorder)
            {
                _local.y += 32;
                _clientRegion.y -= 4;
            }
            else if (Local.y >= MaxRegionBorder)
            {
                _local.y -= 32;
                _clientRegion.y += 4;
            }

            Base = (_clientRegion.x * 8, _clientRegion.y * 8);

            // sync locals to globals
            X = Base.x + _local.x;
            Y = Base.y + _local.y;

            Entity.NeedsSightEvaluation = true;

            UpdateRegion();
        }
    }
}