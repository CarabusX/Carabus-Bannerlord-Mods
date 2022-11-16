using HarmonyLib;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Library;

namespace SmithStaminaRegenAnywhere
{
    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class CraftingCampaignBehaviorHarmonyPatch
    {
        [HarmonyPrefix]
        private static bool HourlyTick(
            CraftingCampaignBehavior __instance,
            ref Dictionary<Hero, object> ____heroCraftingRecords)
        {
            foreach (KeyValuePair<Hero, object> heroCraftingRecord in ____heroCraftingRecords)
            {
                Hero hero = heroCraftingRecord.Key;
                int maxHeroCraftingStamina = __instance.GetMaxHeroCraftingStamina(hero);

                Traverse craftingRecordValue = Traverse.Create(heroCraftingRecord.Value);
                Traverse<int> craftingStamina = craftingRecordValue.Field<int>("CraftingStamina");

                if (craftingStamina.Value < maxHeroCraftingStamina)
                {
                    //if (hero.PartyBelongedTo?.CurrentSettlement != null)
                    //{
                        craftingStamina.Value = MathF.Min(maxHeroCraftingStamina, craftingStamina.Value + GetStaminaHourlyRecoveryRate(__instance, hero));

                        if (craftingStamina.Value == maxHeroCraftingStamina)
                        {
                            string message = string.Format("{0} has fully regenerated {1} crafting stamina.", hero.Name, hero.IsFemale ? "her" : "his");
                            InformationManager.DisplayMessage(new InformationMessage(message));
                        }
                    //}
                }
            }

            return false;
        }

        private static int GetStaminaHourlyRecoveryRate(CraftingCampaignBehavior _this, Hero hero)
        {
            return (int)InvokePrivateMethod(_this, "GetStaminaHourlyRecoveryRate", hero);
        }

        private static object InvokePrivateMethod<T>(T obj, string methodName, params object[] parameters)
        {
            var methodInfo = AccessTools.Method(obj!.GetType(), methodName);
            return methodInfo.Invoke(obj, parameters);
        }
    }
}
