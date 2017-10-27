﻿using System.Net;
using CScape.Models;

namespace CScape.Dev.Tests.Impl
{
    public class MockConfig : IGameServerConfig
    {
        public int MaxPlayers { get; }
        public int MaxNpcs { get; }
        public int Revision { get; } 
        public string Version { get; }
        public EndPoint ListenEndPoint { get; }
        public int Backlog { get; }
        public int SocketSendTimeout { get; }
        public int SocketReceiveTimeout { get; }
        public int TickRate { get; }
        public int EntityGcInternalMs { get; }
        public string PrivateLoginKeyDir { get; }
        public string Greeting { get; }
    }
}
