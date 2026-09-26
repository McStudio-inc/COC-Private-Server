using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicBoostTrainingCommand : LogicCommand
    {
        public LogicBoostTrainingCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public int ProductionType { get; set; }

        public override void Decode()
        {
            ProductionType = Reader.ReadInt();
            base.Decode();
        }

        public override void Process()
        {
            // ProductionType: 0 = troops, 1 = spells
            // TODO: implement boost cost & deduction saat unit production system sudah ada
        }
    }
}