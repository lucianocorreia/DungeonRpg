using System;
using System.Linq;
using Godot;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export]
    public AnimationPlayer AnimationPlayerNode { get; private set; }
    [Export]
    public Sprite3D SpriteNode { get; private set; }
    [Export]
    public StateMachine StateMachineNode { get; private set; }
    [Export]
    public Area3D HurtBoxNode { get; private set; }
    [Export]
    public Area3D HitBoxNode { get; private set; }
    [Export]
    public CollisionShape3D HitboxShapeNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export]
    public Path3D PathNode { get; private set; }
    [Export]
    public NavigationAgent3D NavigationAgentNode { get; private set; }
    [Export]
    public Area3D ChaseAreaNode { get; private set; }
    [Export]
    public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();

    public override void _Ready()
    {
        HurtBoxNode.AreaEntered += OnHurtBoxAreaEntered;
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally) { return; }

        bool isMovingLeft = Velocity.X < 0;
        SpriteNode.FlipH = isMovingLeft;
    }

    public void ToggleHitBox(bool flag)
    {
        HitboxShapeNode.Disabled = flag;
    }

    private void OnHurtBoxAreaEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);

        Character player = area.GetOwner<Character>();

        health.StatValue -= player.GetStatResource(Stat.Strength).StatValue;

        GD.Print($"Health before damage: {health.StatValue}, Player Health: {player.Name}");
    }

    private StatResource GetStatResource(Stat health)
    {
        return stats.FirstOrDefault(stat => stat.StatType == health);
    }

}
