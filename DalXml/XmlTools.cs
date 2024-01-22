//namespace Dal;

//using DO;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

//static class XMLTools
//{
//    const string s_xml_dir = @"..\xml\";
//    static XMLTools()
//    {
//        if (!Directory.Exists(s_xml_dir))
//            Directory.CreateDirectory(s_xml_dir);
//    }

//    #region Extension Fuctions
//    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
//        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;
//    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
//        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;
//    public static double? ToDoubleNullable(this XElement element, string name) =>
//        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;
//    public static int? ToIntNullable(this XElement element, string name) =>
//        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
//    #endregion

//    #region XmlConfig
//    public static int GetAndIncreaseNextId(string data_config_xml, string elemName)
//    {
//        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
//        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert id.  {data_config_xml}, {elemName}");
//        root.Element(elemName)?.SetValue((nextId + 1).ToString());
//        XMLTools.SaveListToXMLElement(root, data_config_xml);
//        return nextId;
//    }
//    #endregion

//    #region SaveLoadWithXElement
//    public static void SaveListToXMLElement(XElement rootElem, string entity)
//    {
//        string filePath = $"{s_xml_dir + entity}.xml";
//        try
//        {
//            rootElem.Save(filePath);
//        }
//        catch (Exception ex)
//        {
//            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
//        }
//    }
//    public static XElement LoadListFromXMLElement(string entity)
//    {
//        string filePath = $"{s_xml_dir + entity}.xml";
//        try
//        {
//            if (File.Exists(filePath))
//                return XElement.Load(filePath);
//            XElement rootElem = new(entity);
//            rootElem.Save(filePath);
//            return rootElem;
//        }
//        catch (Exception ex)
//        {
//            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
//        }
//    }
//    #endregion

//    #region SaveLoadWithXMLSerializer
//    public static void SaveListToXMLSerializer<T>(List<T> list, string entity) where T : class
//    {
//        string filePath = $"{s_xml_dir + entity}.xml";
//        try
//        {
//            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
//            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
//        }
//        catch (Exception ex)
//        {
//            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
//        }
//    }
//    public static List<T> LoadListFromXMLSerializer<T>(string entity) where T : class
//    {
//        string filePath = $"{s_xml_dir + entity}.xml";
//        try
//        {
//            if (!File.Exists(filePath)) return new();
//            using FileStream file = new(filePath, FileMode.Open);
//            XmlSerializer x = new(typeof(List<T>));
//            return x.Deserialize(file) as List<T> ?? new();
//        }
//        catch (Exception ex)
//        {
//            throw new DalXMLFileLoadCreateException($"fail to load xml file: {filePath}, {ex.Message}");
//        }
//    }
//    #endregion
//}


namespace Dal;

using DO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// Provides XML utility functions for loading, saving, and manipulating XML data.
/// </summary>
 static class XMLTools
{
    // Directory for storing XML files.
    const string s_xml_dir = @"..\xml\";

    /// <summary>
    /// Static constructor to initialize the XMLTools class.
    /// </summary>
    static XMLTools()
    {
        if (!Directory.Exists(s_xml_dir))
            Directory.CreateDirectory(s_xml_dir);
    }

    #region Extension Fuctions

    /// <summary>
    /// Converts an XElement's value to a nullable enum of type T.
    /// </summary>
    /// <param name="element">The XElement containing the enum value.</param>
    /// <param name="name">The name of the element to convert.</param>
    /// <returns>A nullable enum of type T.</returns>
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    /// <summary>
    /// Converts an XElement's value to a nullable DateTime.
    /// </summary>
    /// <param name="element">The XElement containing the DateTime value.</param>
    /// <param name="name">The name of the element to convert.</param>
    /// <returns>A nullable DateTime.</returns>
    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;

    /// <summary>
    /// Converts an XElement's value to a nullable double.
    /// </summary>
    /// <param name="element">The XElement containing the double value.</param>
    /// <param name="name">The name of the element to convert.</param>
    /// <returns>A nullable double.</returns>
    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;

    /// <summary>
    /// Converts an XElement's value to a nullable int.
    /// </summary>
    /// <param name="element">The XElement containing the int value.</param>
    /// <param name="name">The name of the element to convert.</param>
    /// <returns>A nullable int.</returns>
    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;

    #endregion

    #region XmlConfig

    /// <summary>
    /// Retrieves and increments the next ID from the XML configuration file.
    /// </summary>
    /// <param name="data_config_xml">The XML file containing the configuration.</param>
    /// <param name="elemName">The name of the element containing the ID.</param>
    /// <returns>The next available ID.</returns>
    public static int GetAndIncreaseNextId(string data_config_xml, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert id.  {data_config_xml}, {elemName}");
        root.Element(elemName)?.SetValue((nextId + 1).ToString());
        XMLTools.SaveListToXMLElement(root, data_config_xml);
        return nextId;
    }

    #endregion

    #region SaveLoadWithXElement

    /// <summary>
    /// Saves an XElement to an XML file.
    /// </summary>
    /// <param name="rootElem">The root XElement to save.</param>
    /// <param name="entity">The entity name which is used to name the file.</param>
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    /// <summary>
    /// Loads an XML file and returns its root XElement.
    /// </summary>
    /// <param name="entity">The entity name which is used to name the file.</param>
    /// <returns>The root XElement of the loaded XML file.</returns>
    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    #endregion

    #region SaveLoadWithXMLSerializer

    /// <summary>
    /// Serializes a list of type T to an XML file using the XMLSerializer.
    /// </summary>
    /// <param name="list">The list of type T to serialize.</param>
    /// <param name="entity">The entity name which is used to name the file.</param>
    /// <typeparam name="T">The type of the list items.</typeparam>
    public static void SaveListToXMLSerializer<T>(List<T> list, string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    /// <summary>
    /// Deserializes a list of type T from an XML file using the XMLSerializer.
    /// </summary>
    /// <param name="entity">The entity name which is used to name the file.</param>
    /// <typeparam name="T">The type of the list items.</typeparam>
    /// <returns>A list of type T deserialized from the XML file.</returns>
    public static List<T> LoadListFromXMLSerializer<T>(string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T>));
            return x.Deserialize(file) as List<T> ?? new();
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {filePath}, {ex.Message}");
        }
    }
    #endregion
}



