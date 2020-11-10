using RoR2;
using TILER2;

namespace FoopysExtras.Equipments
{
    class EngineerDisguise : Equipment_V2<EngineerDisguise>
    {
        private CharacterMaster _turret;

        public override string displayName => "Engineer Disguise";
        protected override string GetDescString(string langID = null) => "Spawns a <style=cIsDamage>Mobile Turret</style> that inherits your items.";
        protected override string GetLoreString(string langID = null) => "Disguise yourself as the <style=cIsDamage>Engineer</style>, what benefits would that grant you?";
        protected override string GetNameString(string langID = null) => displayName;
        protected override string GetPickupString(string langID = null) => "Summons a <style=cIsDamage>Mobile Turret</style>.";
        public override float cooldown { get; protected set; } = 60;

        public override void Install()
        {
            base.Install();
            _turret = new CharacterMaster(); 
            On.RoR2.Inventory.GiveItem += GiveTurretItem;
            On.RoR2.Inventory.RemoveItem += RemoveTurretItem;
        }

        public override void Uninstall()
        {
            base.Uninstall();
            if (_turret)
            {
                _turret.TrueKill();
            }
            On.RoR2.Inventory.GiveItem -= GiveTurretItem;
            On.RoR2.Inventory.RemoveItem -= RemoveTurretItem;
        }

        private void GiveTurretItem(On.RoR2.Inventory.orig_GiveItem orig, Inventory self, ItemIndex itemIndex, int count)
        {
            orig(self, itemIndex, count);
            if (count < 1) return;
            if (_turret && !ItemCatalog.GetItemDef(itemIndex).ContainsTag(ItemTag.AIBlacklist))
            {
                _turret.inventory.GiveItem(itemIndex, count);
            }
        }

        private void RemoveTurretItem(On.RoR2.Inventory.orig_RemoveItem orig, Inventory self, ItemIndex itemIndex, int count)
        {
            orig(self, itemIndex, count);
            if (count < 1) return;
            if (_turret)
            {
                _turret.inventory.RemoveItem(itemIndex, count);
            }
        }

        protected override bool PerformEquipmentAction(EquipmentSlot slot)
        {
            if (_turret)
            {
                _turret.TrueKill();
            }

            _turret = new MasterSummon
            {
                masterPrefab = MasterCatalog.FindMasterPrefab("EngiWalkerTurretMaster"),
                position = slot.transform.position,
                rotation = slot.transform.rotation,
                summonerBodyObject = slot.characterBody.gameObject,
                ignoreTeamMemberLimit = true,
                teamIndexOverride = TeamIndex.Player
            }.Perform();

            _turret.inventory.CopyItemsFrom(slot.characterBody.inventory);
            for (int i = 0; i < ItemCatalog.itemCount; i++)
            {
                ItemIndex itemIndex = (ItemIndex)i;
                if (ItemCatalog.GetItemDef(itemIndex).ContainsTag(ItemTag.AIBlacklist))
                {
                    _turret.inventory.ResetItem(itemIndex);
                }
            }

            return true;
        }
    }
}
