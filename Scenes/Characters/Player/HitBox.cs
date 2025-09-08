using Godot;
using System;

public partial class HitBox : Area3D, IHitbox
{
    public bool CanStun()
    {
        return false;
    }

    public float GetDamage()
    {
        return GetOwner<Character>().GetStatResource(Stat.Strength).StatValue;
    }
}
