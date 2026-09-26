using ClashofClans.Extensions;
using ClashofClans.Files;
using ClashofClans.Logic;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicBuyResourcesCommand : LogicCommand
    {
        public LogicBuyResourcesCommand(Device device, IByteBuffer buffer) : base(device, buffer)
        {
        }

        public int ResourceCount { get; set; }
        public int ResourceId { get; set; }
        public int Resource2Count { get; set; }
        public int Resource2Id { get; set; }
        public bool HasSubCommand { get; set; }
        public int SubCommandType { get; set; }

        public override void Decode()
        {
            ResourceCount = Reader.ReadInt();
            ResourceId = Reader.ReadInt();
            Resource2Count = Reader.ReadInt();

            if (Resource2Count > 0)
                Resource2Id = Reader.ReadInt();

            HasSubCommand = Reader.ReadBoolean();

            if (HasSubCommand)
                SubCommandType = Reader.ReadInt();

            base.Decode();
        }

        public override void Process()
        {
            var home = Device.Player.Home;

            // Ambil nama resource dari Global ID
            string resourceName = GetResourceName(ResourceId);
            if (resourceName == null || resourceName == "Diamonds") return;

            int cost = GetDiamondCost(resourceName, ResourceCount);
            if (cost < 0) return;

            if (Resource2Count > 0)
            {
                string resource2Name = GetResourceName(Resource2Id);
                if (resource2Name == null || resource2Name == "Diamonds") return;

                int cost2 = GetDiamondCost(resource2Name, Resource2Count);
                if (cost2 < 0) return;

                int totalCost = cost + cost2;
                if (home.Diamonds < totalCost) return;

                if (!home.UseDiamonds(totalCost)) return;

                home.AddResourceByName(resourceName, ResourceCount);
                home.AddResourceByName(resource2Name, Resource2Count);
            }
            else
            {
                if (home.Diamonds < cost) return;

                if (!home.UseDiamonds(cost)) return;

                home.AddResourceByName(resourceName, ResourceCount);
            }

            if (HasSubCommand && SubCommandType >= 500 && SubCommandType < 700)
            {
                var subCommand = LogicCommandManager.CreateCommand(Device, Reader, SubCommandType);
                if (subCommand != null)
                {
                    subCommand.Type = SubCommandType;
                    subCommand.Decode();
                    subCommand.Process();
                }
            }
        }

        /// <summary>
        ///     Ambil nama resource dari Global ID (3000001 = Gold, dst)
        /// </summary>
        private string GetResourceName(int globalId)
        {
            switch (globalId)
            {
                case 3000001: return "Gold";
                case 3000002: return "Elixir";
                case 3000003: return "DarkElixir";
                case 3000007: return "Gold2";
                case 3000008: return "Elixir2";
                default: return null;
            }
        }

        /// <summary>
        ///     Hitung biaya diamonds untuk membeli resource
        /// </summary>
        private int GetDiamondCost(string resourceName, int amount)
        {
            if (amount <= 0) return 0;

            var util = new GamePlayUtil();
            return util.GetResourceDiamondCost(amount, resourceName);
        }
    }
}