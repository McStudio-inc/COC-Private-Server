// Location: ClashofClans.Protocol.Messages.Client.EndClientTurnMessage.cs
using System;
using System.Collections.Generic;
using System.Linq;
using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Messages.Client
{
    public class EndClientTurnMessage : PiranhaMessage
    {
        public EndClientTurnMessage(Device device, IByteBuffer buffer) : base(device, buffer)
        {
            RequiredState = Device.State.NotDefinied;
        }

        public int SubTick { get; set; }
        public int Count { get; set; }

        public override void Decode()
        {
            SubTick = Reader.ReadInt();
            Reader.ReadInt();
            Count = Reader.ReadInt();
        }

        public override async void Process()
        {
            if (SubTick < 0)
            {
                Logger.Log($"Client Tick ({SubTick}) is corrupted. Disconnecting.", GetType(),
                    Logger.ErrorLevel.Warning);
                Device.Disconnect($"Client Tick ({SubTick}) is corrupted.");
                return;
            }

            if (Count <= 0 || Count > 128) return;

            var home = Device.Player.Home;
            home.Tick();

            var commands = new List<LogicCommand>(Count);

            for (var i = 0; i < Count; i++)
            {
                var type = Reader.ReadInt();

                var command = LogicCommandManager.CreateCommand(Device, Reader, type);

                if (command != null)
                {
                    try
                    {
                        command.Type = type;
                        command.Decode();
                        commands.Add(command);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"Failed to decode command {type}: {ex.Message}", GetType(),
                            Logger.ErrorLevel.Error);
                    }
                }
                else
                {
                    var bytes = new byte[Reader.ReadableBytes];
                    Reader.GetBytes(Reader.ReaderIndex, bytes);
                    Logger.Log($"Command {type} is unhandled. Hex: {BitConverter.ToString(bytes)}",
                        GetType(), Logger.ErrorLevel.Warning);
                    return;
                }
            }

            Save = true;

            foreach (var command in commands.OrderBy(x => x.Tick))
            {
                try
                {
                    command.Process();
                    Logger.Log($"Command {command.Type} ({command.GetType().Name}) - Tick: {command.Tick}",
                        GetType(), Logger.ErrorLevel.Debug);
                }
                catch (Exception ex)
                {
                    Logger.Log($"Failed processing command {command.Type}: {ex.Message}", GetType(),
                        Logger.ErrorLevel.Error);
                }
            }

            home.Time.SubTick = SubTick;
        }
    }
}