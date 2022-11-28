using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Interfaces;
using KnightsOfLaCampus.UnitsGameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightsOfLaCampus.Units;

internal sealed class Knight : IFriendlyUnit
{
    private Vector2 mPosition;
    private Vector2 mVelocity;

    private readonly AnimationManager mAnimationManager;


    private readonly SoundManager mSoundManager;

    private readonly SaveManager mSaveManager;

    private readonly Dictionary<string, Animation> mAnimations;

    internal Knight()
    {
        IsDead = false;
        // load animation set from source folder in this case Content\\King\\...
        var animations = new Dictionary<string, Animation>()
        {
            { "up", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkTop"), 4) },
            { "down", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkDown"), 4) },
            { "left", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkLeft"), 8) },
            { "right", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkRight"), 8) }
        };
        // allocate animations in to this object.
        mAnimations = animations;
        mAnimationManager = new AnimationManager(animations.First().Value);

        // init by false.
        IsSelected = false;
        mSaveManager = new SaveManager();
        mSoundManager = new SoundManager();
        // and sound effect to this object.
        mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");
    }

    public void Update(GameTime gameTime, List<IEnemyUnit> enemies)
    {
        CheckIfSelected();
        Move(gameTime);
        GraphicsUpdate();
        AudioUpdate();
        mAnimationManager.Update(gameTime);
        mVelocity = Vector2.Zero;
    }


    // this function always check if this Unit is selected or not.
    // mIfSelected is ours flag,
    private void CheckIfSelected()
    {
        // we check if Left Mouse is Clicked.
        if (!Globals.Mouse.LeftClick())
        {
            return;
        }

        // mouseKingDist is distance between mouse last pick position and Unit position.
        var mouseKingDist = Vector2.Distance(this.Position, Globals.Mouse.mNewMousePos);
        IsSelected = mouseKingDist switch
        {
            // we set our flag to true if unit was never been selected.
            < 17 when !IsSelected => true,
            // else mouse has been click but not on Unit we set our flag false.
            > 17 => false,
            _ => IsSelected
        };

    }

    private void Move(GameTime gameTime)
    {
        if (Path == null || 0 > Path.Count - 1)
        {
            return;
        }

        if (MoveTowardsSpot(Path[0], gameTime))
        {
            Path.RemoveAt(0);
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
        Position += Velocity * 0.1f * gameTime.ElapsedGameTime.Milliseconds;

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

    public void Draw(SpriteBatch spriteBatch)
    {
        mAnimationManager?.Draw(spriteBatch, new Vector2(16, 50));
    }


    public bool GetIfSelected()
    {
        return IsSelected;
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
        switch (mVelocity.X)
        {
            case > 0:
                mAnimationManager.Play(mAnimations["right"]);
                break;
            case < 0:
                mAnimationManager.Play(mAnimations["left"]);
                break;
            default:
                {
                    switch (mVelocity.Y)
                    {
                        case > 0:
                            mAnimationManager.Play(mAnimations["down"]);
                            break;
                        case < 0:
                            mAnimationManager.Play(mAnimations["up"]);
                            break;
                        default:
                            mAnimationManager.Stop();
                            break;
                    }
                    break;
                }
        }
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

    public List<Vector2> Path { get; set; } = new List<Vector2>();

    public bool IsSelected { get; set; }

    public bool IsDead { get; set; }
}