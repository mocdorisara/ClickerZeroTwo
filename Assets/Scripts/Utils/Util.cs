using System;
using System.Xml;
using System.Diagnostics;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Game.Utils
{
    public class Util
    {
        public enum BUTTONTYPE
        {
            Button,
            Story
        }

        const float SCREEN_HEIGHT = 1080.0f;
        const float SCREEN_WIDTH = 1920.0f;

        Action action = null;

        public void test()
        {
            if (action != null)
            {
                action.Invoke();
            }
        }

        public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            UnityEngine.Debug.Log(typeof(T).Name);

            

            
            T component = go.GetComponent<T>();
            if (component == null)
            {
                component = go.AddComponent<T>();
            }

            return component;
        }
        public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
        {
            Transform transform = FindChild<Transform>(go, name, recursive);
            if (transform == null)
            {
                return null;
            }

            return transform.gameObject;
        }
        public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
        {
            if (go == null)
            {
                return null;
            }

            if (recursive == false)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    Transform transform = go.transform.GetChild(i);
                    if (string.IsNullOrEmpty(name) || transform.name == name)
                    {
                        T component = transform.GetComponent<T>();
                        if (component != null)
                        {
                            return component;
                        }
                    }
                }
            }
            else
            {
                foreach (T component in go.GetComponentsInChildren<T>(true))
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                    {
                        return component;
                    }
                }
            }

            return null;
        }

        public static XmlNodeList LoadXml(string path, string rootNode)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(path);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            return xmlDoc.SelectNodes(rootNode);
        }

        public static XmlNodeList LoadXmlFullPath(string path, string rootNode)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            return xmlDoc.SelectNodes(rootNode);
        }

        public static Sprite CreateSprite(Texture2D texture)
        {
            return Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
        }


        public static void DebugErrorLog(string message)
        {
#if DEV_BUILD
            UnityEngine.Debug.LogError(message);
#endif
        }

        public static bool NeedAppUpdate(string version)
        {
            var appVersions = ParseSematicVersion(Application.version);
            var newVersions = ParseSematicVersion(version);

            // major
            if (appVersions.Item1 < newVersions.Item1) return true;
            if (appVersions.Item2 < newVersions.Item2) return true;
            if (appVersions.Item3 < newVersions.Item3) return true;

            return false;
        }

        private static Tuple<int, int, int> ParseSematicVersion(string version)
        {
            string[] versions = version.Split('.');
            return new Tuple<int, int, int>(
                Int32.Parse(versions[0]),
                Int32.Parse(versions[1]),
                Int32.Parse(versions[2]));
        }

        private static Stopwatch sw = new Stopwatch();

        public static void StartTime()
        {
            sw.Restart();
        }

        public static TimeSpan EndTime()
        {
            sw.Stop();

            return sw.Elapsed;
        }

        public static byte[] ObjectToByteArray(System.Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        public static System.Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            System.Object obj = (System.Object)binForm.Deserialize(memStream);

            return obj;
        }
    }
}