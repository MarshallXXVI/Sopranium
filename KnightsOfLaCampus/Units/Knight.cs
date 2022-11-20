using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfLaCampus.Units;

internal sealed class Knight : IFriendlyUnit
{
    private Vector2 mPosition;
    internal Vector2 mVelocity;

    private readonly AnimationManager mAnimationManager;


    private readonly SoundManager mSoundManager;

    private readonly SaveManager mSaveManager;

    private readonly Dictionary<string, Animation> mAnimations;

    private bool mIfSelected;

    internal Knight()
    {
        var animations = new Dictionary<string, Animation>()
        {
            { "up", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkTop"), 4) },
            { "down", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkDown"), 4) },
            { "left", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkLeft"), 8) },
            { "right", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkRight"), 8) }
        };
        mAnimations = animations;
        mAnimationManager = new AnimationManager(animations.First().Value);
        mIfSelected = false;
        mSaveManager = new SaveManager();
        mSoundManager = new SoundManager();
        mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");
    }

    public void Update(GameTime gameTime)
    {
        CheckIfSelected();
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
        mIfSelected = mouseKingDist switch
        {
            // we set our flag to true if unit was never been selected.
            < 17 when !mIfSelected => true,
            // else mouse has been click but not on Unit we set our flag false.
            > 17 => false,
            _ => mIfSelected
        };

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        mAnimationManager?.Draw(spriteBatch, new Vector2(16, 50));
    }


    public bool GetIfSelected()
    {
        return mIfSelected;
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

    public List<Vector2> Path { get; set; }
}