using RoR2;
using TILER2;
using UnityEngine;

namespace FoopysExtras.Equipments
{
    class PocketBossShrine : Equipment_V2<PocketBossShrine>
    {
        public override string displayName => "Pocket Mountain Shrine";
        protected override string GetDescString(string langID = null) => "Activates the <style=cIsDamage>Shrine of the Mountain</style>";
        protected override string GetLoreString(string langID = null) => "Guess I'll just yoink this huge shrine in my pocket.";
        protected override string GetNameString(string langID = null) => displayName;
        protected override string GetPickupString(string langID = null) => "Activates the <style=cIsDamage>Shrine of the Mountain</style>";
        public override float cooldown { get; protected set; } = 20;

        protected override bool PerformEquipmentAction(EquipmentSlot slot)
        {
            if (TeleporterInteraction.instance)
            {
                TeleporterInteraction.instance.AddShrineStack();
            }
            Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
            {
                subjectAsCharacterBody = slot.characterBody,
                baseToken = "SHRINE_BOSS_USE_MESSAGE"
            });
            EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
            {
                origin = slot.transform.position,
                rotation = Quaternion.identity,
                scale = 1f,
                color = new Color(0.7372549f, 0.90588236f, 0.94509804f)
            }, true);

            return true;
        }
    }
}
