using CScape.Core.Game.Entities.Component;
using CScape.Core.Network.Entity.Flag;

namespace CScape.Core.Network.Entity
{
    public sealed class LocalUpdateWriter : PlayerUpdateWriter
    {
        protected override PlayerFlag GetHeader()
        {
            PlayerFlag retval = 0;

            foreach (var flag in this)
            {
                // don't sync chat messages that the player sent
                if (flag.Type == FlagType.ChatMessage)
                {
                    var chat = flag as PlayerChatUpdateFlag;
                    if (chat.Chat.IsForced)
                        retval |= flag.Type.ToPlayer();
                }
                else
                {
                    retval |= flag.Type.ToPlayer();
                }
            }


            return retval;
        }

        public override bool NeedsUpdate()
        {
            return GetHeader() != 0;
        }

        public LocalUpdateWriter(FlagAccumulatorComponent flags) : base(flags)
        {
        }
    }
}