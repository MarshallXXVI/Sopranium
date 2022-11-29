using System;
using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.Source.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.UnitsGameObject;
/// <summary>
/// Implementing same ideas with Units but using abstract and inheritance.
/// </summary>

internal abstract class Enemy : IEnemyUnit
{
    protected bool mIfDead;
    protected float mHitDist;
    protected float mHp;
    protected float mMaxHp;

    private Vector2 mPosition;
    internal Vector2 mVelocity;

    protected AnimationManager mAnimationManager;


    protected SoundManager mSoundManager;

    protected Dictionary<string, Animation> mAnimations;

    protected IFriendlyUnit mTarget;

    protected SoMuchOfSpots mEnemyField;

    private List<Vector2> mPath = new List<Vector2>();

    public abstract int Id { get; }

    public Vector2 Position
    {
        get => mPosition;
        set
        {
            mPosition = value;

            if (mAnimationManager != null)
            {
                mAnimationManager.Position = mPosition;
            }
        }
    }

    public float HitDist { get => mHitDist; set => mHitDist = value; }

    public bool IsDead
    {
        get => mIfDead;
        set => mIfDead = value;
    }

    public Vector2 Velocity
    {
        get => mVelocity;
        set => mVelocity = value;
    }

    public List<Vector2> Path { get; set; }


    public abstract void Update(GameTime gameTime);

    protected void ApproachTarget(GameTime gameTime)
    {
        if (mTarget != null)
        {
            mPath = FindPath(mEnemyField, mTarget.Position);
        }
        if (mPath == null || 0 > mPath.Count - 1)
        {
            return;
        }

        if (MoveTowardsSpot(mPath[0], gameTime))
        {
            mPath.RemoveAt(0);
        }

    }

    private bool MoveTowardsSpot(Vector2 target, GameTime gameTime)
    {
        // if we hit ours next destination return true;
        if (Position == target)
        {
            return true;
        }

        // if not we get next direction and add to ours Sprite Position.
        var tempVector2 = Vector2.Normalize(target - Position);
        Velocity = tempVector2;
        Position += Velocity * 0.08f * gameTime.ElapsedGameTime.Milliseconds;

        // if ours Sprite move pass target.
        // we set ours Sprite position to that targetPosition.
        if (Math.Abs(Vector2.Dot(Velocity,
                Vector2.Normalize(target - Position)) + 1) < 0.1f)
        {
            Position = target;
        }
        // return true or false;
        return Position == target;
    }

    private List<Vector2> FindPath(SoMuchOfSpots grid, Vector2 target)
    {
        mPath.Clear();
        var tempStartSpot = grid.GetSpotsFromPixel(Position, Vector2.Zero);
        var tempTarget = grid.GetSpotsFromPixel(target, Vector2.Zero);
        var tempPath = grid.GetPathFromGrid(tempStartSpot, tempTarget);
        return tempPath;
    }

    public abstract void TakeDamage(float damage);

    public abstract void Draw(SpriteBatch spriteBatch);

    public abstract void AudioUpdate();

    public abstract void GraphicsUpdate();

}