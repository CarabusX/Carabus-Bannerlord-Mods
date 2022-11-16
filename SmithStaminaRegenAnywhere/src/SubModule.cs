using HarmonyLib;
using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SmithStaminaRegenAnywhere
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try
            {
                Debug.Print("SmithStaminaRegenAnywhere | Patching...");
                new Harmony("bannerlord.mod.SmithStaminaRegenAnywhere").PatchAll();
                Debug.Print("SmithStaminaRegenAnywhere | Patched!");
            }
            catch (Exception ex)
            {
                Debug.PrintError("SmithStaminaRegenAnywhere | Error patching:\n" + ex.Message, ex.StackTrace);
            }
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }
    }
}