using UnityEngine;
using System.Collections;
namespace GDGeek{
	
	public class VoxelBuilder{
		public static VoxelGeometry.MeshData Struct2Data(VoxelStruct vs){
			VoxelProduct product = new VoxelProduct();
			VoxelData[] datas = vs.datas.ToArray ();
			Build.Run (new VoxelData2Point (datas), product);
			Build.Run (new VoxelSplitSmall (new VectorInt3(8, 8, 8)), product);
			Build.Run (new VoxelMeshBuild (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			Build.Run (new VoxelRemoveFace (), product);
			Build.Run (new VoxelRemoveSameVertices (), product);
			var data = product.getMeshData ();
			return data;
		}

		public static Mesh Data2Mesh(VoxelGeometry.MeshData data){//创建mesh

			Mesh mesh = new Mesh();
			mesh.name = "ScriptedMesh";
			mesh.SetVertices (data.vertices);
			mesh.SetColors (data.colors);

			mesh.SetUVs (0, data.uvs);
			mesh.SetTriangles(data.triangles, 0);
			mesh.RecalculateNormals();

			return mesh;
		}
		public static MeshFilter Mesh2Filter(Mesh mesh){
			GameObject obj = new GameObject();
			MeshFilter filter = obj.AddComponent<MeshFilter>();
			filter.sharedMesh = mesh;

			return filter;
		}

		public static MeshRenderer FilterAddRenderer(MeshFilter filter, Material material){
			MeshRenderer renderer = filter.gameObject.AddComponent<MeshRenderer>();
			renderer.material = material;
			return renderer;
		}

		public static MeshCollider FilterAddCollider(MeshFilter filter){
			MeshCollider collider = filter.gameObject.AddComponent<MeshCollider>();
			collider.sharedMesh = filter.mesh;
			return collider;

		}

		
	}
	public class VoxelBuilderHelper{
		public static VoxelGeometry.MeshData Struct2DataInCache(VoxelStruct vs){
            return VoxelBuilder.Struct2Data(vs);
            /*
			string md5 = MagicaVoxelFormater.GetMd5 (vs);
			VoxelGeometry.MeshData data = VoxelDirectorFile.LoadFromFile (VoxelDirectorFile.GetKey(md5));

			if(data == null){
				data = VoxelBuilder.Struct2Data (vs);
				VoxelDirectorFile.SaveToFile (VoxelDirectorFile.GetKey(md5), data);
			}
			return data;
            */
        }
		public static MeshFilter Struct2Filter(VoxelStruct vs){
			var data = Struct2DataInCache (vs);
			var mesh = VoxelBuilder.Data2Mesh (data);
			var filter = VoxelBuilder.Mesh2Filter (mesh);
			return filter;
		}

		public static MeshFilter Data2Filter(VoxelGeometry.MeshData data){
			var mesh = VoxelBuilder.Data2Mesh (data);
			var filter = VoxelBuilder.Mesh2Filter (mesh);
			return filter;
		}
/*		public static AddFilterToGameObject(Transform parent, Transform son, VoxelGeometry.MeshData data){
			
		}*/
	}
}
