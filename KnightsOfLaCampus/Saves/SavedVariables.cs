using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Saves;

internal static class SavedVariables
{
    // The flag if the stored variables are going to be loaded
    public static bool LoadSavedVariables { get; set; }

    // Each Variable, that has to be saved needs to declared here:

    // The King's Position is allways the first
    public static Vector2 KingPositon { get; set; }
}