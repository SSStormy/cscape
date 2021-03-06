using CScape.Models.Data;
using CScape.Models.Game.Item;

namespace CScape.Core.Network.Packet
{
    /// <summary>
    /// Encodes a packet which removes a ground item whose id matches the given one at the given packed x/y region coords
    /// </summary>
    public class DeleteGroundItemPacket : BaseGroundObjectPacket
    {
        public const int Id = 156;
        private readonly int _id;

        public DeleteGroundItemPacket(
            ItemStack item,
            (int x, int y) off)
            :base(off.x, off.y)
        {
            _id = item.Id.ItemId - 1;
        }

        protected override void InternalSend(OutBlob stream)
        {
            stream.BeginPacket(Id);

            stream.Write(PackedPos);
            stream.Write16((short)(_id));

            stream.EndPacket();
        }
    }
}