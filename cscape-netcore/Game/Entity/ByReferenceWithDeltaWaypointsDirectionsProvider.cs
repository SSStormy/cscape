using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace CScape.Game.Entity
{
    public sealed class ByReferenceWithDeltaWaypointsDirectionsProvider : IDirectionsProvider
    {
        private (int x, int y) _local;
        private readonly (int x, int y) _reference;
        private readonly (sbyte x, sbyte y)[] _deltaWaypoints;
        private  (int, int) _target;
        private readonly IEnumerator<(sbyte, sbyte)> _nextProvider;
        private int _idx;
        private bool _isFirst = true;

        [NotNull]
        public Transform Pos { get; }

        public ByReferenceWithDeltaWaypointsDirectionsProvider(
            [NotNull] Transform pos,
            (int, int) reference,
            (sbyte, sbyte)[] deltaWaypoints)
        {
            _reference = reference;
            _target = reference;
            _deltaWaypoints = deltaWaypoints;
            Pos = pos;
            _local = (Pos.LocalX, Pos.LocalY);

            // create ienumerable
            _nextProvider = NextProvider();
        }

        public (sbyte, sbyte) GetNextDir()
        {
            return _nextProvider.Current;
        }

        public bool IsDone()
        {
            if (!_nextProvider.MoveNext())
                return true;

            return false;
        }

        public void Dispose()
        {
            _nextProvider.Dispose();
        }

        private IEnumerator<(sbyte x, sbyte y)> NextProvider()
        {
            foreach (var delta in _deltaWaypoints)
            {
                if (_isFirst)
                {
                    _target = _reference;
                    _isFirst = false;
                }
                else
                    _target = (_reference.x + delta.x, _reference.y + delta.y);

                foreach (var d in Interpolate(_local, _target))
                {
                    yield return d;

                    // keep track of locals ourselves incase transform 
                    // update dx and dy kick in and changes locals in the transform
                    _local.x += d.x;
                    _local.y += d.y;
                }
            }
        }

        /// <summary>
        /// Interpolates movement between local x y's local and target.
        /// Assumes no obstacles between the two points.
        /// todo : collision checking
        /// </summary>
        /// <param name="local"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private IEnumerable<(sbyte x, sbyte y)> Interpolate((int x, int y) local, (int x, int y) target)
        {
            var max = Math.Max(Math.Abs(local.x - target.x), Math.Abs(local.y - target.y));
            if (max == 0)
                yield break;

            var diffX = local.x < target.x ? (sbyte)1 : (sbyte)-1;
            var diffY = local.y < target.y ? (sbyte)1 : (sbyte)-1;

            for (var i = 0; i < max; i++)
            {
                sbyte dX = 0;
                sbyte dY = 0;

                if (local.x != target.x)
                    local.x += dX = diffX;

                if (local.y != target.y)
                    local.y += dY = diffY;

                yield return (dX, dY);
            }
        }
    }
}