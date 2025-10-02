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
	[BepInProcess("Subnautica.exe")]
	public class TimeCapsuleNoCoffee : BaseUnityPlugin
	{
		public void Awake()
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
		static List<TimeCapsuleItem> PostFix(List<TimeCapsuleItem> items)
		{
			List<TimeCapsuleItem> result = new List<TimeCapsuleItem>();
			foreach (TimeCapsuleItem item in items)
			{
				if (item.techType == TechType.Coffee)
				{
					TimeCapsuleNoCoffee.LogSource.LogInfo("Removed a coffee in timecapsule!");
                    continue;
				}
				result.Add(item);
				TimeCapsuleNoCoffee.LogSource.LogInfo("Foreached a item in timecapsule, techtype = " + item.techType.ToString() + "(" + ((int)item.techType) + ")");
			}
			return result;
		}
	}
}
