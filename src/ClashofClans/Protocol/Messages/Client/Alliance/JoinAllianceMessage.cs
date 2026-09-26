using ClashofClans.Database;
using ClashofClans.Logic;
using ClashofClans.Logic.Clan;
using ClashofClans.Protocol.Commands.Server;
using ClashofClans.Protocol.Messages.Server;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;
using ClanAlliance = ClashofClans.Logic.Clan.Alliance;
namespace ClashofClans.Protocol.Messages.Client.Alliance
{
    public class JoinAllianceMessage : PiranhaMessage
    {
        public JoinAllianceMessage(Device device, IByteBuffer buffer) : base(device, buffer)
        {
            RequiredState = Device.State.Home;
        }

        public long AllianceId { get; set; }

        public override void Decode()
        {
            AllianceId = Reader.ReadLong();
        }

        public override async void Process()
        {
            var alliance = await AllianceDb.GetAsync(AllianceId);
            if (alliance == null) return;

            var home = Device.Player.Home;
            var info = home.AllianceInfo;

            info.Id = alliance.Id;
            info.Name = alliance.Name;
            info.Badge = alliance.Badge;
            info.Role = (int)ClanAlliance.Role.Member;
            info.Level = 1;

            alliance.Add(new AllianceMember(Device.Player, ClanAlliance.Role.Member));
            alliance.Save();
            Device.Player.Save();

            await new AvailableServerCommandMessage(Device)
            {
                Command = new LogicJoinAllianceCommand(Device)
                {
                    AllianceId = alliance.Id,
                    AllianceName = alliance.Name,
                    AllianceBadgeId = alliance.Badge,
                    AllianceExpLevel = 1,
                    AllianceCreate = false
                }.Handle()
            }.SendAsync();
        }
    }
}