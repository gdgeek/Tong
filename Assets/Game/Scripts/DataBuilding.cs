using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;

public class DataBuilding : MonoBehaviour {
	public TextAsset _voxFile = null;
	public TextAsset _diffFile = null;
	public Material _material;
	public Material _diffMaterial;
	public GameObject _offset = null;
	public GameObject _mirror = null;
	public GameObject _diff = null;
	public VoxelParticle _particle = null;
	private Vector3 offset_;
	private VoxelStruct mirrorVoxel_ = null;
	private VoxelStruct worldVoxel_ = null;
	void mirrorBuilding(){
		mirrorVoxel_ = MagicaVoxelFormater.ReadFromFile(_voxFile).vs;
		var data = VoxelBuilderHelper.Struct2DataInCache(mirrorVoxel_);
		offset_ = data.offset;


		MeshFilter filter = VoxelBuilderHelper.Data2Filter(data);
		VoxelBuilder.FilterAddRenderer (filter, _material);
		filter.name = "Object";
		filter.transform.SetParent (_mirror.transform);
		filter.transform.localEulerAngles = Vector3.zero;
		filter.transform.localPosition = offset_;
		filter.gameObject.layer = _mirror.gameObject.layer;

	}
	GameObject add_ = null;
	void worldBuilding(){
		worldVoxel_ = MagicaVoxelFormater.ReadFromFile(_diffFile).vs;



		VoxelStruct add = new VoxelStruct ();
		VoxelStruct left = new VoxelStruct ();

		HashSet<VectorInt3> set = new HashSet<VectorInt3> ();
		foreach (var data in mirrorVoxel_.datas) {
			set.Add (data.pos);
		}

		foreach (var data in worldVoxel_.datas) {
			if (set.Contains (data.pos)) {
				left.datas.Add (data);
			} else {
				add.datas.Add (data);
			}
		}


		var addVoxel = VoxelBuilderHelper.Struct2DataInCache(add);
		MeshFilter filter = VoxelBuilderHelper.Data2Filter(addVoxel);
		VoxelBuilder.FilterAddRenderer (filter, _material);
		filter.name = "Add";
		add_ = filter.gameObject;
		filter.transform.SetParent (_offset.transform);
		filter.transform.localEulerAngles = Vector3.zero;
		filter.transform.localPosition = offset_;
		filter.gameObject.layer = _offset.gameObject.layer;




		var leftVoxel = VoxelBuilderHelper.Struct2DataInCache(left);
		MeshFilter lfilter = VoxelBuilderHelper.Data2Filter(leftVoxel);
		VoxelBuilder.FilterAddRenderer (lfilter, _material);
		lfilter.name = "Left";
		lfilter.transform.SetParent (_offset.transform);
		lfilter.transform.localEulerAngles = Vector3.zero;
		lfilter.transform.localPosition = offset_;
		lfilter.gameObject.layer = _offset.gameObject.layer;

		_particle.transform.localPosition =  offset_;
		_particle.data = add;

	}
	void differentBuilding(){

		var diff = VoxelStruct.Different (worldVoxel_, mirrorVoxel_);
		var data = VoxelBuilderHelper.Struct2DataInCache(VoxelStruct.Create(diff,Color.white));
		//var add = VoxelStruct.Create (diff, vox1);
		MeshFilter filter = VoxelBuilderHelper.Data2Filter(data);
		VoxelBuilder.FilterAddRenderer (filter, _diffMaterial);
		filter.name = "Object";
		filter.transform.SetParent (_diff.transform);
		filter.transform.localEulerAngles = Vector3.zero;
		filter.transform.localPosition = offset_;
		filter.gameObject.layer = _diff.gameObject.layer;
		VoxelBuilder.FilterAddCollider (filter);
		Different different = filter.gameObject.AddComponent<Different> ();
	}
	void Awake () {
		mirrorBuilding ();
		worldBuilding ();
		differentBuilding ();



	

	
	
	
	}
	//public 
	public Task slected(){
		TaskSet ts = new TaskSet ();
		ts.push (particle ());
		ts.push (flicker ());
		return ts;
	}
	private Task flicker(){
		TaskCircle tc = new TaskCircle ();
		TaskWait tw1 = new TaskWait (0.1f);
		int n = 0;
		TaskManager.PushFront (tw1, delegate() {
			n++;
			if(n > 2){
				tc.forceQuit();
			}
			_offset.SetActive (false);

			for(int i = 0; i<_mirror.transform.childCount; ++i){
				_mirror.transform.GetChild(i).gameObject.layer = this.gameObject.layer;
			}
			//_mirror.gameObject.layer = this.gameObject.layer;	
		});
		TaskWait tw2 = new TaskWait (0.1f);
		TaskManager.PushFront (tw2, delegate() {
			add_.SetActive(false);
			_offset.SetActive (true);

			for(int i = 0; i<_mirror.transform.childCount; ++i){
				_mirror.transform.GetChild(i).gameObject.layer = this._mirror.layer;
			}
			//_mirror.gameObject.layer = this._mirror.layer;	

		});
		tc.push (tw1);
		tc.push (tw2);

		return tc;


	}
	private Task particle(){
		
		Task task = new Task ();
		TaskManager.PushBack (task, delegate() {
			ParticleSystem ps = _particle.gameObject.GetComponent<ParticleSystem> ();
			_particle.gameObject.SetActive (true);
			ps.Play ();

			for(int i = 0; i<_mirror.transform.childCount; ++i){
				_mirror.transform.GetChild(i).gameObject.layer = this.gameObject.layer;
			}

		//	ps.Stop();
		});
		return task;

	}

}
