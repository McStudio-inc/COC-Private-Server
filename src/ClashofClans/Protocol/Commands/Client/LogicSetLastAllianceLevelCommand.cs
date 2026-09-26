// Location: ClashofClans.Protocol.Commands.Client

using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicSetLastAllianceLevelCommand : LogicCommand
    {
        public LogicSetLastAllianceLevelCommand(Device device, IByteBuffer buffer) : base(device, buffer)
        {
        }

        public int AllianceLevel { get; set; }

        public override void Decode()
        {
            AllianceLevel = Reader.ReadInt();
            base.Decode();
        }

        public override void Process()
        {
            var home = Device.Player.Home;
            if (!home.AllianceInfo.HasAlliance) return;

            home.AllianceInfo.Level = AllianceLevel;
        }
    }
}