using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.Source.Interfaces;
using KnightsOfLaCampus.Units;
using KnightsOfLaCampus.UnitsGameObject;
using KnightsOfLaCampus.UnitsGameObject.Friends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Source;

internal sealed class Player
{
    // bug unit sliding sometimes after start moving.
    // TODO:: test or try ignore first element Vector2D of path. It might caused buffer position.

    // Coordinate that King will be Spawn.
    private const int X = (Globals.ScreenWidth / 2) + 15;
    private const int Y = (Globals.ScreenHeight / 2) + 15;
    public readonly King mKing;
    public readonly Kaiser mKaiser;
    public readonly Sniper mSniper;
    private readonly SoMuchOfSpots mPlayerField;
    public readonly List<IFriendlyUnit> mMyFriendlyUnits = new List<IFriendlyUnit>();
    private readonly List<IFriendlyUnit> mNowControlUnit = new List<IFriendlyUnit>();
    private readonly SoundEffect mHitEffect;
    private readonly Basic2D mGoThereArrow;

    internal Player(SoMuchOfSpots grid)
    {
        mPlayerField = grid;
        mHitEffect = Globals.Content.Load<SoundEffect>("Stuffs\\Logo_hit");
        mGoThereArrow = new Basic2D("Stuffs\\goThereArrow",
            new Vector2(16, 16),
            new Vector2(32, 32));
        mKing = new King(){Position = new Vector2(X + 32, Y + 32)};


        // King is always ours friend.
        mMyFriendlyUnits.Add(mKing);

        // Archer can buy from recruitment screen. actually every unit can be bought from recruitment screen.
        // But will get only Knight that look like King or aka SwordMan.

        // these 2 classes were inherit from Friend class and Friend class has IFriendlyUnit interface.
        // less redundancy.
        mKaiser = new Kaiser() { Position = new Vector2(X + 64, Y + 32) };
        mSniper = new Sniper() { Position = new Vector2(X + 64, Y + 64) };
        mMyFriendlyUnits.Add(mKaiser); // he is King in another universe.
        mMyFriendlyUnits.Add(mSniper); // he is Archer in another dimension.

        // declare function to pass to Friends list.
        GameGlobals.mPassFriends = AddFriend;
    }

    internal void Update(GameTime gameTime, List<IEnemyUnit> enemies)
    {
        // testing enemy die. if they move too close to King, they die and drop gold.
        foreach (var mob in enemies.Where(mob => Vector2.Distance(mob.Position, mKing.Position) < 40))
        {
            GameGlobals.mPassGolds(new Gold(mob.Position, mKing));
            mob.IsDead = true;
        }
        FriendUpdate(gameTime, enemies);
    }

    // Update always friend if our friend has flag true. we push him to our stack.
    // if none of them has flag true.
    // we clear our stack. this stack hold maximum 1 element.
    // and also update all friends if they are not dead.
    // if all dead == lose.
    private void FriendUpdate(GameTime gameTime, List<IEnemyUnit> enemies)
    {
        if (mMyFriendlyUnits.Count > 0)
        {
            for (var i = 0; i < mMyFriendlyUnits.Count; i++)
            {
                mMyFriendlyUnits[i].Update(gameTime, enemies);
                if (!mMyFriendlyUnits[i].IsDead)
                {
                    continue;
                }

                mMyFriendlyUnits.RemoveAt(i);
                i--;
            }
        }

        foreach (var t in mMyFriendlyUnits)
        {
            if (t.GetIfSelected() && mNowControlUnit.Count == 0)
            {
                mNowControlUnit.Add(t);
            }
            else if(t.GetIfSelected() && mNowControlUnit.Contains(t))
            {
                GivePathToUnit(mNowControlUnit[0], mPlayerField);
            }
            else if (!t.GetIfSelected() && mNowControlUnit.Contains(t))
            {
                mNowControlUnit.RemoveAt(0);
            }
        }
    }

    // This Function always check currently Unit selected. If Player have no Unit in Control play HitEffect.
    private void GivePathToUnit(IFriendlyUnit unit, SoMuchOfSpots grid)
    {
        if (Globals.Mouse.RightClick())
        {
            //unit.Path.Clear();
            var moveTo = new Vector2(Globals.Mouse.mNewMousePos.X, Globals.Mouse.mNewMousePos.Y);
            // product from PathFinding should come after this.
            unit.Path = FindPath(unit.Position, grid.GetSpotsFromPixel(moveTo, Vector2.Zero));
        }
        else if (Globals.Mouse.RightClick() && mNowControlUnit.Count == 0)
        {
            mHitEffect.Play();
        }
    }

    //FindPath function.
    private List<Vector2> FindPath(Vector2 currentPos, Vector2 target)
    {
        var tempStartSpot = mPlayerField.GetSpotsFromPixel(currentPos, Vector2.Zero);

        var tempPath = mPlayerField.GetPathFromGrid(tempStartSpot, target);

        if (tempPath == null || tempPath.Count == 0)
        {
            System.Diagnostics.Debug.WriteLine("noPathIsCreate");
        }

        return tempPath;
    }

    // just draw an Arrow if there is some character in Control not thing fancy.
    private void DrawArrow()
    {
        var topLeft = mPlayerField.GetSpotsFromPixel(new Vector2(0, 0), Vector2.Zero);
        var botRight =
            mPlayerField.GetSpotsFromPixel(new Vector2(Globals.ScreenWidth, Globals.ScreenHeight), Vector2.Zero);

        for (var j = (int)topLeft.X; j < botRight.X && j <= mPlayerField.mGrid.Count; j++)
        {
            for (var k = (int)topLeft.Y; k < botRight.Y && k <= mPlayerField.mGrid[0].Count; k++)
            {
                if (((int)mPlayerField.mCurrentHoverSlot.X == j && (int)mPlayerField.mCurrentHoverSlot.Y == k) && !mPlayerField.mGrid[j][k].mIfFilled && mNowControlUnit.Count == 1)
                {
                    mGoThereArrow.Draw(new Vector2(j * 32, k * 32));
                }
            }
        }

    }

    /// <summary>
    /// Pass object to mMyFriendlyUnits list can be any IFriendlyUnit.
    /// </summary>
    /// <param name="info"></param>
    private void AddFriend(object info)
    {
        mMyFriendlyUnits.Add((IFriendlyUnit)info);
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        DrawArrow();
        foreach (var friend in mMyFriendlyUnits)
        {
            friend.Draw(spriteBatch);
        }
    }
}