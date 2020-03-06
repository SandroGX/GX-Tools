#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace GX
{
    public static class SODatabase
    {

        public static void Add<P, C>(P parent, C newObject, IList<C> children) where P : ScriptableObject where C : ScriptableObject
        {
            AssetDatabase.AddObjectToAsset(newObject, parent);
            children.Add(newObject);
        }
        public static void Add<P, C>(P parent, C newObject, System.Action<C> Add) where P : ScriptableObject where C : ScriptableObject
        {
            AssetDatabase.AddObjectToAsset(newObject, parent);
            Add(newObject);
        }

        public static void Remove<P, C>(P parent, C toRemove, IList<C> children) where P : ScriptableObject where C : ScriptableObject
        {
            children.Remove(toRemove);
            ScriptableObject.DestroyImmediate(toRemove, true);
        }
        public static void Remove<P, C>(P parent, C toRemove, System.Action<C> Remove) where P : ScriptableObject where C : ScriptableObject
        {
            Remove(toRemove);
            ScriptableObject.DestroyImmediate(toRemove, true);
        }

        public static void Clear<P, C>(P parent, IList<C> children) where P : ScriptableObject where C : ScriptableObject
        {
            foreach(C c in children)
                ScriptableObject.DestroyImmediate(c, true);

            children.Clear();
        }

        public static void Add<P, V, K>(P parent, V newObject, K key, Dictionary<K, V> children) where P : ScriptableObject where V : ScriptableObject
        {
            AssetDatabase.AddObjectToAsset(newObject, parent);
            children.Add(key, newObject);
        }
        public static void Add<P, V, K>(P parent, V newObject, K key, System.Action<K, V> Add) where P : ScriptableObject where V : ScriptableObject
        {
            AssetDatabase.AddObjectToAsset(newObject, parent);
            Add(key, newObject);
        }

        public static void Remove<P, V, K>(P parent, V value, K key, Dictionary<K, V> children) where P : ScriptableObject where V : ScriptableObject
        {
            children.Remove(key);
            ScriptableObject.DestroyImmediate(value, true);
        }
        public static void Remove<P, V, K>(P parent, V value, K key, System.Action<K> Remove) where P : ScriptableObject where V : ScriptableObject
        {
            Remove(key);
            ScriptableObject.DestroyImmediate(value, true);
        }

        public static List<T> GetSubObjectsOfType<T>(ScriptableObject asset) where T : UnityEngine.Object
        {
            Object[] objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(asset));

            List<T> ofType = new List<T>();

            foreach (Object o in objs)
            {
                if (o is T)
                {
                    ofType.Add((T)o);
                }
            }

            return ofType;
        }

        public static T GetSubObjectOfType<T>(ScriptableObject asset) where T : UnityEngine.Object
        {
            Object[] objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(asset));

            foreach (Object o in objs)
            {
                if (o is T)
                {
                    return (T)o;
                }
            }

            return null;
        }
    }
}
#endif