// Location: ClashofClans.Protocol.Commands.Client.LogicSaveBaseLayoutCommand.cs
using System.Collections.Generic;
using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicSaveBaseLayoutCommand : LogicCommand
    {
        private int _layoutId;
        private int _state;

        public LogicSaveBaseLayoutCommand(Device device, IByteBuffer buffer)
            : base(device, buffer) { }

        public override void Decode()
        {
            _layoutId = Reader.ReadInt();
            _state = Reader.ReadInt();

            base.Decode();
        }

        public override void Process()
        {
            if (_layoutId == 6 || _layoutId == 7) return;

            var home = Device.Player?.Home;
            if (home == null) return;

            var gom = home.GameObjectManager;
            var buildings = gom.GetBuildings();
            var traps = gom.GetTraps();
            var decos = gom.GetDecos();

            // Kumpulkan semua posisi edit mode untuk cek overlap
            var allObjects = new List<(int x, int y, int w, int h)>();

            foreach (var b in buildings)
            {
                var pos = b.GetPositionLayout(_layoutId, true);
                if (pos.Item1 != -1 && pos.Item2 != -1)
                    allObjects.Add((pos.Item1, pos.Item2, b.GetWidthInTiles(), b.GetHeightInTiles()));
            }
            foreach (var t in traps)
            {
                var pos = t.GetPositionLayout(_layoutId, true);
                if (pos.Item1 != -1 && pos.Item2 != -1)
                    allObjects.Add((pos.Item1, pos.Item2, t.GetWidthInTiles(), t.GetHeightInTiles()));
            }
            foreach (var d in decos)
            {
                var pos = d.GetPositionLayout(_layoutId, true);
                if (pos.Item1 != -1 && pos.Item2 != -1)
                    allObjects.Add((pos.Item1, pos.Item2, d.GetWidthInTiles(), d.GetHeightInTiles()));
            }

            // Validasi overlap
            for (int i = 0; i < allObjects.Count; i++)
            {
                int x1 = allObjects[i].x, y1 = allObjects[i].y;
                int w1 = allObjects[i].w, h1 = allObjects[i].h;

                for (int j = i + 1; j < allObjects.Count; j++)
                {
                    int x2 = allObjects[j].x, y2 = allObjects[j].y;
                    int w2 = allObjects[j].w, h2 = allObjects[j].h;

                    if (x1 + w1 > x2 && y1 + h1 > y2 && x1 < x2 + w2 && y1 < y2 + h2)
                    {
                        Logger.Log($"SaveBaseLayout: overlap at layout {_layoutId}", null, Logger.ErrorLevel.Warning);
                        return;
                    }
                }
            }

            // Copy edit mode → permanent
            foreach (var b in buildings)
            {
                var pos = b.GetPositionLayout(_layoutId, true);
                b.SetPositionLayoutXY(pos.Item1, pos.Item2, _layoutId, false);
            }
            foreach (var t in traps)
            {
                var pos = t.GetPositionLayout(_layoutId, true);
                t.SetPositionLayoutXY(pos.Item1, pos.Item2, _layoutId, false);
            }
            foreach (var d in decos)
            {
                var pos = d.GetPositionLayout(_layoutId, true);
                d.SetPositionLayoutXY(pos.Item1, pos.Item2, _layoutId, false);
            }
        }
    }
}