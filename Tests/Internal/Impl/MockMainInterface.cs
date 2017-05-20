﻿using CScape.Core.Game.Interface;
using JetBrains.Annotations;

namespace CScape.Dev.Tests.Internal.Impl
{
    public class MockMainInterface : SingleUserShowableInterface
    {
        public MockMainInterface(int id, [CanBeNull] IButtonHandler buttonHandler = null) : base(id, buttonHandler)
        {
        }

        protected override bool InternalRegister(IInterfaceManagerApiBackend api)
        {
            if (!api.Frontend.CanShow(InterfaceType.Main)) return false;
            api.Main = this;
            return true;
        }

        protected override void InternalUnregister()
        {
            Api.Main = null;
        }

        protected override bool CanCloseRigtNow() => true;

        protected override void InternalClose() { }
        public override void Show() { }
    }
}