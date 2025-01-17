﻿using System.Collections.Generic;
using KnightsOfLaCampus.UnitsGameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Source.Interfaces
{
    internal interface IFriendlyUnit

    {
        public int Id { get;}
        public bool IsSelected { get; set; }
        public bool IsDead { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public List<Vector2> Path { get; set; }
        public bool GetIfSelected();
        public void AudioUpdate();
        public void GraphicsUpdate();
        public void Update(GameTime gameTime, List<IEnemyUnit> enemies);
        public void Draw(SpriteBatch spriteBatch);
    }
}