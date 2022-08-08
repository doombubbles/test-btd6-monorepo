using MelonLoader;
using BTD_Mod_Helper;
using ExampleMonoRepoMod2;

[assembly: MelonInfo(typeof(ExampleMonoRepoMod2.ExampleMonoRepoMod2), "ExampleMonoRepoMod2", "1.0.0", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ExampleMonoRepoMod2;

public class ExampleMonoRepoMod2 : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<ExampleMonoRepoMod2>("ExampleMonoRepoMod2 loaded!");
    }
}