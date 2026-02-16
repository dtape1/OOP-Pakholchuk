using System;

class Program
{
    static void Main()
    {
        // Конфігурація залежностей
        IWeaponSystem weapon = new SwordSystem();
        IMedicalKit medical = new SimpleMedicalKit();
        IDialogueManager dialogue = new NpcDialogue();

        HeroAction hero = new HeroAction(weapon, medical, dialogue);

        hero.AttackEnemy();
        hero.HealSelf();
        hero.TalkToNpc();
    }
}
