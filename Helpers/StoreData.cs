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
          
            using (var writer = new FileStream(path, FileMode.Create))
            {
                var xml = new XmlSerializer(typeof(List<JailModel>));
                var jailModels = new List<JailModel>();
                jailModels.Add(new JailModel(){ name = "Test", radius = 10, x = 10, z = 5, y = 20});
                xml.Serialize(writer, jailModels);
            }

        }

        public static void writeObject()
        {
            using (var writer = new FileStream(path, FileMode.Open))
            {
                var xml = new XmlSerializer(typeof(List<JailModel>));
                xml.Serialize(writer, JailModel.getJails());
            }

        }

        public static void loadJails()
        {

            using (var ws = new FileStream(path, FileMode.Open))
            {
                var xml = new XmlSerializer(typeof(List<JailModel>));
                foreach (JailModel jail in (List<JailModel>)xml.Deserialize(ws))
                {

                    JailModel.addNewJail(jail.name, jail.radius, new Vector3(jail.x, jail.y, jail.z));

                }
            }

        }


    }
}
