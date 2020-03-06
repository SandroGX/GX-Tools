using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GX {
    [System.Serializable]
    public class SODictionary : ScriptableObject, ISerializationCallbackReceiver
    {
        private Dictionary<ScriptableObject, ScriptableObject> dic;


        public void OnEnable()
        {
            if (dic == null)
                dic = new Dictionary<ScriptableObject, ScriptableObject>();
        }

        public void Add(ScriptableObject key, ScriptableObject value)
        {
            if (ContainsKey(key))
                return;
            SODatabase.Add(key, value, key, dic);
            EditorUtility.SetDirty(this);
        }

        public void Remove(ScriptableObject key)
        {
            if (!ContainsKey(key))
                return;
            SODatabase.Remove(key, Get<ScriptableObject>(key), key, dic);
            EditorUtility.SetDirty(this);
        }

        public T Get<T>(ScriptableObject key) where T : ScriptableObject
        {
            return ContainsKey(key) ? dic[key] as T : null;
        }

        public bool ContainsKey(ScriptableObject key)
        {
            return dic.ContainsKey(key);
        }

        //for serialization
        [SerializeField]
        private List<ScriptableObject> keys, values;

        public void OnAfterDeserialize()
        {
            dic = new Dictionary<ScriptableObject, ScriptableObject>();

            if (keys == null)
                return;

            for (int i = 0; i < keys.Count; ++i)
                dic.Add(keys[i], values[i]);
        }

        public void OnBeforeSerialize()
        {
            if (dic == null)
                return;
            keys = new List<ScriptableObject>();
            values = new List<ScriptableObject>();
            foreach (ScriptableObject key in dic.Keys)
                if (key)
                    keys.Add(key);
            foreach (ScriptableObject value in dic.Values)
                if (value)
                    values.Add(value);
        }
    }
}