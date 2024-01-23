using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GDGeek {

    public static class GameObjectUtility {
        public static void enableBehaviour<T>(this GameObject obj) where T : MonoBehaviour
        {
            T[] list = obj.GetComponents<T>();
            foreach (T t in list)
            {
                t.enabled = true;
            }

        }

        public static void disableBehaviour<T>(this GameObject obj) where T : MonoBehaviour
        {
            T[] list = obj.GetComponents<T>();
            foreach (T t in list)
            {
                t.enabled = false;
            }

        }
        public static void removeComponent<T>(this GameObject obj) where T : Component
        {

            T[] list = obj.GetComponents<T>();
            foreach (T t in list) {
                UnityEngine.Object.DestroyImmediate(t);
            }


        }
        public static bool HasComponent(this GameObject obj, Type type) 
        {
            if (type == null) {
                return false;
            }
            //Debug.Log(type.Name);
            Component component = obj.GetComponent(type);
            if (component == null)
            {
                return false;
            }
            return true;

        }
       
        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {

            T component = obj.GetComponent<T>();
            if (component == null)
            {
                return false;
            }
            return true;

        }
        public static T AboveComponent<T>(this GameObject obj) where T : Component
        {
            T component = obj.GetComponent<T>();
            if (component == null)
            {
                component = obj.GetComponentInParent<T>();
             //   Debug.Log(obj.longName() + "!!!!!!" + component);
            }
            return component;
        }
        public static Component AskComponent(this GameObject obj, Type type) {
            if (obj.HasComponent(type)) {
                return obj.GetComponent(type);
            }
            return obj.AddComponent(type);

        }
        
        public static T[] GetComponentsInChildrenExceptSelf<T>(this GameObject obj, bool includeInactive = false) where T : Component
        {
            T[] children = obj.GetComponentsInChildren<T>(includeInactive);

            T[] self = obj.GetComponents<T>();
            return children.Except(self).ToArray();

        }
        public static T GetComponentInSelfOrParent<T>(this GameObject obj) where T : Component
        {
            T component = null;
            component = obj.GetComponent<T>();
            if (component == null)
            {
                component = obj.GetComponentInParent<T>();

            }
            return component;

        }

        public static T AskComponent<T>(this GameObject obj) where T:Component  
	    {
            
		    T component = obj.GetComponent<T>();
		    if (component == null) {
			    component = obj.AddComponent<T> ();
		    }
		    return component;
	
	    }
        
	    public static void SetActiveTrue(this GameObject obj) {
             obj.SetActive(true);	
	    }

        public static void setLayerRecursively(this GameObject gameObject, int layer) {

            if (gameObject == null) {
                return;
            }
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform) {
                if (child != null) {
                    setLayerRecursively(child.gameObject, layer);
                }
            }
        }
	    public static void SetActiveFalse(this GameObject obj) {

            obj.SetActive(false);
	    }

        public static Renderer getRenderer(this GameObject obj)
        {
            return obj.gameObject.GetComponent<Renderer>();

        }

        public static Collider getCollider(this GameObject obj) {

             return obj.gameObject.GetComponent<Collider>();
        }

        public static string longName(this GameObject obj) {
            string name = obj.name;
            Transform parent = obj.transform.parent;
            while(parent != null) {
                name = parent.name + "/" + name;
                parent = parent.parent;
            }
            return name;
        
        }

    }
}