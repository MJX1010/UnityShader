Shader "VAG/Water River" {
    Properties {
       _Color ("Water Color (RGB) Transparency (A)", COLOR) = (1, 1, 1, 0.5)
       _BumpMap ("Normal Map 1", 2D) = "white" {}
       _BumpMap2 ("Normal Map 2", 2D) = "white" {}
       _FlowMap ("Flow Map", 2D) = "white" {}
       _NoiseMap ("Noise Map", 2D) = "black" {}
       _Cube ("Reflection Cubemap", Cube) = "white" { TexGen CubeReflect }
	   _FoamTex ("FoamTexture", 2D) = "gray" {}
       _Cycle ("Cycle", float) = 1.0
       _Speed ("Speed", float) = 0.05
       _SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
       _Shininess ("Shininess", Range (0.01, 2)) = 0.078125
       _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
       _Displacement ("_Displacement", Range (0.0, 2.0)) = 1.0
       _DisplacementTiling ("_DisplacementTiling", Range (0.1, 4.0)) = 1.0
       _DisSpeed("_DisSpeed", Range (0.0, 10.0)) = 0.8
    }
 
    // Common water code that will be used in all CGPROGRAMS below
	CGINCLUDE
	#include "UnityCG.cginc"

	struct v2f_aniOnly {
		float4 vertex : POSITION;
	};
 
	sampler2D _MainTex;
 
	float _Displacement;
	float _DisplacementTiling;   
	float _DisSpeed;
 
	half3 vertexOffsetObjectSpace(appdata_full v) {
		return v.normal.xyz * sin((length(v.vertex.zy + v.color.rgb-0.5) + _Time.w * _DisSpeed )*_DisplacementTiling) * _Displacement * 1.5 * v.color.a;          
	}
 
	v2f_aniOnly vert_onlyAnimation(inout appdata_full v)
	{
		v2f_aniOnly o;
 
		v.vertex.xyz += vertexOffsetObjectSpace(v);       
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);          
 
		return o;
	}
	ENDCG
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 500
		Cull Off
		

			CGPROGRAM

			#pragma surface surf BlinnPhong 
			#pragma vertex vert_onlyAnimation    
			#pragma target 3.0
 
			float4 _Color;
			sampler2D _BumpMap;
			sampler2D _BumpMap2;
			samplerCUBE _Cube;
			sampler2D _FlowMap;
			sampler2D _NoiseMap;
			float _Cycle;
			float _Speed;
			float _Shininess;
			float4 _ReflectColor;
			sampler2D _FoamTex;
 
 
 
			struct Input {
			float2 uv_BumpMap;
			float2 uv_FlowMap;
			float3 worldRefl;
			float3 worldNormal;
			float3 viewDir;
			float2 uv_FoamTex;
			INTERNAL_DATA
			};
 
			//normal water shader
			void surf (Input IN, inout SurfaceOutput o) {
				float3 flowDir = tex2D(_FlowMap, IN.uv_FlowMap) * 2 - 1;
				flowDir *= _Speed;
				flowDir.y *= -1; // A dirty fix because I didn't want to rearrange my scene, you should be able to remove it
				float3 noise = tex2D(_NoiseMap, IN.uv_FlowMap);
 
				float phase = _Time[1] / _Cycle + noise.r * 0.5f;
				float f = frac(phase);
 
				half3 n1 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap + flowDir.xy * frac(phase + 0.5f)));
				half3 n2 = UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap + flowDir.xy * f));
 
				if (f > 0.5f)
					f = 2.0f * (1.0f - f);
				else
					f = 2.0f * f;
 
				o.Normal = lerp(n1, n2, f);
				o.Alpha = _Color.a;
				o.Gloss = 1;
				o.Specular = _Shininess;
	
				fixed4 reflcol = texCUBE (_Cube, WorldReflectionVector(IN, o.Normal));
				o.Albedo = _Color.rgb;
				//o.Albedo *= tex2D(_FoamTex, IN.uv_FoamTex).rgb;
				o.Emission = reflcol.rgb * _ReflectColor.rgb;
			}
			ENDCG
		
		
    }
    FallBack "Reflective/Bumped Specular"
}
