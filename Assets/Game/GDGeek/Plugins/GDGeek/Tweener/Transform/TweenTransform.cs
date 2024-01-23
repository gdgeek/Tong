using UnityEngine;
using System.Collections;
namespace GDGeek
{
    public class TweenTransform : Tween
    {
        
        [SerializeField]
        public Space space = Space.World;

        [SerializeField]
        private TransformData fromData_ = null;

        [SerializeField]
        private bool _tweenPosition = true;
        [SerializeField]
        private bool _tweenScale = true;
        [SerializeField]
        private bool _tweenRotation = true;
        
        public TransformData fromData {

            get {
                if (_from != null)
                {

                    return new TransformData(_from, this.space);
                }
                else {
                    return this.fromData_;
                }
            }
        }

        public override void onStarted()
        {
            this.fromData_ = new TransformData(target.transform, this.space);
            base.onStarted();
        }
      
        [SerializeField]
        private Transform _from;
        [SerializeField]
        private Transform _to;


        override protected void onUpdate(float factor, bool isFinished)
        {
            if (_to)
            {
                if (space == Space.Self)
                {
                    if (_tweenPosition)
                    {
                        target.transform.localPosition = fromData.position * (1f - factor) + _to.localPosition * factor;
                    }

                    if (_tweenRotation)
                    {
                        target.transform.localRotation = Quaternion.Slerp(fromData.rotation, _to.localRotation, factor);
                    }

                    if (_tweenScale)
                    {
                        target.transform.localScale = (fromData.scale * (1f - factor) + _to.localScale * factor);
                    }
                }
                else
                {
                    if (_tweenPosition)
                    {
                        target.transform.position = fromData.position * (1f - factor) + _to.position * factor;
                    }

                    if (_tweenRotation)
                    {
                        target.transform.rotation = Quaternion.Slerp(fromData.rotation, _to.rotation, factor);
                    }

                    if (_tweenScale)
                    {
                        target.transform.setGlobalScale(fromData.scale * (1f - factor) + _to.lossyScale * factor);
                    }
                }


            }
            if (isFinished)
            {
                fromData_ = null;
            }
        }


        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public TweenTransform Begin(GameObject go, float duration, Transform to, Space space = Space.World)
        {
            TweenTransform component = Tween.Begin<TweenTransform>(go, duration);
            component.space = space;
            component._to = to;

            if (duration <= 0f)
            {
                component.sample(1f, true);
                component.enabled = false;
            }
            return component;
        }
    }
}