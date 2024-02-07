using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StructureClipping.Patches
{
    internal class StructureBuilderPatch
    {
        [HarmonyPatch(typeof(StructureBuilder), "CheckBuildIsValid")]
        [HarmonyPostfix]
        static void alwaysTrue(ref bool __result) {
            Debug.Log("Overwrote result of CheckBuildIsValid");
            __result = true;
        }

        [HarmonyPatch(typeof(StructureBuilder), "CheckBuildHasLEqualVoxelOverlapAndNoAABBGridOverlap")]
        [HarmonyPostfix]
        static void alwaysTrue2(ref bool  __result) {
            Debug.Log("Overwrote result of CheckBuildIsValid");
            __result = true; 
        }
    }
}
