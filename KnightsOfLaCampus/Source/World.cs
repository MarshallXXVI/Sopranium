using System;
using System.Collections.Generic;
using System.Diagnostics;
using KnightsOfLaCampus.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.Source.Interfaces;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework.Audio;
using TiledSharp;
using System.Net.NetworkInformation;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.UnitsGameObject;
using KnightsOfLaCampus.UnitsGameObject.Enemies;

namespace KnightsOfLaCampus.Source;

internal sealed class World
{
    private bool mIfDay = true;
    private readonly Vector2 mOffSet;
    private readonly SoMuchOfSpots mPlayerField;
    private readonly List<Gold> mGolds = new List<Gold>();
    private readonly List<Arrow> mArrows = new List<Arrow>();
    private readonly SpawnPoint mSpawnPoint;
    private readonly TileMapManager mMapManager;
    public readonly Player mPlayer;
    public readonly List<IEnemyUnit> mMobs = new List<IEnemyUnit>();


    internal World()
    {
        var soundManager = new SoundManager();
        mOffSet = new Vector2(0, 0);
        mPlayerField = new SoMuchOfSpots(new Vector2(32, 32),
            new Vector2(0, 0),
            new Vector2(Globals.ScreenWidth, Globals.ScreenHeight));

        // Import Map
        var mapLevel1 = new TmxMap("Content\\Maps\\Level1.tmx");
        mMapManager = new TileMapManager(mapLevel1);

        // Import Player call Player.cs
        mPlayer = new Player(mPlayerField);

        mPlayerField.SetMap(mapLevel1);

        // Test set MusicBackground
        soundManager.ChangeMusic(0);

        // declare a function in GameGlobals to pass a object in to this world.
        GameGlobals.mPassMobs = AddEnemy;
        GameGlobals.mPassGolds = AddGold;
        GameGlobals.mPassArrow = AddArrow;

        // add Spawn point top mid of screen.
        mSpawnPoint = new SpawnPoint(mPlayer, mPlayerField, new Vector2(1000, 0));

        if (Globals.mLoadGame)
        {
            LoadSaveGame();
        }

    }
    internal void Update(GameTime gameTime)
    {
        //Trace.WriteLine(GameGlobals.mLevel);
        // testing dimming the screen shifting from day to night.
        if (GameGlobals.mTimer.mTimeLeft < 1f)
        {
            switch (mIfDay)
            {
                case true:
                    mIfDay = false;
                    GameGlobals.mTimer.Reset();
                    GameGlobals.mLevel++;
                    break;
                case false:
                    mIfDay = true;
                    GameGlobals.mTimer.Reset();
                    break;
            }
        }

        Globals.mBrightness = mIfDay ? (byte)100 : (byte)15;

        mPlayer.Update(gameTime, mMobs);
        for (var i = 0; i < mMobs.Count; i++)
        {
            mMobs[i].Update(gameTime);
            if (!mMobs[i].IsDead)
            {
                continue;
            }

            mMobs.RemoveAt(i);
            i--;
        }

        for (var i = 0; i < mGolds.Count; i++)
        {
            mGolds[i].Update();

            if (!mGolds[i].mIfDead)
            {
                continue;
            }

            mGolds.RemoveAt(i);
            i--;
        }

        for (var i = 0; i < mArrows.Count; i++)
        {
            mArrows[i].Update(mMobs,gameTime);

            if (!mArrows[i].mIfDead)
            {
                continue;
            }

            mArrows.RemoveAt(i);
            i--;
        }
        
        mSpawnPoint.Update(gameTime);
        mPlayerField.Update(mOffSet);
    }

    // throw a info in and cast a Enemy class and put it in to Mobs list.
    private void AddEnemy(object info)
    {
        mMobs.Add((IEnemyUnit)info);
    }
    // throw a info in and cast a Gold class and put it in to Golds list.
    private void AddGold(object info)
    {
        mGolds.Add((Gold)info);
    }
    // throw a info in and cast a Arrow class and put it in to Arrows list.
    private void AddArrow(object info)
    {
        mArrows.Add((Arrow)info);
    }

    private void LoadSaveGame()
    {
        foreach (var thing in JsonManager.LoadGameObjects()!)
        {
            if (thing.Id == 0)
            {
                mPlayer.mKing.Position = new Vector2(thing.PositionX, thing.PositionY);
            }

            if (thing.Id == 5)
            {
                mMobs.Add(new BadGuy(mPlayer, mPlayerField){Position = new Vector2(thing.PositionX, thing.PositionY)});
            }
        }

        GameGlobals.mGold = JsonManager.LoadGameValue()!.Gold;
        GameGlobals.mTimer.mTimeLeft = JsonManager.LoadGameValue()!.TimeLeft;
        GameGlobals.mLevel = JsonManager.LoadGameValue()!.Level;
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        mMapManager.Draw();
        mPlayerField.Draw(Vector2.Zero);

        foreach (var spawn in mGolds)
        {
            spawn.Draw(spriteBatch);
        }
        foreach (var enemy in mMobs)
        {
            enemy.Draw(spriteBatch);
        }

        foreach (var arrow in mArrows)
        {
            arrow.Draw(spriteBatch);
        }

        mPlayer.Draw(spriteBatch);
    }
}