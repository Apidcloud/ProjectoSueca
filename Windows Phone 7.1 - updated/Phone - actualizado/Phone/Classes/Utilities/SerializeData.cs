using Phone.Classes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Phone.Classes.Utilities
{
    public static class SerializeData
    {
        public static List<Carta> DeserializeFromXML(string filename)
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Carta>));
                Stream testpath = Application.GetResourceStream(new Uri(filename, UriKind.Relative)).Stream;
                TextReader textReader = new StreamReader(testpath);
                List<Carta> ob;
                ob = (List<Carta>)deserializer.Deserialize(textReader);
                textReader.Close();
                return ob;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
