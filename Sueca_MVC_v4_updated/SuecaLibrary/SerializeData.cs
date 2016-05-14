using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SuecaLibrary
{
    public static class SerializeData
    {
        public static class Binary
        {
            public static void SerializeObject(string filename, Object obj)
            {
                try
                {
                    BinaryFormatter bm = new BinaryFormatter();
                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bm.Serialize(fs, obj);
                    }
                }
                catch (Exception ex)
                {

                    Trace.Write(string.Format("Error!\nError Message: {0}", ex.Message));
                }

            }

            public static Object DeserializeObject(string filename)
            {
                try
                {
                    BinaryFormatter bm = new BinaryFormatter();

                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        Object o = (Object)(bm.Deserialize(fs));
                        return o;
                    }
                }
                catch (Exception ex)
                {

                    Trace.Write(string.Format("Error!\nError Message: {0}", ex.Message));
                    return null;
                }

            }
        }

        //public static class XML
        //{
        //    public static void SerializeToXML(string filename, object o)
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(object));
        //        TextWriter textWriter = new StreamWriter(filename + ".xml");
        //        serializer.Serialize(textWriter, o);
        //        textWriter.Close();
        //    }

        //    public static Object DeserializeFromXML(string filename)
        //    {
        //        XmlSerializer deserializer = new XmlSerializer(typeof(Object));
        //        TextReader textReader = new StreamReader(filename);
        //        Object ob;
        //        ob = (Object)deserializer.Deserialize(textReader);
        //        textReader.Close();
        //        return ob;
        //    }

        //}
        
    }
}
