// Location: ClashofClans.Protocol.LogicCommandManager.cs
using ClashofClans.Logic;
using ClashofClans.Protocol.Commands.Client;
using ClashofClans.Protocol.Commands.Server;
using ClashofClans.Protocol.Messages.Client.Alliance;
using DotNetty.Buffers;

namespace ClashofClans.Protocol
{
    public class LogicCommandManager
    {
        public static LogicCommand CreateCommand(Device device, IByteBuffer buffer, int commandId)
        {
            LogicCommand command = null;

            switch ((LogicCommandType)commandId)
            {
                case LogicCommandType.JOIN_ALLIANCE:
                    command = new LogicJoinAllianceCommand(device);
                    break;
                case LogicCommandType.BUY_BUILDING:
                    command = new LogicBuyBuildingCommand(device, buffer);
                    break;
                case LogicCommandType.MOVE_BUILDING:
                    command = new LogicMoveBuildingCommand(device, buffer);
                    break;
                case LogicCommandType.UPGRADE_BUILDING:
                    command = new LogicUpgradeBuildingCommand(device, buffer);
                    break;
                case LogicCommandType.SPEED_UP_CONSTRUCTION:
                    command = new LogicSpeedUpConstructionCommand(device, buffer);
                    break;
                case LogicCommandType.CANCEL_CONSTRUCTION:
                    command = new LogicCancelConstructionCommand(device, buffer);
                    break;
                case LogicCommandType.COLLECT_RESOURCES:
                    command = new LogicCollectResourcesCommand(device, buffer);
                    break;
                case LogicCommandType.CHANGE_ALLIANCE_CHAT_FILTER:
                    command = new LogicChangeAllianceChatFilterCommand(device, buffer);
                    break;
                case LogicCommandType.SET_PERSISTENT_BOOL:
                    command = new LogicSetPersistentBoolCommand(device, buffer);
                    break;
                case LogicCommandType.BOOST_TRAINING:
                    command = new LogicBoostTrainingCommand(device, buffer);
                    break;
                case LogicCommandType.CLEAR_OBSTACLE:
                    command = new LogicClearObstacleCommand(device, buffer);
                    break;
                case LogicCommandType.TRAIN_UNIT:
                    command = new LogicTrainUnitCommand(device, buffer);
                    break;
                case LogicCommandType.CANCEL_UNIT_PRODUCTION:
                    command = new LogicCancelUnitProductionCommand(device, buffer);
                    break;
                case LogicCommandType.BUY_TRAP:
                    command = new LogicBuyTrapCommand(device, buffer);
                    break;
                case LogicCommandType.BUY_DECO:
                    command = new LogicBuyDecoCommand(device, buffer);
                    break;
                case LogicCommandType.SPEED_UP_TRAINING:
                    command = new LogicSpeedUpTrainingCommand(device, buffer);
                    break;
                case LogicCommandType.UPGRADE_UNIT:
                    command = new LogicUpgradeUnitCommand(device, buffer);
                    break;
                case LogicCommandType.SPEED_UP_UPGRADE_UNIT:
                    command = new LogicSpeedUpUpgradeUnitCommand(device, buffer);
                    break;
                case LogicCommandType.BUY_RESOURCES:
                    command = new LogicBuyResourcesCommand(device, buffer);
                    break;
                case LogicCommandType.MISSION_PROGRESS:
                    command = new LogicMissionProgressCommand(device, buffer);
                    break;
                case LogicCommandType.UNLOCK_BUILDING:
                    command = new LogicUnlockBuildingCommand(device, buffer);
                    break;
                case LogicCommandType.FREE_WORKER:
                    command = new LogicFreeWorkerCommand(device, buffer);
                    break;
                case LogicCommandType.CLAIM_ACHIEVEMENT_REWARD:
                    command = new LogicCollectAchievementCommand(device, buffer);
                    break;
                case LogicCommandType.TOGGLE_ATTACK_MODE:
                    command = new LogicToggleAttackModeCommand(device, buffer);
                    break;
                case LogicCommandType.BOOST_BUILDING:
                    command = new LogicBoostBuildingCommand(device, buffer);
                    break;
                case LogicCommandType.UPGRADE_HERO:
                    command = new LogicUpgradeHeroCommand(device, buffer);
                    break;
                case LogicCommandType.SPEED_UP_HERO_UPGRADE:
                    command = new LogicSpeedUpHeroUpgradeCommand(device, buffer);
                    break;
                case LogicCommandType.TOGGLE_HERO_SLEEP:
                    command = new LogicToggleHeroSleepCommand(device, buffer);
                    break;
                case LogicCommandType.NEW_SHOP_ITEMS_SEEN:
                    command = new LogicNewShopItemsSeenCommand(device, buffer);
                    break;
                case LogicCommandType.MOVE_MULTIPLE_BUILDING:
                    command = new LogicMoveMultipleBuildingsCommand(device, buffer);
                    break;
                case LogicCommandType.LEAGUE_NOTIFICATION_SEEN:
                    command = new LogicLeagueNotificationsSeenCommand(device, buffer);
                    break;
                case LogicCommandType.NEWS_SEEN:
                    command = new LogicNewsSeenCommand(device, buffer);
                    break;
                case LogicCommandType.EDIT_MODE_SHOWN:
                    command = new LogicEditModeShownCommand(device, buffer);
                    break;
                case LogicCommandType.UPGRADE_MULTIPLE_BUILDINGS:
                    command = new LogicUpgradeMultipleBuildingsCommand(device, buffer);
                    break;
                case LogicCommandType.REMOVE_UNITS:
                    command = new LogicRemoveUnitCommand(device, buffer);
                    break;
                case LogicCommandType.SET_LAYOUT_STATE:
                    command = new LogicSetLayoutStateCommand(device, buffer);
                    break;
                case LogicCommandType.CHANGE_LAYOUT:
                    command = new LogicChangeLayoutCommand(device, buffer);
                    break;
                case LogicCommandType.COPY_LAYOUT:
                    command = new LogicCopyLayoutCommand(device, buffer);
                    break;
                case LogicCommandType.MOVE_BUILDING_EDIT_MODE:
                    command = new LogicMoveBuildingEditModeCommand(device, buffer);
                    break;
                case LogicCommandType.MOVE_MULTIPLE_BUILDINGS_EDIT_MODE:
                    command = new LogicMoveMultipleBuildingsEditModeCommand(device, buffer);
                    break;
                case LogicCommandType.SAVE_BASE_LAYOUT:
                    command = new LogicSaveBaseLayoutCommand(device, buffer);
                    break;
                case LogicCommandType.SWAP_BUILDING:
                    command = new LogicSwapBuildingsCommand(device, buffer);
                    break;
                case LogicCommandType.PLACE_UNPLACED_OBJECT:
                    command = new LogicPlacePendingBuildingCommand(device, buffer);
                    break;
                case LogicCommandType.BUY_WALL_BLOCK:
                    command = new LogicBuyWallCommand(device, buffer);
                    break;
                case LogicCommandType.SET_CURRENT_VILLAGE:
                    command = new LogicSwitchVillageStateCommand(device, buffer);
                    break;
                case LogicCommandType.TRAIN_UNIT_VILLAGE2:
                    command = new LogicTrainUnitVillage2Command(device, buffer);
                    break;
                case LogicCommandType.SPEED_UP_TRAINING_VILLAGE2:
                    command = new LogicSpeedUpTrainingVillage2Command(device, buffer);
                    break;
                case LogicCommandType.EVENT_SEEN:
                    command = new LogicEventsSeenCommand(device, buffer);
                    break;
                case LogicCommandType.MATCHMAKE_VILLAGE2:
                    command = new LogicMatchmakingVillage2Command(device, buffer);
                    break;
                case LogicCommandType.ACCOUNT_BOUND:
                    command = new LogicAccountBoundCommand(device, buffer);
                    break;
                case LogicCommandType.SET_LAST_ALLIANCE_LEVEL:
                    command = new LogicSetLastAllianceLevelCommand(device, buffer);
                    break;
                case LogicCommandType.BUY_DAILY_DEAL_ITEM:
                    command = new LogicBuyDailyDealItemCommand(device, buffer);
                    break;
                case LogicCommandType.UPGRADE_WEAPON:
                    command = new LogicUpgradeWeaponCommand(device, buffer);
                    break;
                case LogicCommandType.TOGGLE_CLAN_CASTLE_SLEEP:
                    command = new LogicToggleClanCastleSleepCommand(device, buffer);
                    break;
                case LogicCommandType.ATTACK_NPC:
                    command = new LogicAttackNpcCommand(device, buffer);
                    break;
                case LogicCommandType.TRIGGER_HERO_ABILITY_ON_DEATH:
                    command = new LogicTriggerHeroAbilityOnDeathCommand(device, buffer);
                    break;
                case LogicCommandType.MOVE_BUILDING_688:
                    command = new LogicMoveBuildingCommand(device, buffer);
                    break;
                case LogicCommandType.PLACE_ATTACKER:
                    command = new LogicPlaceAttackerCommand(device, buffer);
                    break;
                case LogicCommandType.END_COMBAT:
                    command = new LogicEndCombatCommand(device, buffer);
                    break;
                case LogicCommandType.PLACE_HERO:
                    command = new LogicPlaceHeroCommand(device, buffer);
                    break;
                case LogicCommandType.TRIGGER_HERO_ABILITY:
                    command = new LogicTriggerHeroAbility(device, buffer);
                    break;
                case LogicCommandType.TRIGGER_UNIT_ABILITY:
                    command = new LogicTriggerUnitAbility(device, buffer);
                    break;
                case LogicCommandType.SET_HERO_MODE_BATTLE:
                    command = new LogicSetHeroModeBattleCommand(device, buffer);
                    break;
                case LogicCommandType.CAST_SPELL:
                    command = new LogicCastSpellCommand(device, buffer);
                    break;
                case LogicCommandType.PLACE_HERO_788:
                    command = new LogicPlaceHeroCommand(device, buffer);
                    break;
                case LogicCommandType.MATCHMAKING:
                    command = new LogicMatchmakingCommand(device, buffer);
                    break;
                case LogicCommandType.ACCEPT_CHAT_RULES:
                    command = new LogicAcceptChatRulesCommand(device, buffer);
                    break;

                default:
                    Logger.Log($"Command {commandId} ({(LogicCommandType)commandId}) is unhandled.",
                        null, Logger.ErrorLevel.Warning);
                    break;
            }

            return command;
        }
    }
}