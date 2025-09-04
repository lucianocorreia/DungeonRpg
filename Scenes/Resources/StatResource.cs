using System;
using Godot;

[GlobalClass]
public partial class StatResource : Resource
{
    public event Action OnZero;
    public event Action OnUpdate;

    private float statValue;

    [Export] public Stat StatType { get; private set; }

    [Export]
    public float StatValue
    {
        get => statValue;
        set
        {
            statValue = Mathf.Clamp(value, 0, Mathf.Inf);

            OnUpdate?.Invoke();

            if (statValue == 0)
            {
                OnZero?.Invoke();
            }
        }
    }
}
