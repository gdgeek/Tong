using UnityEngine;
using System.Collections;
using GDGeek;
public class GameData : MonoBehaviour {
	public class Pair{
		public VoxelStruct first;
		public VoxelStruct second;
		//public VoxelStruct different;
	}
	public TextAsset _voxFile = null;
	public TextAsset[] _diffFiles = null;
	private Pair data_ = null;
	public Pair data{
		get{

			Pair data = new Pair ();
			data.first = MagicaVoxelFormater.ReadFromFile(_voxFile).vs;
			data.second =  MagicaVoxelFormater.ReadFromFile(_diffFiles[Random.Range(0, _diffFiles.Length)]).vs;

			return data;
		}

	}
}
