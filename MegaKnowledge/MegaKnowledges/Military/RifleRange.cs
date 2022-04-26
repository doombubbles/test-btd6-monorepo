﻿using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using BTD_Mod_Helper.Extensions;

namespace MegaKnowledge.MegaKnowledges.Military
{
    public class RifleRange : ModMegaKnowledge
    {
        public override string TowerId => TowerType.SniperMonkey;
        public override string Description => "Sniper Monkey shots can critically strike for double damage.";
        public override int Offset => -1200;

        public override void Apply(TowerModel model)
        {
            var damage = model.GetWeapon().projectile.GetDamageModel().damage;
            model.GetWeapon().AddBehavior(new CritMultiplierModel("CritMultiplierModel_", damage * 2, 1, 6,
                "252e82e70578330429a758339e10fd25", true));

            model.GetWeapon().projectile.AddBehavior(new ShowTextOnHitModel("ShowTextOnHitModel_",
                "3dcdbc19136c60846ab944ada06695c0", 0.5f, false, ""));
        }
    }
}