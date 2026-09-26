using ClashofClans.Files;
using ClashofClans.Files.Logic;
using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicAccountBoundCommand : LogicCommand
    {
        public LogicAccountBoundCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public int Bound { get; set; }

        public override void Decode()
        {
            Bound = Reader.ReadInt();
            base.Decode();
        }

        public override void Process()
        {
            var home = Device.Player.Home;

            home.AccountBound = Bound == 1;

            var table = Csv.Tables.Get(Csv.Files.Achievements);
            if (table == null) return;

            for (var i = 0; i < table.Count(); i++)
            {
                var data = table.GetDataWithInstanceId<Achievements>(i);
                if (data == null) continue;

                var id = 23000000 + i;

                if (home.CompletedAchievements.Contains(id)) continue;

                if (!home.AchievementProgress.ContainsKey(id))
                    home.AchievementProgress[id] = 0;
            }
        }
    }
}