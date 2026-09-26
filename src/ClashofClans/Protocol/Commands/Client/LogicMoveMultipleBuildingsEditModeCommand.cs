// Location: ClashofClans.Protocol.Commands.Client.LogicMoveMultipleBuildingsEditModeCommand.cs
using System;
using System.Collections.Generic;
using ClashofClans.Logic;
using ClashofClans.Logic.Manager.Items;
using ClashofClans.Logic.Manager.Items.GameObjects;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicMoveMultipleBuildingsEditModeCommand : LogicCommand
    {
        private int _layoutId;
        private readonly List<int> _gameObjectIds = new List<int>();
        private readonly List<int> _xPositions = new List<int>();
        private readonly List<int> _yPositions = new List<int>();

        public LogicMoveMultipleBuildingsEditModeCommand(Device device, IByteBuffer buffer)
            : base(device, buffer) { }

        public override void Decode()
        {
            _layoutId = Reader.ReadInt();

            int count = Math.Min(500, Reader.ReadInt());

            for (int i = 0; i < count; i++)
            {
                _xPositions.Add(Reader.ReadInt());
                _yPositions.Add(Reader.ReadInt());
                _gameObjectIds.Add(Reader.ReadInt());
            }

            base.Decode();
        }

        public override void Process()
        {
            if (_layoutId == 6 || _layoutId == 7) return;

            var home = Device.Player?.Home;
            if (home == null) return;

            int count = _gameObjectIds.Count;
            if (count <= 0 || count > 500) return;

            var gom = home.GameObjectManager;
            var gameObjects = new List<GameObject>(count);

            for (int i = 0; i < count; i++)
            {
                var obj = gom.GetGameObjectById(_gameObjectIds[i]);
                if (obj == null) return;

                bool valid = obj is Building || obj is Trap || obj is Deco;
                if (!valid) return;

                gameObjects.Add(obj);
            }

            for (int i = 0; i < count; i++)
            {
                gameObjects[i].SetPositionLayoutXY(_xPositions[i], _yPositions[i], _layoutId, true);
            }
        }
    }
}