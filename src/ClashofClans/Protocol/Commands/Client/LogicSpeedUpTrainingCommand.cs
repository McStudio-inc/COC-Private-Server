using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicSpeedUpTrainingCommand : LogicCommand
    {
        public LogicSpeedUpTrainingCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public int GameObjectId { get; set; }
        public bool SpellProduction { get; set; }

        public override void Decode()
        {
            GameObjectId = Reader.ReadInt();
            SpellProduction = Reader.ReadBoolean();
            base.Decode();
        }

        public override void Process()
        {
            // TODO: implement saat unit production queue system sudah ada
        }
    }
}