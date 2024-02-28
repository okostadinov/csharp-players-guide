namespace FinalBattle;

public abstract class Gear
{
    public abstract string Name { get; }
    public abstract IAttack Attack { get; }
    public IAttack Equip() => Attack;
}

public class Sword : Gear
{
    public override string Name { get; } = "Sword";
    public override IAttack Attack { get; } = new Slash();
}

public class Dagger : Gear
{
    public override string Name { get; } = "Dagger";
    public override IAttack Attack { get; } = new Stab();
}

public class Bow : Gear
{
    public override string Name { get; } = "Bow";
    public override IAttack Attack { get; } = new QuickShot();
}