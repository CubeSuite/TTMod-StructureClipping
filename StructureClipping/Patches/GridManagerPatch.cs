using HarmonyLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StructureClipping.Patches
{
    internal class GridManagerPatch
    {
        [HarmonyPatch(typeof(GridManager), nameof(GridManager.CheckBuildableAt), new Type[] {
            typeof(Vector3Int),
            typeof(Vector3Int),
            typeof(int),
            typeof(CheckOptions)
        }, new ArgumentType[] {
            ArgumentType.Ref,
            ArgumentType.Ref,
            ArgumentType.Normal,
            ArgumentType.Normal
        })]
        [HarmonyPostfix]
        static void CheckBuildableAtPostfix(ref bool __result, in Vector3Int minInt, in Vector3Int maxInt, int gridClearance, CheckOptions options) {
            bool allStructures = true;

            for (int x = minInt.x; x <= maxInt.x; x++) {
                for (int y = minInt.y; y <= maxInt.y; y++) {
                    for (int z = minInt.z; z <= maxInt.z; z++) {
                        int chunkIndex;
                        ushort coordIndex;
                        bool gotChunk = GridManager.instance.GetChunkIndexAndCoordIndexFromCoord(x, y, z, out chunkIndex, out coordIndex);
                        if (gotChunk) {
                            ref GridManager.ChunkGridData chunkData = ref GridManager.instance.chunkDatas[chunkIndex];
                            GenericMachineInstanceRef machine;
                            if (chunkData.coordToObj.TryGetValue(coordIndex, out machine)) {
                                if (machine.typeIndex != MachineTypeEnum.Structure) allStructures = false;
                            }
                        }
                    }
                }
            }

            
            if (allStructures) {
                __result = true;
            }
        }
    }
}
