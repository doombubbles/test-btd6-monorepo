using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Mods;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using MegaKnowledge;
using MegaKnowledge.MegaKnowledges;
using MegaKnowledge.MegaKnowledges.Support;
using MelonLoader;
using ModHelperData = MegaKnowledge.ModHelperData;

[assembly: MelonInfo(typeof(MegaKnowledgeMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MegaKnowledge
{
    public class MegaKnowledgeMod : BloonsTD6Mod
    {
        private static readonly Dictionary<string, TowerModel> BackupTowerModels = new Dictionary<string, TowerModel>();

        public override void OnMainMenu()
        {
            foreach (var towerModel in Game.instance.model.towers)
            {
                foreach (var megaKnowledge in ModContent.GetContent<ModMegaKnowledge>().Where(megaKnowledge =>
                             towerModel.baseId == megaKnowledge.TowerId && megaKnowledge.TargetChanging))
                {
                    if (!BackupTowerModels.ContainsKey(towerModel.name))
                    {
                        BackupTowerModels[towerModel.name] = towerModel.Duplicate();
                    }

                    if (!megaKnowledge.Enabled)
                    {
                        Reset(towerModel);
                    }
                }
            }
        }

        private static void Reset(TowerModel towerModel)
        {
            var model = BackupTowerModels[towerModel.name].Duplicate();
            towerModel.behaviors = model.behaviors;
            towerModel.towerSelectionMenuThemeId = model.towerSelectionMenuThemeId;
            towerModel.targetTypes = model.targetTypes;
        }

        public override void OnUpdate()
        {
            if (InGame.instance.Exists()?.bridge != null && ModContent.GetInstance<Overtime>().Enabled)
            {
                var overclock = Game.instance.model.GetTower(TowerType.EngineerMonkey, 0, 4).GetAbility()
                    .GetBehavior<OverclockModel>();
                foreach (var tts in InGame.instance.bridge.GetAllTowers())
                {
                    var baseId = tts.tower.towerModel.baseId;
                    if ((baseId == TowerType.EngineerMonkey || baseId.Contains("Sentry")) &&
                        tts.tower.GetMutatorById("Overclock") == null)
                    {
                        tts.tower.AddMutator(overclock.Mutator, -1, false);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(GameModel), nameof(GameModel.CreateModded),
            typeof(Il2CppSystem.Collections.Generic.List<string>), typeof(MapModel))]
        internal class GameModel_CreateModded
        {
            [HarmonyPrefix]
            internal static void Prefix(Il2CppSystem.Collections.Generic.List<string> activeMods)
            {
                var knowledge = !Game.instance.GetPlayerProfile().knowledgeDisabled;
                foreach (var activeMod in activeMods)
                {
                    knowledge &= Game.instance.model.mods.Where(modelMod => modelMod.name == activeMod)
                        .All(modelMod =>
                            modelMod.mutatorMods.All(mod => mod.TryCast<DisableMonkeyKnowledgeModModel>() == null));
                }

                foreach (var towerModel in Game.instance.model.towers)
                {
                    foreach (var megaKnowledge in ModContent.GetContent<ModMegaKnowledge>()
                                 .Where(megaKnowledge =>
                                     megaKnowledge.Enabled &&
                                     megaKnowledge.TargetChanging &&
                                     towerModel.baseId == megaKnowledge.TowerId))
                    {
                        if (!knowledge)
                        {
                            Reset(towerModel);
                        }
                        else
                        {
                            megaKnowledge.Apply(towerModel);
                        }
                    }
                }
            }

            [HarmonyPostfix]
            internal static void Postfix(ref GameModel __result,
                Il2CppSystem.Collections.Generic.List<string> activeMods)
            {
                var knowledge = !Game.instance.GetPlayerProfile().knowledgeDisabled;
                foreach (var activeMod in activeMods)
                {
                    knowledge &= Game.instance.model.mods.Where(modelMod => modelMod.name == activeMod)
                        .All(modelMod =>
                            modelMod.mutatorMods.All(mod => mod.TryCast<DisableMonkeyKnowledgeModModel>() == null));
                }

                if (!knowledge)
                {
                    return;
                }

                foreach (var towerModel in __result.towers)
                {
                    foreach (var megaKnowledge in ModContent.GetContent<ModMegaKnowledge>()
                                 .Where(megaKnowledge =>
                                     megaKnowledge.Enabled &&
                                     !megaKnowledge.TargetChanging &&
                                     towerModel.baseId == megaKnowledge.TowerId))
                    {
                        megaKnowledge.Apply(towerModel);
                    }
                }
            }
        }
    }
}