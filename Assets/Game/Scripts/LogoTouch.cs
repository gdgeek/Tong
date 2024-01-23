using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LogoTouch : MonoBehaviour {
    public LogoInput _input;
    private float time_ = 0;
    private bool enable_ = false;
    public float _allTime = 1.0f;


    public delegate void Function();

    [SerializeField]
    private UnityEvent _onEnter;
   // public event Function onEnter_;
    // Use this for initialization
    void Start () {
        _input.onMouseEnter.AddListener(() =>
        {
            time_ = 0;
            enable_ = true;
        });

        _input.onMouseClick.AddListener(() =>
        {
            _onEnter?.Invoke();
        });
            
       

        _input.onMouseExit.AddListener(() =>
        {
            enable_ = false;
        });


    }
	
	// Update is called once per frame
	void Update () {
        if (enable_) {
            time_ += Time.deltaTime;
            if (time_ > _allTime) {
                enable_ = false;
                _onEnter?.Invoke();

            }
        }
	}
}
