using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicSetPersistentBoolCommand : LogicCommand
    {
        public LogicSetPersistentBoolCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public int Index { get; set; }
        public bool Value { get; set; }

        public override void Decode()
        {
            Index = Reader.ReadInt();
            Value = Reader.ReadBoolean();
            base.Decode();
        }

        public override void Process()
        {
            if (Index == 0)
                Device.Player.Home.PersistentBool0 = Value;
        }
    }
}