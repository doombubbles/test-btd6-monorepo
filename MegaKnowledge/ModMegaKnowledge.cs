using System.Collections.Generic;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using MelonLoader;

namespace MegaKnowledge
{
    public abstract class ModMegaKnowledge : NamedModContent
    {
        private static MelonPreferences_Category category;
        private MelonPreferences_Entry<bool> setting;

        public bool Enabled
        {
            get => setting?.Value == true;
            set => setting.Value = value;
        }
        
        public abstract string TowerId { get; }

        public abstract int Offset { get; }

        public virtual bool TargetChanging => false;

        public string towerSet;


        public override void Register()
        {
            if (category == null)
            {
                category = MelonPreferences.CreateCategory("MegaKnowledges");
            }

            towerSet = Game.instance.model.GetTowerWithName(TowerId).towerSet;

            setting = category.CreateEntry(Name, false, DisplayName, Description);
        }

        public abstract void Apply(TowerModel model);

        public string GetCloneFrom()
        {
            switch (towerSet)
            {
                case "Primary":
                    return "MoreCash";
                case "Military":
                    return "BigBloonSabotage";
                case "Magic":
                    return "ManaShield";
                case "Support":
                    return "BankDeposits";
                default:
                    return "";
            }
        }
    }
}