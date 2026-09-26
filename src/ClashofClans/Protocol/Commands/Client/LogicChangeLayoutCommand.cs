// Location: ClashofClans.Protocol.Commands.Client.LogicChangeLayoutCommand.cs
using System.Numerics;
using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicChangeLayoutCommand : LogicCommand
    {
        private int _layoutId;
        private int _layoutType;

        public LogicChangeLayoutCommand(Device device, IByteBuffer buffer)
            : base(device, buffer) { }

        public override void Decode()
        {
            _layoutId = Reader.ReadInt();
            _layoutType = Reader.ReadInt();

            if (_layoutId == 6 || _layoutId == 7)
                _layoutId = -1;

            base.Decode();
        }

        public override void Process()
        {
            if (_layoutId == -1) return;

            var home = Device.Player?.Home;
            if (home == null) return;

            var gom = home.GameObjectManager;

            // Validasi semua objek punya permanent layout position
            foreach (var b in gom.GetBuildings())
            {
                var pos = b.GetPositionLayout(_layoutId, false);
                if (pos.Item1 == -1 || pos.Item2 == -1) return;
            }
            foreach (var t in gom.GetTraps())
            {
                var pos = t.GetPositionLayout(_layoutId, false);
                if (pos.Item1 == -1 || pos.Item2 == -1) return;
            }

            // Apply posisi layout ke posisi aktual
            foreach (var b in gom.GetBuildings())
            {
                var pos = b.GetPositionLayout(_layoutId, false);
                b.Position = new Vector2(pos.Item1, pos.Item2);
            }
            foreach (var t in gom.GetTraps())
            {
                var pos = t.GetPositionLayout(_layoutId, false);
                t.Position = new Vector2(pos.Item1, pos.Item2);
            }
            foreach (var d in gom.GetDecos())
            {
                var pos = d.GetPositionLayout(_layoutId, false);
                if (pos.Item1 != -1 && pos.Item2 != -1)
                    d.Position = new Vector2(pos.Item1, pos.Item2);
            }

            home.ActiveLayout = _layoutId;
        }
    }
}