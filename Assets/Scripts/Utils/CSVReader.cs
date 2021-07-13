using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
using System.IO;

namespace Game.Utils
{

    public class CSVReader
    {
        static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        static char[] TRIM_CHARS = { '\"' };

        public static List<T> Read<T>(string file) where T : new()
        {
            var list = new List<T>();

            string csv = File.ReadAllText(file);

            var lines = Regex.Split(csv, LINE_SPLIT_RE);

            if (lines.Length <= 1) return list;

            var header = Regex.Split(lines[0], SPLIT_RE);

            Type tp = typeof(T);
            FieldInfo[] flds = tp.GetFields(BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static);

            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "") continue;

                var entry = new T();

                for (var j = 0; j < header.Length && j < values.Length; j++)
                {

                    string value = values[j];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                    value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
                    value = value.Replace("<c>", ",");

                    object finalvalue = value;
                    int n;
                    float f;
                    if (int.TryParse(value, out n))
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))
                    {
                        finalvalue = f;
                    }

                    for (var k = 0; k < flds.Length; k++)
                    {

                        FieldInfo fieldInfo = flds[k];
                        if (header[j] == fieldInfo.Name)
                        {

                            if ("crc32".Equals(fieldInfo.Name))
                            {
                                fieldInfo.SetValue(entry, finalvalue.ToString());
                            }
                            else if ("textFileVersion".Equals(fieldInfo.Name) ||
                                "thumbnailVersion".Equals(fieldInfo.Name))
                            {
                                continue;
                            }
                            else
                            {
                                try
                                {
                                    fieldInfo.SetValue(entry, finalvalue);
                                }
                                catch (Exception e)
                                {
                                    Debug.Log($"{file}: {fieldInfo.Name}");
                                    Debug.Log($"{e.ToString()}");
                                }
                            }
                        }
                    }
                }
                list.Add(entry);
            }
            return list;

        }
        public static List<Dictionary<string, object>> Read(string file)
        {
            var list = new List<Dictionary<string, object>>();
            TextAsset data = Resources.Load(file) as TextAsset;

            var lines = Regex.Split(data.text, LINE_SPLIT_RE);

            if (lines.Length <= 1) return list;

            var header = Regex.Split(lines[0], SPLIT_RE);
            for (var i = 1; i < lines.Length; i++)
            {

                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "") continue;

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                    value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
                    value = value.Replace("<c>", ",");

                    object finalvalue = value;
                    int n;
                    float f;
                    if (int.TryParse(value, out n))
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))
                    {
                        finalvalue = f;
                    }
                    entry[header[j]] = finalvalue;
                }
                list.Add(entry);
            }
            return list;
        }
    }
}