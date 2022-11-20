using System.Collections.Generic;
using System.IO;
using KnightsOfLaCampus.Saves;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Managers;

internal sealed class SaveManager
{
    private readonly XmlManager<List<object>> mXmlManager;

    // The path of the output file
    private readonly string mPath;

    public SaveManager()
    {
        mXmlManager = new XmlManager<List<object>>();

        mPath = Directory.GetCurrentDirectory() + @"\Saves\SaveGame.xml";

        if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Saves"))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Saves");
        }
    }

    // Saves each property of SavedVariables to the xml file
    public void SaveToXml()
    {
        // The list, that is going to be stored
        var toSave = new List<object>();

        // Adds the values to the list
        toSave.Add(SavedVariables.KingPositon);

        // #TODO add more values that need to be stored

        // Stores the list to xml file 
        mXmlManager.Save(mPath, toSave);
    }

    // Loads the contents of the xml in the Saved Variables class
    public void LoadFromXml()
    {
        // The list that the xml Manager returns
        var fromSave = mXmlManager.Load(mPath);

        // Loads the King's position (very dirty solution)
        SavedVariables.KingPositon = (Vector2)fromSave[0];
    }
}