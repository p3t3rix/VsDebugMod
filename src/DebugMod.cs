using System.Diagnostics;
using DebugMod.Commands;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace DebugMod
{
    public class DebugMod : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide side)
        {
            return true;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            api.Server.Logger.EntryAdded += OnServerLogEntry;
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            api.World.Logger.EntryAdded += OnClientLogEntry;
            api.RegisterCommand(new DebugTreeCommand(api));
            api.RegisterCommand(new LangMatchDebugCommand(api));
            api.RegisterCommand(new LangListDebugCommand(api));
        }

        private void OnServerLogEntry(EnumLogType logType, string message, params object[] args)
        {
            if (logType == EnumLogType.VerboseDebug) return;
            Debug.WriteLine("[Server " + logType + "] " + message, args);
        }

        private void OnClientLogEntry(EnumLogType logType, string message, params object[] args)
        {
            if (logType == EnumLogType.VerboseDebug) return;
            Debug.WriteLine("[Client " + logType + "] " + message, args);
        }
    }
}
