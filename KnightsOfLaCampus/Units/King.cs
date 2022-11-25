using System;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.Source.Interfaces;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Units;

internal class King : IFriendlyUnit
{
    public int mGold;
    private Vector2 mPosition;
    private Vector2 mVelocity;

    private const int KingXOffset = 16;

    private const int KingYOffset = 50;

    private readonly AnimationManager mAnimationManager;

    //use to test the actual position with Animation.
    //private readonly Texture2D mTexture;

    private readonly SoundManager mSoundManager;

    private readonly SaveManager mSaveManager;

    private readonly Dictionary<string, Animation> mAnimations;


    internal King()
    {
        IsDead = false;
        var animations = new Dictionary<string, Animation>()
        {
            { "up", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkTop"), 4) },
            { "down", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkDown"), 4) },
            { "left", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkLeft"), 8) },
            { "right", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkRight"), 8) }
        };
        mAnimations = animations;
        mAnimationManager = new AnimationManager(mAnimations.First().Value);
        IsSelected = false;
        mSaveManager = new SaveManager();
        mSoundManager = new SoundManager();
        mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");
    }

    public bool GetIfSelected()
    {
        return IsSelected;
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

    public void Update(GameTime gameTime)
    {
        CheckIfSelected();
        Move(gameTime);
        GraphicsUpdate();
        AudioUpdate();
        mAnimationManager.Update(gameTime);
        mVelocity = Vector2.Zero;

        if (SavedVariables.LoadSavedVariables)
        {
            mSaveManager.LoadFromXml();
            Position = SavedVariables.KingPositon;
            SavedVariables.LoadSavedVariables = false;
        }

        SavedVariables.KingPositon = Position;
        mSaveManager.SaveToXml();
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
        var mouseKingDist = Vector2.Distance(Position, Globals.Mouse.mNewMousePos);
        IsSelected = mouseKingDist switch
        {
            // we set our flag to true if unit was never been selected.
            < 17 when !IsSelected => true,
            // else mouse has been click but not on Unit we set our flag false.
            > 17 => false,
            _ => IsSelected
        };

    }

    /// <summary>
    /// we move our Sprite to next position in ours mPath which provides by Path finding.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="gameTime"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// this Draw what suppose to draw on the screen.
    /// </summary>
    /// <param name="spriteBatch"></param>
    public void Draw(SpriteBatch spriteBatch)
    {
        mAnimationManager?.Draw(spriteBatch, new Vector2(KingXOffset, KingYOffset));
    }

    /// <summary>
    /// update Audio if moving play Walk.
    /// </summary>
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

    /// <summary>
    /// we update what should be drawn from Animations Set.
    /// </summary>
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

    /// <summary>
    /// return Position from this Object.
    /// </summary>
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
    /// <summary>
    /// return Velocity from this Object.
    /// </summary>
    public Vector2 Velocity
    {
        get => mVelocity;
        set => mVelocity = value;
    }

    /// <summary>
    /// return List of Path from this Object.
    /// </summary>
    public List<Vector2> Path { get; set; } = new List<Vector2>();

    public bool IsSelected { get; set; }

    public bool IsDead { get; set; }
}

// Will be implement later.

//public override void SpawnPoint(Vector2 position)
//{
//    // Currently empty
//}

//public override void TakeDamage(int damage)
//{
//    mHpBar -= 1;
//}

//public static void AttackOther(Vector2 position)
//{
//    // Currently empty
//}

//public static void CollectGold()
//{
//    // Currently empty
//}

//public static void RepairWall()
//{
//    // can call value of Gold here directly no use of Parameter.
//    // Currently empty
//}
