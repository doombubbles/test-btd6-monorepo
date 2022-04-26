using Assets.Scripts.Models.Towers;

namespace MegaKnowledge.MegaKnowledges.Support
{
    public class Overtime : ModMegaKnowledge
    {
        public override string TowerId => TowerType.EngineerMonkey;
        public override string Description => "Engineers and their Sentries are permanently Overclocked.";
        public override int Offset => 1200;
        
        
        public override void Apply(TowerModel model)
        {
        }
    }
}