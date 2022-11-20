using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Managers;

internal sealed class XmlManager<T>
{
    // The Type of object, that has to be loaded
    // In our case it might be the type of the 
    // the ScreenType that has to be loaded. If 
    // there is a GameScreen the type is the type of 
    // GameScreen
    private readonly XmlSerializer mXmlSerializer;

    public XmlManager()
    {
        // To make it possible to serialize types like Vector2, ...
        mXmlSerializer = new XmlSerializer(typeof(T), new Type[]
        {
            typeof(Rectangle), 
            typeof(Vector2), 
            typeof(Color)
        });
    }

    public T Load(string path)
    {
        T instance;

        // To dispose the TextReader after it goes out of scope
        // cant be removed 
        using (TextReader read = new StreamReader(path))
        {
            instance = (T)mXmlSerializer.Deserialize(read);
        }

        return instance;
    }

    public void Save(string path, object obj)
    {
        // To dispose the TextWriter after it goes out of scope
        // cant be removed 
        using (TextWriter write = new StreamWriter(path))
        {
            // obj can be any object that needs to be stored at
            // the given path
            mXmlSerializer.Serialize(write, obj);
        }
    }
}