Shader "ColaShader/Scroll2LayersAditive-addtiveBlend-ClipPlane" {

	Properties{

		_MainTex("Base layer (RGB)", 2D) = "white" {}      //前层图

		_DetailTex("2nd layer (RGB)", 2D) = "white" {}      //后层图

		_ScrollX("Base layer Scroll speed X", Float) = 1.0  // 前层图X方向的速度

			_ScrollY("Base layer Scroll speed Y", Float) = 0.0  // 前层图Y方向的速度

			_Scroll2X("2nd layer Scroll speed X", Float) = 1.0  // 后层图X方向的速度

			_Scroll2Y("2nd layer Scroll speed Y", Float) = 0.0  // 后层图Y方向的速度

			_SineAmplX("Base Layer sine amplitude X", Float) = 0.5 //振幅
			_SineAmplY("Base Layer sine amplitude Y", Float) = 0.5
			_SineFreqX("Base Layer sine freq X", Float) = 10 //频率
			_SineFreqY("Base Layer sine freq Y", Float) = 10

			_SineAmpl2X("2nd Layer sine amplitude X", Float) = 0.5 //振幅
			_SineAmpl2Y("2nd Layer sine amplitude Y", Float) = 0.5
			_SineFreq2X("2nd Layer sine freq X", Float) = 10 //频率
			_SineFreq2Y("2nd Layer sine freq Y", Float) = 10

			_AMultiplier("Layer Multiplier", Float) = 0.5

			_TintColor("Tint color", Color) = (1, 1, 1, 1)

			_PlanePoint("Plane Point", Vector) = (0, 0, 0, 0)
		    _PlaneNormal("Plane Normal", Vector) = (0, -1, 0, 0)

	}

	SubShader{

			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

			Blend One Zero

			ZWrite Off

			Cull Off

			Lighting Off Fog{ Mode Off }

			LOD 100

			CGINCLUDE

#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON

#include "UnityCG.cginc"

			sampler2D _MainTex;

			sampler2D _DetailTex;

			float4 _MainTex_ST;

			float4 _DetailTex_ST;

			float _ScrollX;

			float _ScrollY;

			float _Scroll2X;

			float _Scroll2Y;

			float _SineAmplX;
			float _SineAmplY;
			float _SineFreqX;
			float _SineFreqY;

			float _SineAmpl2X;
			float _SineAmpl2Y;
			float _SineFreq2X;
			float _SineFreq2Y;

			float _AMultiplier;

			float4 _TintColor;

			Vector _PlanePoint;
			Vector _PlaneNormal;

			struct v2f {

				float4 pos : SV_POSITION;

				float2 uv : TEXCOORD0;

				float2 uv2 : TEXCOORD1;

				fixed4 color : TEXCOORD2;

				float3 worldPos : TEXCOORD3;
			};

			v2f vert(appdata_full v)

			{

				v2f o;

				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				o.uv = TRANSFORM_TEX(v.texcoord.xy, _MainTex) + frac(float2(_ScrollX, _ScrollY) * _Time);  //frac    返回输入值的小数部分。

				o.uv2 = TRANSFORM_TEX(v.texcoord.xy, _DetailTex) + frac(float2(_Scroll2X, _Scroll2Y) * _Time);

				o.uv.x += sin(_Time * _SineFreqX) * _SineAmplX;
				o.uv.y += sin(_Time * _SineFreqY) * _SineAmplY;

				o.uv2.x += sin(_Time * _SineFreq2X) * _SineAmpl2X;
				o.uv2.y += sin(_Time * _SineFreq2Y) * _SineAmpl2Y;

				o.color = v.color * _TintColor * _TintColor.a;

				o.color.xyz *= _AMultiplier;

				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				return o;

			}

			ENDCG

				Pass{

				CGPROGRAM

#pragma vertex vert

#pragma fragment frag

#pragma fragmentoption ARB_precision_hint_fastest

				fixed4 frag(v2f i) : SV_Target

				{

					fixed4 o;

					fixed4 tex = tex2D(_MainTex, i.uv);

					fixed4 tex2 = tex2D(_DetailTex, i.uv2);

					o = (tex + tex2) * i.color;

					//Clip with custom plane (clip on plane normal direction)
					half dist = ((i.worldPos.x * _PlaneNormal.x) + (i.worldPos.y * _PlaneNormal.y) + (i.worldPos.z * _PlaneNormal.z)
						- (_PlanePoint.x * _PlaneNormal.x) - (_PlanePoint.y * _PlaneNormal.y) - (_PlanePoint.z * _PlaneNormal.z) )
						/ sqrt(pow(_PlaneNormal.x, 2) + pow(_PlaneNormal.y, 2) + pow(_PlaneNormal.z, 2));

					clip(dist);

					return o;

				}

					ENDCG

			}

		}

}