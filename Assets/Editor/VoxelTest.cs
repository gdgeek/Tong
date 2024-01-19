using UnityEngine;
using UnityEditor;
//using NUnit.Framework;
using GDGeek;
using System.IO;
using GDGeek;
using System.Collections.Generic;
using System.Text;
using System;


public class VoxelTest : MonoBehaviour {
	


	public void Voxel()
	{
		GDGeek.VoxelStruct from = new GDGeek.VoxelStruct();
		GDGeek.VoxelStruct to = new GDGeek.VoxelStruct();
		var diff = GDGeek.VoxelStruct.Different (from, to);
		var ret = VoxelStruct.Create (diff, Color.white);

	}

}
