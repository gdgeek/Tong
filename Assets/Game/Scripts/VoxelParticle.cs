using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	public class VoxelParticle : MonoBehaviour {
		
		public ParticleSystem _ps = null;
		private ParticleSystem.Particle[] particles_ = null;
		private List<Color> colors_;
		private VoxelStruct data_;
	
		public VoxelStruct data{
			set{ 
				data_ = value;
			}

		}
		public void Start() {
			_ps = GetComponent<ParticleSystem>();
			_ps.maxParticles = data_.datas.Count;
		
			particles_ = new ParticleSystem.Particle[_ps.maxParticles];

		}

		void LateUpdate() {
			var count = _ps.GetParticles(particles_);
			for (var i = 0; i < count; i++) {
				if (particles_[i].color == Color.black) {

					particles_[i].color = data_.datas[i].color;
					var pos = data_.datas [i].pos;
					particles_[i].position = new Vector3(pos.x,pos.y,pos.z);

					//particles_[i].position = new Vectordata_.datas[i].pos;
				}
			}
			_ps.SetParticles(particles_, count);
		}



	}
}