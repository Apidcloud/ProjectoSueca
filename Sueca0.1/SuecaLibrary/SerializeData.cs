using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SuecaLibrary
{
    public static class SerializeData
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
}
