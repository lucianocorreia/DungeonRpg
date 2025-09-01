using System;
using Godot;

[GlobalClass]
public partial class StatResource : Resource
{
    public Action OnZero;

    private float statValue;

    [Export] public Stat StatType { get; private set; }

    [Export]
    public float StatValue
    {
        get => statValue;
        set
        {
            statValue = Mathf.Clamp(value, 0, Mathf.Inf);

            if (statValue == 0)
            {
                OnZero?.Invoke();
            }
        }
    }
}
