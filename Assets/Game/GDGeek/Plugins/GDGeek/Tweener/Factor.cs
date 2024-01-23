using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek{
    [System.Serializable]
    public class Factor {


        public enum Style
        {
            Once,
            Loop,
            PingPong,
            Forward,
        }

        [SerializeField]
        private Style _style;
        public Style style {
            set {
                _style = value;
            }
        }

        private float val_ = 0;
        public float val{
            get {
                return val_;
            }
        }

        private bool finished_ = false;
        private float accumulation_ = 0.0f;
        public bool finished
        {
            get
            {
                return finished_;
            }
        }
        public void once() {
            if (accumulation_ >= 1.0f)
            {
                val_ = 1.0f;
                finished_ = true;
            }
            else {
                val_ = accumulation_;
                finished_ = false;
            }
        }

        public void forward()
        {
            if (accumulation_ >= 1.0f)
            {
                val_ = 0.0f;
                finished_ = true;
            }
            else
            {
                val_ = 1.0f - accumulation_;
                finished_ = false;
            }
        }

        public void reset() {
            accumulation_ = 0f;
            finished_ = false;
        }
        public void loop()
        {
            val_ = Mathf.Repeat(accumulation_, 1f);
            finished_ = false;

        }
        public void pingPong() {
            val_ = Mathf.PingPong(accumulation_, 1f);
            finished_ = false;
        }


        public void increase(float delta) {
            accumulation_ += delta;
            switch (_style)
            {
                case Style.Once:
                    once();
                    break;
                case Style.Loop:
                    loop();
                    break;
                case Style.Forward:
                    forward();
                    break;
                case Style.PingPong:
                    pingPong();
                    break;

            }

        }

    }

}
