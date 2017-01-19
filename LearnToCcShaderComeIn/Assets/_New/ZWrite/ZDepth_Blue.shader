Shader "学习Shader/深度/ZDepth_Blue" {
	Properties { //properties
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color("Main Color", Color) = (1, 1, 1, 1)
	}
	SubShader {   //subshader不区分大小写 
		//Tags { "RenderType"="Opaque" "Queue"="Geometry"}//可并行书写
		Tags{"RenderType"="opaque"}//Opaque不区分大小写
		LOD 200

		/*Test*/  //注意 Tags此处又不需要区分大小写了
		tags{"queue"="geometry+300"}
		//zwrite off
		//ztest off
		/*Test*/
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert  

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;//又区分大小写了,

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
