using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using TILER2;
using UnityEngine;
using Path = System.IO.Path;

namespace FoopysExtras
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInDependency(R2API.R2API.PluginGUID, R2API.R2API.PluginVersion)]
    [BepInDependency(TILER2Plugin.ModGuid, TILER2Plugin.ModVer)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI))]
    public class FoopysExtrasPlugin : BaseUnityPlugin
    {
        private MiscUtil.FilingDictionary<CatalogBoilerplate> itemList;

        public const string ModVer = "0.0.1";
        public const string ModName = "Foopy's Extras";
        public const string ModGuid = "com.Foopy.FoopysExtras";

        private void Awake()
        {
            itemList = T2Module.InitAll<CatalogBoilerplate>(new T2Module.ModInfo
            {
                displayName = "Foopy's Extras",
                longIdentifier = "FOOPYSEXTRAS",
                shortIdentifier = "FPYXTRA",
                mainConfigFile = new ConfigFile(Path.Combine(Paths.ConfigPath, ModGuid + ".cfg"), true)
            });

            T2Module.SetupAll_PluginAwake(itemList);
        }

        private void Start()
        {
            T2Module.SetupAll_PluginStart(itemList);
            CatalogBoilerplate.ConsoleDump(Logger, itemList);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                foreach(var item in itemList)
                {
                    PickupDropletController.CreatePickupDroplet(item.pickupIndex, transform.position, transform.forward * 20f);
                }
            }
        }
    }
}
