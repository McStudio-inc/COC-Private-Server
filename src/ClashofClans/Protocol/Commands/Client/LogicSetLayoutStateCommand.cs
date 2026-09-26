// Location: ClashofClans.Protocol.Commands.Client.LogicSetLayoutStateCommand.cs
using ClashofClans.Logic;
using ClashofClans.Logic.Manager.Items.GameObjects;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicSetLayoutStateCommand : LogicCommand
    {
        private int _layoutId;
        private int _state;
        private bool _updateListener;

        public LogicSetLayoutStateCommand(Device device, IByteBuffer buffer)
            : base(device, buffer) { }

        public override void Decode()
        {
            _layoutId = Reader.ReadInt();
            _state = Reader.ReadInt();
            _updateListener = Reader.ReadByte() != 0;

            base.Decode(); // baca Tick
        }

        public override void Process()
        {
            // Supercell: layoutId 6 = -10, layoutId 7 = -11
            if (_layoutId == 6 || _layoutId == 7) return;

            var home = Device.Player?.Home;
            if (home == null) return;

            var gom = home.GameObjectManager;

            // Kalau state == 0: reset semua posisi objek di layout ini
            if (_state == 0)
            {
                foreach (var building in gom.GetBuildings())
                    building.SetPositionLayoutXY(-1, -1, _layoutId);

                foreach (var trap in gom.GetTraps())
                    trap.SetPositionLayoutXY(-1, -1, _layoutId);

                foreach (var deco in gom.GetDecos())
                    deco.SetPositionLayoutXY(-1, -1, _layoutId);
            }

            home.SetLayoutState(_layoutId, _state);

            // Update ActiveLayout jika state aktif
            if (_state == 1)
                home.ActiveLayout = _layoutId;
        }
    }
}