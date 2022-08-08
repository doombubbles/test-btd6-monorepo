using MelonLoader;
using BTD_Mod_Helper;
using ExampleMonoRepoMod1;

[assembly: MelonInfo(typeof(ExampleMonoRepoMod1.ExampleMonoRepoMod1), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ExampleMonoRepoMod1;

public class ExampleMonoRepoMod1 : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<ExampleMonoRepoMod1>("ExampleMonoRepoMod1 loaded!");
    }
}