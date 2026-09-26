using ClashofClans.Extensions;
using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicFreeWorkerCommand : LogicCommand
    {
        public LogicFreeWorkerCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public int SecondsLeft { get; set; }
        public bool CommandEmbed { get; set; }

        public override void Decode()
        {
            SecondsLeft = Reader.ReadInt();
            CommandEmbed = Reader.ReadBoolean();
            base.Decode();
        }

        public override void Process()
        {
            var home = Device.Player.Home;
            var cost = GamePlayUtil.GetSpeedUpCost(SecondsLeft);

            if (!home.UseDiamonds(cost)) return;

            home.GameObjectManager.FastForward(SecondsLeft);
        }
    }
}