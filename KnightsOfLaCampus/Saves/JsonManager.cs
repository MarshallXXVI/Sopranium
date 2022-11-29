#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Interfaces;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Saves
{
    // mId is UnitID.
    // 0 = King.
    // 1 = Swordsman.
    // 2 = Archer.
    // 3 = Cavalry.
    // 4 = Healer.
    // 5 = Enemy1.
    // 6 = Enemy2.
    // 7 = Wall.

    public sealed class SaveObject
    {
        public int Id { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
    }
    internal static class JsonManager
    {
        public static void SaveGameObject(World info)
        {
            var saveContent = "[";
            for (var i = 0; i < info.mPlayer.mMyFriendlyUnits.Count; i++)
            {
                var save = new SaveObject
                {
                    Id = info.mPlayer.mMyFriendlyUnits[i].Id,
                    PositionX = info.mPlayer.mMyFriendlyUnits[i].Position.X,
                    PositionY = info.mPlayer.mMyFriendlyUnits[i].Position.Y,
                };
                var jsonString = JsonSerializer.Serialize(save);
                if (i == info.mPlayer.mMyFriendlyUnits.Count - 1 && info.mMobs.Count == 0)
                {
                    saveContent += jsonString;
                }
                else
                {
                    saveContent += jsonString + ",";
                }
            }

            for (var i = 0; i < info.mMobs.Count; i++)
            {
                var save = new SaveObject
                {
                    Id = info.mMobs[i].Id,
                    PositionX = info.mMobs[i].Position.X,
                    PositionY = info.mMobs[i].Position.Y,
                };
                var jsonString = JsonSerializer.Serialize(save);
                if (i != info.mMobs.Count - 1)
                {
                    saveContent += jsonString + ",";
                }
                else if (i == info.mMobs.Count - 1)
                {
                    saveContent += jsonString;
                }
            }

            //TODO ADD GOLD THAT DROP FROM MOBS.
            saveContent += "]";
            File.WriteAllText("save.json", saveContent);
            SaveGameValue();
        }

        private static void SaveGameValue()
        {
            //TODO ADD ALL SINGLE VALUE SUCH AS AMOUNT OF GOLD, LEVEL, MapID, TIME.
            var value = JsonSerializer.Serialize(GameGlobals.mGold);
            File.WriteAllText("saveValue.json", value);
        }

        public static List<SaveObject>? LoadGameObjects()
        {
            var saveObjects =
                JsonSerializer.Deserialize<List<SaveObject>>(File.ReadAllText("save.json"));
            return saveObjects ?? null;
        }

        public static int LoadGameValue()
        {
            var saveValue = JsonSerializer.Deserialize<int>(File.ReadAllText("saveValue.json"));
            return saveValue;
        }
    }
}
