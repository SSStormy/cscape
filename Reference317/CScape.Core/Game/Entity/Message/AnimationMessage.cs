using System;
using CScape.Models.Game.Entity;
using JetBrains.Annotations;

namespace CScape.Core.Game.Entity.Message
{
    public sealed class AnimationMessage : IGameMessage
    {
        [NotNull]
        public Animation Animation { get; }
        public int EventId => (int)MessageId.NewAnimation;

        public AnimationMessage([NotNull] Animation animation)
        {
            Animation = animation ?? throw new ArgumentNullException(nameof(animation));
        }
    }
}