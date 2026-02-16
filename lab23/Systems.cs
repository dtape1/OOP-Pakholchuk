using System;

public class SwordSystem : IWeaponSystem
{
    public void Attack()
    {
        Console.WriteLine("Герой атакує мечем");
    }
}

public class SimpleMedicalKit : IMedicalKit
{
    public void Heal()
    {
        Console.WriteLine("Герой лікується");
    }
}

public class NpcDialogue : IDialogueManager
{
    public void Talk()
    {
        Console.WriteLine("Герой говорить з NPC");
    }
}
