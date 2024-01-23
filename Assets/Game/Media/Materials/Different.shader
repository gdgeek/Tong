Shader "Different" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,0.25)
   _Amount ("Extrusion Amount", Range(-1,1)) = 0.5

}

SubShader {
	Tags {"Queue"="Transparent"}
	LOD 200

	CGPROGRAM
	#pragma surface surf Lambert alpha vertex:vert

    

	struct Input {
		float2 uv_MainTex;
	};

	fixed4 _Color;
	float _Amount;
	void vert (inout appdata_full v) {
  		v.vertex.xyz += v.normal * _Amount;
	}
	void surf (Input IN, inout SurfaceOutput o) {
		o.Albedo = _Color.rgb;
		o.Alpha = (0.2+sin(_Time.z*2)/3) * _Color.a;
	}
ENDCG
}

}

