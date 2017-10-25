﻿using System;

namespace CScape.Core.Game.Entity.Component
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public sealed class RequiresComponent : Attribute
    {
        public Type FragmentType { get; }

        public RequiresComponent(Type fragType)
        {
            FragmentType = fragType;
        }
    }
}