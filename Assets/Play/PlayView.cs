using UnityEngine;
using System.Collections;
using GDGeek;

public class PlayView : Singleton<PlayView> {
	//public 


	public delegate void Function(bool insert);


	public event Function onClick;


	public Material _material;
	public Material _diffMaterial;
	public GameObject _offset = null;
	public PlayBasePair _curr = null;
	public PlayBasePair _back = null;
    public TimeManager _time;
	public GameUI _ui = null;
	private Vector3 offset_;
	private bool where_ = true;

    public Task close(float time = 1.3f) {
        float w = 0;
        TweenTask tt = new TweenTask(delegate () {
            return TweenRotation.Begin(_offset, time, Quaternion.AngleAxis(w, Vector3.right));
        });
        TaskManager.PushFront(tt, delegate () {
            _isEnter = false;
            _back.hide();
            _curr.stop();
            if (where_)
            {
                _offset.transform.localRotation = Quaternion.AngleAxis(0, Vector3.right);
                w = 180;
            }
            else
            {
                _offset.transform.localRotation = Quaternion.AngleAxis(180, Vector3.right);
                w = 360;
            }
            where_ = !where_;

        });
        TaskManager.PushBack(tt, delegate () {
            var temp = _curr;
            _curr = _back;
            _back = temp;
            _back.hide();
        });

        return tt;
    }
	public Task open(float time = 1.3f){

		float w = 0;
		TweenTask tt = new TweenTask (delegate() {
			return TweenRotation.Begin(_offset, time, Quaternion.AngleAxis(w, Vector3.right));
		});
		TaskManager.PushFront (tt, delegate() {
			_isEnter = false;
			_back.reset();
			_curr.stop();
			if(where_){
				_offset.transform.localRotation =  Quaternion.AngleAxis(0, Vector3.right);
				w = 180;
			}else{
				_offset.transform.localRotation =  Quaternion.AngleAxis(180, Vector3.right);
				w = 360;
			}
			where_ = !where_;
		
		});
		TaskManager.PushBack (tt, delegate() {
			var temp = _curr;
			_curr = _back;
			_back = temp;
			_back.reset();
			_curr.play();
		});

		return tt;

	}
   
	public void setup(VoxelStruct left, VoxelStruct right, VoxelStruct diff){
		
		setupLeft (left);
		setupRight (right);
		setupDiff (diff);
	
	}
	private bool _isEnter = false;
	//private 
	private void setupDiff(VoxelStruct diff){
		var data = VoxelBuilderHelper.Struct2DataInCache(diff);
		MeshFilter filter = VoxelBuilderHelper.Data2Filter(data);
		VoxelBuilder.FilterAddRenderer (filter, _diffMaterial);
		filter.name = "Object";
		filter.gameObject.layer = _back._left.gameObject.layer;
		VoxelBuilder.FilterAddCollider (filter);
		LogoInput input = filter.gameObject.AddComponent<LogoInput> ();
		input.onMouseEnter += delegate() {
			_isEnter = true;
		};
		input.onMouseExit += delegate() {
			_isEnter = false;
		};
		_back._left.setDiff (input.gameObject);

		filter.transform.localPosition = offset_;

		var input2 = GameObject.Instantiate (input);
		input2.onMouseEnter += delegate() {
			_isEnter = true;
		};
		input2.onMouseExit += delegate() {
			_isEnter = false;
		};

		_back._right.setDiff (input2.gameObject);
		input2.transform.localPosition = offset_;
	}

	private void setupRight(VoxelStruct right){
		var data = VoxelBuilderHelper.Struct2DataInCache(right);
		var mesh = VoxelBuilder.Data2Mesh (data);
		var filter = VoxelBuilder.Mesh2Filter (mesh);
		VoxelBuilder.FilterAddRenderer (filter, _material);
		_back._right.setMesh (filter.gameObject);
		filter.gameObject.transform.localPosition = offset_;


	}
	private void setupLeft(VoxelStruct left){
		var data = VoxelBuilderHelper.Struct2DataInCache(left);
		var mesh = VoxelBuilder.Data2Mesh (data);
		var filter = VoxelBuilder.Mesh2Filter (mesh);
		VoxelBuilder.FilterAddRenderer (filter, _material);
		_back._left.setMesh (filter.gameObject);
		offset_ = new Vector3(data.offset.x, 2, data.offset.z);
		filter.gameObject.transform.localPosition = offset_;

	}
    public void Start() {
	    /*
        if(GazeGestureManager.Instance != null) { 
            GazeGestureManager.Instance.onClicked += delegate()
            {
                if (onClick != null)
                {
                    onClick(_isEnter);
                }
            };
        }*/
    }
	public void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (onClick != null) {
				onClick (_isEnter);
			}
		}
	}


}
