using System;
using System.Linq;
using System.Reflection.Metadata;
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
    [Export]
    public Timer ShaderTimerNode { get; private set; }

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
    private ShaderMaterial shader;

    public override void _Ready()
    {
        shader = SpriteNode.MaterialOverlay as ShaderMaterial;
        HurtBoxNode.AreaEntered += OnHurtBoxAreaEntered;
        SpriteNode.TextureChanged += HandleTextureChanged;
        ShaderTimerNode.Timeout += HandleShaderTimeout;
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
        if (area is not IHitbox hitBox) { return; }

        StatResource health = GetStatResource(Stat.Health);

        float damage = hitBox.GetDamage();

        health.StatValue -= damage;

        shader.SetShaderParameter("active", true);

        ShaderTimerNode.Start();
    }

    public StatResource GetStatResource(Stat health)
    {
        return stats.FirstOrDefault(stat => stat.StatType == health);
    }

    private void HandleTextureChanged()
    {
        shader.SetShaderParameter("tex", SpriteNode.Texture);
    }

    private void HandleShaderTimeout()
    {
        shader.SetShaderParameter("active", false);
    }

}
