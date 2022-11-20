using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Source.Astar;
using System;
using System.Numerics;
using MonoGame.Extended.Timers;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Source;

internal class Enemy : IEnemyUnit
{
    private Vector2 mPosition;
    internal Vector2 mVelocity;

    private readonly AnimationManager mAnimationManager;


    private readonly SoundManager mSoundManager;

    private readonly SaveManager mSaveManager;

    private readonly Dictionary<string, Animation> mAnimations;

    private readonly IFriendlyUnit mTarget;

    private readonly SoMuchOfSpots mEnemyField;

    private List<Vector2> mPath = new List<Vector2>();

    internal Enemy(Player target, SoMuchOfSpots field)
    {
        var animations = new Dictionary<string, Animation>()
        {
            { "up", new Animation(Globals.Content.Load<Texture2D>("UNIT1/up"), 3) },
            { "down", new Animation(Globals.Content.Load<Texture2D>("UNIT1/down"), 3) },
            { "left", new Animation(Globals.Content.Load<Texture2D>("UNIT1/left"), 3) },
            { "right", new Animation(Globals.Content.Load<Texture2D>("UNIT1/right"), 3) }
        };
        mAnimations = animations;
        mAnimationManager = new AnimationManager(animations.First().Value);
        mSaveManager = new SaveManager();
        mSoundManager = new SoundManager();
        mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");
        mTarget = target.mKing;
        mEnemyField = field;
    }

    public void Update(GameTime gameTime)
    {
        AiMovement(gameTime);
        GraphicsUpdate();
        Velocity = Vector2.Zero;
        AudioUpdate();
        mAnimationManager.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        mAnimationManager?.Draw(spriteBatch, new Vector2(9, 24));
    }


    public void AudioUpdate()
    {
        if (mVelocity != Vector2.Zero)
        {
            mSoundManager.PlaySound("Walk");
        }
        else
        {
            mSoundManager.StopSound("Walk");
        }
    }

    public void GraphicsUpdate()
    {
        if (Velocity.X > 0 && Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
        {
            mAnimationManager.Play(mAnimations["right"]);
        }

        if (Velocity.X < 0 && Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
        {
            mAnimationManager.Play(mAnimations["left"]);
        }

        if (Velocity.Y > 0 && Math.Abs(Velocity.Y) > Math.Abs(Velocity.X))
        {
            mAnimationManager.Play(mAnimations["down"]);
        }

        if (Velocity.Y < 0 && Math.Abs(Velocity.Y) > Math.Abs(Velocity.X))
        {
            mAnimationManager.Play(mAnimations["up"]);
        }
        if (Velocity.Y == 0 && Velocity.X == 0)
        {
            mAnimationManager.Stop();
        }

    }

    private void AiMovement(GameTime gameTime)
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

    public Vector2 Velocity
    {
        get => mVelocity;
        set => mVelocity = value;
    }

    public List<Vector2> Path { get; set; }
}