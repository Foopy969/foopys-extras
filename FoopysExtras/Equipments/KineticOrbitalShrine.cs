using RoR2;
using System;
using TILER2;

namespace FoopysExtras.Equipments
{
    class KineticOrbitalShrine : Equipment_V2<KineticOrbitalShrine>
    {
        public override string displayName => "Kinetic Orbital Shrine";
        protected override string GetDescString(string langID = null) => "Calls a shrine strike from God.";
        protected override string GetLoreString(string langID = null) => "The new coolest way to go commit dye.";
        protected override string GetNameString(string langID = null) => displayName;
        protected override string GetPickupString(string langID = null) => "Summons a <style=cIsDamage>Useless Shrine</style>.";
        public override float cooldown { get; protected set; } = 45;

        protected override bool PerformEquipmentAction(EquipmentSlot slot)
        {
            throw new NotImplementedException();
        }

    }
}
