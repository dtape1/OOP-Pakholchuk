public class HeroAction
{
    private readonly IWeaponSystem weapon;
    private readonly IMedicalKit medical;
    private readonly IDialogueManager dialogue;

    //Dependency Injection
    public HeroAction(
        IWeaponSystem weapon,
        IMedicalKit medical,
        IDialogueManager dialogue)
    {
        this.weapon = weapon;
        this.medical = medical;
        this.dialogue = dialogue;
    }

    public void AttackEnemy() => weapon.Attack();
    public void HealSelf() => medical.Heal();
    public void TalkToNpc() => dialogue.Talk();
}
