using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicChangeAllianceChatFilterCommand : LogicCommand
    {
        public LogicChangeAllianceChatFilterCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public bool Enabled { get; set; }

        public override void Decode()
        {
            Enabled = Reader.ReadBoolean();
            base.Decode();
        }

        public override void Process()
        {
            Device.Player.Home.AllianceChatFilter = Enabled;
        }
    }
}