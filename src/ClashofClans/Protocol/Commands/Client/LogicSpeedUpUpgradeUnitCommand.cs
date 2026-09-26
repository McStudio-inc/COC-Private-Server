using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicSpeedUpUpgradeUnitCommand : LogicCommand
    {
        public LogicSpeedUpUpgradeUnitCommand(Device device, IByteBuffer buffer) : base(device, buffer) { }

        public int GameObjectId { get; set; }

        public override void Decode()
        {
            GameObjectId = Reader.ReadInt();
            base.Decode();
        }

        public override void Process()
        {
            var home = Device.Player.Home;
            var building = home.GameObjectManager.GetBuildings().Find(x => x.Id == GameObjectId);
            if (building == null) return;

            var component = building.UnitUpgradeComponent;
            if (component == null) return;

            // TODO: implement SpeedUp saat UnitUpgradeComponent.Timer sudah aktif
        }
    }
}