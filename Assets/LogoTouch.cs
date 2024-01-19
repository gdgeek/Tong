using UnityEngine;
using System.Collections;

public class LogoTouch : MonoBehaviour {
    public LogoInput _input;
    private float time_ = 0;
    private bool enable_ = false;
    public float _allTime = 1.0f;


    public delegate void Function();


    public event Function onEnter;
    // Use this for initialization
    void Start () {
        _input.onMouseEnter += delegate
        {
            time_ = 0;
            enable_ = true;
        };

        _input.onMouseClick += delegate
        {
            if (onEnter != null)
            {
                onEnter();
            }
        };

        _input.onMouseExit += delegate
        {
            enable_ = false;
        };


    }
	
	// Update is called once per frame
	void Update () {
        if (enable_) {
            time_ += Time.deltaTime;
            if (time_ > _allTime) {
                enable_ = false;
                if(onEnter != null){
                    onEnter();
                }

            }
        }
	}
}
