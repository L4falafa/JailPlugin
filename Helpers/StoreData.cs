using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Lafalafa.JailPlugin.Helpers

{
    public static class StoreData 
    {
        public static string path;

        public static void createFile()
        {
          
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                var xml = new XmlSerializer(typeof(List<JailModel>));
                var jailModels = new List<JailModel>();
                jailModels.Add(new JailModel(){ name = "Test", radius = 10, x = 10, z = 5, y = 20});
                xml.Serialize(writer, jailModels);
                writer.Dispose();
                writer.Close();
            }

        }

        public static void writeObject()
        {
            using (var writer = new FileStream(path, FileMode.OpenOrCreate,FileAccess.Write))
            {
               
                var xml = new XmlSerializer(typeof(List<JailModel>));
                xml.Serialize(writer, JailModel.getJails());
                writer.Dispose();
                writer.Close();
            }

        }

        public static void loadJails()
        {

            using (var ws = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var xml = new XmlSerializer(typeof(List<JailModel>));
    
                var deser = (List<JailModel>)xml.Deserialize(ws);
                Console.WriteLine($"[{Jail.instance.Name}] Jails loaded: {deser.Count }");
                foreach (JailModel jail in deser)
                {
           
        
                    JailModel.addNewJail(jail.name, jail.radius, new Vector3(jail.x, jail.y, jail.z));
                    Console.WriteLine($"[{Jail.instance.Name}] Name: {jail.name }");
                }
                ws.Dispose();
                ws.Close();
            }

        }


    }
}
