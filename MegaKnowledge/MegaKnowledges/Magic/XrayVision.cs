using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Projectiles;
using BTD_Mod_Helper.Extensions;

namespace MegaKnowledge.MegaKnowledges.Magic
{
    public class XrayVision : ModMegaKnowledge
    {
        public override string TowerId => TowerType.SuperMonkey;
        public override string Description => "Super Monkeys can see through walls and have increased pierce.";
        public override int Offset => -1600;

        public override void Apply(TowerModel model)
        {
            model.ignoreBlockers = true;
            foreach (var attackModel in model.GetAttackModels())
            {
                attackModel.attackThroughWalls = true;
            }

            foreach (var projectileModel in model.GetDescendants<ProjectileModel>().ToList())
            {
                projectileModel.ignoreBlockers = true;
                if (projectileModel.pierce > 0)
                {
                    projectileModel.pierce += model.tier + 1;
                }
            }
        }
    }
}