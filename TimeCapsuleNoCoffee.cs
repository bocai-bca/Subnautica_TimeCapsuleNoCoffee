using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace TimeCapsuleNoCoffee
{
	[BepInPlugin("net.bcasoft.timecapsulenocoffee", "TimeCapsuleNoCoffee", "1.0.0")]
	public class TimeCapsuleNoCoffee : BaseUnityPlugin
	{
		private void Awake()
		{
			Harmony harmony = new Harmony("net.bcasoft.timecapsulenocoffee");
			harmony.PatchAll();
			Logger.LogInfo("Loaded.");
			LogSource = Logger;
		}

		public static ManualLogSource LogSource;
	}

	[HarmonyPatch(typeof(TimeCapsuleContentProvider), nameof(TimeCapsuleContentProvider.GetItems))]
	public class Patch_TimeCapsuleContentProvider
	{
		static List<TimeCapsuleItem> Postfix(List<TimeCapsuleItem> __result)
		{
			List<TimeCapsuleItem> result = new List<TimeCapsuleItem>();
			foreach (TimeCapsuleItem item in __result)
			{
				if (item.techType == TechType.Coffee)
				{
					TimeCapsuleNoCoffee.LogSource.LogInfo("Removed a coffee in timecapsule!");
					continue;
				}
				result.Add(item);
				TimeCapsuleNoCoffee.LogSource.LogInfo("Foreached a item in timecapsule, techtype = " + item.techType.ToString() + "(" + ((int)item.techType) + ").");
			}
			return result;
		}
	}
}
