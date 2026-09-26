using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicCopyLayoutCommand : LogicCommand
    {
        public LogicCopyLayoutCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public int InputLayoutId { get; set; }
        public int OutputLayoutId { get; set; }

        public override void Decode()
        {
            InputLayoutId = Reader.ReadInt();
            OutputLayoutId = Reader.ReadInt();

            if (InputLayoutId < 0) InputLayoutId = 0;
            if (InputLayoutId > 7) InputLayoutId = 7;
            if (OutputLayoutId < 0) OutputLayoutId = 0;
            if (OutputLayoutId > 7) OutputLayoutId = 7;

            base.Decode();
        }

        public override void Process()
        {
            if (InputLayoutId == 6 || InputLayoutId == 7) return;
            if (OutputLayoutId == 6 || OutputLayoutId == 7) return;
        }
    }
}