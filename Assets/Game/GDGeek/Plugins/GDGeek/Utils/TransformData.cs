using System;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
    [Serializable]
    public class TransformData
    {
     /*   public enum Type
        {
            Local,
            World,
        }*/
        public TransformData()
        {
        }

        public static Vector3 L2R(Vector3 lh)
        {
            return new Vector3(-lh.x, lh.y, lh.z);
        }
        
        public static Quaternion L2RQ(Vector3 v)
        {
           
            var qx = Quaternion.AngleAxis(v.x, Vector3.right);
            var qy = Quaternion.AngleAxis(-v.y, Vector3.up);
            var qz = Quaternion.AngleAxis(-v.z, Vector3.forward);
            var qq = qz * qy * qx;
            return qq;
           
        }

        public static bool operator ==(TransformData lhs, TransformData rhs)
        {

            if (null == (object)(lhs) && null == (object)rhs)
            {
                return true;
            }
            if (null == (object)lhs || null == (object)rhs)
            {

                return false;
            }
            if (lhs.Equals(rhs))
            {
                return true;
            }

            return false;
            //return lhs.position == rhs.position && lhs.rotation == rhs.rotation && lhs.scale == rhs.scale;
        }

        public void write(ref Transform transform, Space type = Space.World)
        {

            if (type == Space.Self)
            {
                transform.localPosition = position;
                transform.localRotation = rotation;
                transform.localScale = scale;
            }
            else {

                transform.position = position;
                transform.rotation = rotation;
                transform.setGlobalScale(scale);
            }
         }

        public static bool operator !=(TransformData lhs, TransformData rhs)
        {

            return !(lhs == rhs);
          
        }
        public TransformData(Transform transform, Space type = Space.World)
        {
            if (type == Space.Self)
            {

                position = transform.localPosition;
                rotation = transform.localRotation;
                scale = transform.localScale;
            }
            else {

                position = transform.position;
                rotation = transform.rotation;
                scale = transform.lossyScale;
            }
        }

     
        public TransformData(Vector3 p, Quaternion r, Vector3 s)
        {
           
            position = p;
            rotation = r;
            scale = s;
            
        }
        [SerializeField]
        public Vector3 position;
        [SerializeField]
        public Quaternion rotation;
        [SerializeField]
        public Vector3 scale;

        public override bool Equals(object obj)
        {
            var data = obj as TransformData;
            return data != null &&
                   position.Equals(data.position) &&
                   rotation.Equals(data.rotation) &&
                   scale.Equals(data.scale);
        } 

        public override int GetHashCode()
        {
            var hashCode = -1285106862;
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Quaternion>.Default.GetHashCode(rotation);
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(scale);
            return hashCode;
        }
    }


}