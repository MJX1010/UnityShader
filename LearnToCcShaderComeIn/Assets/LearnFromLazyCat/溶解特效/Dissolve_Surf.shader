/***************************************************************

#Surface Shader#

function: #  #
 
created by dayelongshe

***************************************************************/

Shader "Dissolve/Dissolve_Surf" {
	Properties 
	{
		_Color("MainColor", Color) = (1, 1, 1, 1)
		_Emission("Emission", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Metallic("Metallic", Range(0, 1)) = 0.0
		_Glossiness("Smoothness", Range(0, 1)) = 0.5

		_Amount("Amount(Dissolve)", Range(0, 1)) = 0.5
		_StartAmount("StartAmount(Dissolve)", float) = 0.1
		_Illuminate("Light Intensity(Dissolve)", Range(0, 0.999)) = 0.5
		
		_DissColor("Color(Dissolve)", Color) = (1, 1, 1, 1)
		_DissolveSrc("DissolveSrc", 2D) = "white" {}
		_ColorAnimate("ColorAnimate", vector) = (1, 1, 1, 1)
		
		_Alpha("Alpha", Range(0, 1)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

        #include "UnityCG.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 vertex : SV_POSITION;
		};

		struct VertexInput
		{
			float4 vertex :POSITION;
		};

		half4 _DissColor;
		half _Amount;
		half4 _ColorAnimate;
		half _Illuminate;
		half _StartAmount;
		fixed4 _Color;
		fixed4 _Emission;
		sampler2D _DissolveSrc;

		half _Glossiness;
		half _Metallic;

		half _Alpha;

		static half3 Color = float3(1, 1, 1);

		Input vert(VertexInput v)
		{
			Input i;
			i.vertex = mul(UNITY_MATRIX_MVP,v.vertex);
			return i;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			

			fixed3 col = c.rgb;
			float ClipTex = tex2D(_DissolveSrc, IN.uv_MainTex).r;
			float ClipAmount = ClipTex - _Amount/1.5;
			float Clip = 0;

			if (_Amount > 0)
			{
				//if (_Amount > 0.51)
				if (ClipAmount < 0) 
				{
					Clip = 1;
				}
				else
				{
					if (ClipAmount < _StartAmount)
					{
						if (_ColorAnimate.x == 0)
							Color.x = _DissColor.x;
						else
							Color.x = ClipAmount / _StartAmount;

						if (_ColorAnimate.y == 0)
							Color.y = _DissColor.y;
						else
							Color.y = ClipAmount / _StartAmount;

						if (_ColorAnimate.z == 0)
							Color.z = _DissColor.z;
						else
							Color.z = ClipAmount / _StartAmount;

						col = (col *((Color.x + Color.y + Color.z))* Color *((Color.x + Color.y + Color.z))) / (1 - _Illuminate);
					}
				}
			}

			if (Clip == 1)
			{
				clip(-0.1);
			}

			o.Albedo = col.rgb * _Color;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Emission = _Emission;
			//UNITY_OPAQUE_ALPHA(c.a);
			o.Alpha = c.a;
			//o.Alpha = _Color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
