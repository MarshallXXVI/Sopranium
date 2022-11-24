using System.Collections.Generic;
using KnightsOfLaCampus.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KnightsOfLaCampus.Source.Astar;
using Microsoft.Xna.Framework.Audio;
using TiledSharp;

namespace KnightsOfLaCampus.Source;

internal sealed class World
{
    private readonly Vector2 mOffSet;
    private readonly SoMuchOfSpots mPlayerField;
    private readonly Player mPlayer;
    private readonly Enemy mEnemy, mEnemy1, mEnemy2, mEnemy3, mEnemy4, mEnemy5;
    private readonly List<Enemy> mMobs;
    private readonly TileMapManager mMapManager;


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

        // Test if every body can find path.
        mMobs = new List<Enemy>();
        mEnemy = new Enemy(mPlayer, mPlayerField){Position = new Vector2(16,16)};
        mEnemy1 = new Enemy(mPlayer, mPlayerField) { Position = new Vector2(1100, 16) };
        mEnemy2 = new Enemy(mPlayer, mPlayerField) { Position = new Vector2(1000, 16) };
        mEnemy3 = new Enemy(mPlayer, mPlayerField) { Position = new Vector2(128, 100) };
        mEnemy4 = new Enemy(mPlayer, mPlayerField) { Position = new Vector2(400, 100) };
        mEnemy5 = new Enemy(mPlayer, mPlayerField) { Position = new Vector2(1000, 700) };

        // more Enemy can be inherit from Enemy class with same method.
        // Enemy has not marked as sealed can test it with inheritance.

        mMobs.Add(mEnemy);
        mMobs.Add(mEnemy1);
        mMobs.Add(mEnemy2);
        mMobs.Add(mEnemy3);
        mMobs.Add(mEnemy4);
        mMobs.Add(mEnemy5);
    }
    internal void Update(GameTime gameTime)
    {
        mPlayer.Update(gameTime);
        foreach (var enemy in mMobs)
        {
            enemy.Update(gameTime);
        }
        mPlayerField.Update(mOffSet);
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        mMapManager.Draw();
        mPlayerField.Draw(Vector2.Zero);

        foreach (var enemy in mMobs)
        {
            enemy.Draw(spriteBatch);
        }

        mPlayer.Draw(spriteBatch);
    }
}