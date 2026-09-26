using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;

namespace ClashofClans.Protocol.Commands.Server
{
    public class LogicJoinAllianceCommand : LogicCommand
    {
        public LogicJoinAllianceCommand(Device device) : base(device)
        {
            Type = 1;
        }

        public long AllianceId { get; set; }
        public string AllianceName { get; set; }
        public int AllianceBadgeId { get; set; }
        public int AllianceExpLevel { get; set; }
        public bool AllianceCreate { get; set; }

        public override void Encode()
        {
            Data.WriteLong(AllianceId);
            Data.WriteScString(AllianceName);
            Data.WriteInt(AllianceBadgeId);
            Data.WriteBoolean(AllianceCreate);
            Data.WriteInt(AllianceExpLevel);
        }
    }
}