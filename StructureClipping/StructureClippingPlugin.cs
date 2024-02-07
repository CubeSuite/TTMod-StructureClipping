using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using StructureClipping.Patches;
using System;
using System.Reflection;
using UnityEngine;

namespace StructureClipping
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class StructureClippingPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.equinox.StructureClipping";
        private const string PluginName = "StructureClipping";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        private void Awake() {
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");
            Harmony.PatchAll();

            Harmony.CreateAndPatchAll(typeof(StructureBuilderPatch));
            Harmony.CreateAndPatchAll(typeof(GridManagerPatch));

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loaded.");
            Log = Logger;
        }
    }
}
