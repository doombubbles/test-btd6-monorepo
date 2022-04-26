﻿using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Behaviors.Emissions.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using BTD_Mod_Helper.Extensions;

namespace MegaKnowledge.MegaKnowledges.Magic
{
    public class ShadowDouble : ModMegaKnowledge
    {
        public override string TowerId => TowerType.NinjaMonkey;
        public override string Description => "Ninja Monkeys can throw extra Shurikens behind them if Bloons are present.";
        public override int Offset => -1200;

        public override void Apply(TowerModel model)
        {
            var attackModel = model.GetAttackModel();
            var weapon = attackModel.weapons[0];
            var newWeapon = weapon.Duplicate();
            newWeapon.projectile.display = GetDisplayGUID<ShadowShuriken>();
            weapon.AddBehavior(new FireAlternateWeaponModel("FireAlternateWeaponModel_", 1));

            newWeapon.AddBehavior(new FireWhenAlternateWeaponIsReadyModel("FireWhenAlternateWeaponIsReadyModel_", 1));
            newWeapon.AddBehavior(new FilterTargetAngleFilterModel("FilterTargetAngleFilterModel_", 45.0f, 180f, true,
                56));

            var arcEmissionModel = newWeapon.emission.TryCast<ArcEmissionModel>();
            if (arcEmissionModel != null)
            {
                newWeapon.emission.AddBehavior(
                    new EmissionArcRotationOffTowerDirectionModel("EmissionArcRotationOffTowerDirectionModel_", 180));
            }
            else
            {
                newWeapon.emission.AddBehavior(
                    new EmissionRotationOffTowerDirectionModel("EmissionRotationOffTowerDirectionModel_", 180));
            }

            newWeapon.name += " Secondary";
            newWeapon.ejectX *= -1;

            var trackTargetWithinTimeModel = newWeapon.projectile.GetBehavior<TrackTargetWithinTimeModel>();
            if (trackTargetWithinTimeModel != null)
            {
                trackTargetWithinTimeModel.name += "Behind";
            }


            attackModel.AddWeapon(newWeapon);
        }
    }
}