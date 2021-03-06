using System;
using CScape.Models.Game.Entity;
using CScape.Models.Game.Interface;
using JetBrains.Annotations;

namespace CScape.Models.Game.Item
{
    /// <summary>
    /// Defines how an item with a unique id should behave.
    /// </summary>
    public interface IItemDefinition : IEquatable<IItemDefinition>
    {
        /// <summary>
        /// The unique id of the item.
        /// </summary>
        int ItemId { get; }

        /// <summary>
        /// The name of the item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The maximum stack amount of the item. Domain: (0, int.MaxValue]
        /// </summary>
        int MaxAmount { get; }

        /// <summary>
        /// Whether the item is tradable or not.
        /// </summary>
        bool IsTradable { get; }
        
        /// <summary>
        /// How much, in kg, one of the item weighs.
        /// </summary>
        float Weight { get; }

        /// <summary>
        /// Whether the item is noted or not.
        /// </summary>
        bool IsNoted { get; }

        /// <summary>
        /// If item is noted: the id of the real item ; If item isn't noted: if item is notable, the id of the note, else, -1.
        /// </summary>
        int NoteSwitchId { get; }

        /// <summary>
        /// Called whenever this type of item is used with another item.
        /// <param name="entity">The entity which used the two items together.</param>
        /// </summary>
        void UseWith(IEntity entity, ItemStack other);

        /// <summary>
        /// Called whenever an action occurs on an item with this definition.
        /// </summary>
        /// <param name="itemIndexInContainer">The index of the item in the <see cref="itemsContainer"/>.</param>
        /// <param name="containerInterfaceMetadata">The metadata of the interface that contains the <see cref="itemsContainer"/> container.</param>
        /// <param name="item">The item stack of this item.</param>
        /// <param name="parentEntity">The entity which invoked the action.</param>
        /// <param name="actionId">The implementation specific id of the action.</param>
        /// <param name="itemsContainer">The parent items container in which the <see cref="item"/> is resising.</param>
        void OnAction(
            [NotNull] IEntity parentEntity, 
            [NotNull] IItemContainer itemsContainer,
            int itemIndexInContainer,
            InterfaceMetadata containerInterfaceMetadata, 
            [NotNull] ItemStack item, 
            int actionId);
    }
}