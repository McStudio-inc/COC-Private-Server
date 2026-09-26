// Location: ClashofClans.Protocol.Commands.Client.LogicMoveBuildingEditModeCommand.cs
using ClashofClans.Logic;
using ClashofClans.Logic.Manager.Items.GameObjects;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicMoveBuildingEditModeCommand : LogicCommand
    {
        private int _x;
        private int _y;
        private int _gameObjectId;
        private int _layoutId;

        public LogicMoveBuildingEditModeCommand(Device device, IByteBuffer buffer)
            : base(device, buffer) { }

        public override void Decode()
        {
            _x = Reader.ReadInt();
            _y = Reader.ReadInt();
            _gameObjectId = Reader.ReadInt();
            _layoutId = Reader.ReadInt();

            base.Decode();
        }

        public override void Process()
        {
            if (_layoutId == 6 || _layoutId == 7) return;

            var home = Device.Player?.Home;
            if (home == null) return;

            var gameObject = home.GameObjectManager.GetGameObjectById(_gameObjectId);
            if (gameObject == null) return;

            // Hanya Building, Trap, Deco yang valid
            bool isBuilding = gameObject is Building;
            bool isTrap = gameObject is Trap;
            bool isDeco = gameObject is Deco;

            if (!isBuilding && !isTrap && !isDeco) return;

            // Wall tidak bisa di-move via edit mode individual
            if (isBuilding)
            {
                var building = (Building)gameObject;
                if (building.WallIndex != 0) return;
            }

            gameObject.SetPositionLayoutXY(_x, _y, _layoutId, true);
        }
    }
}