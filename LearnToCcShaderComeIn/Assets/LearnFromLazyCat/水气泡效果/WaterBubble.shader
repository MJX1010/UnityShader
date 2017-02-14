
/***************************************************************

#Vertex & Fragment Shader#

function: #  #
 
created by dayelongshe

***************************************************************/

Shader "Learn_Shader/WaterBubble"
{
	Properties
	{
		_MainTex ("Base Layer(RGB)", 2D) = "white" {}
		_DetailTex("Second Layer(RGB)", 2D) = "white"{}
		_TintColor("Tint Color", Color) = (1, 1, 1, 1)
		_AMultiplier("Layer Multiplier",float) = 0.5
		_ScrollX("Base Scroll X", float) = 1.0
		_ScrollY("Base Scroll Y", float) = 1.0
		_SndScrollX("Second Scroll X",float) = 1.0
		_SndScrollY("Second Scroll Y",float) = 1.0
	}
	SubShader
		{
			Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
			LOD 100
			Blend One One
			ZWrite Off
			Lighting Off
			Fog{ Mode Off }

				CGINCLUDE
				// make fog work
				//#pragma multi_compile_fog
				//顶点片元着色器中调用unity内置的lightmap 和 light probes
				#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON 
				

				#include "UnityCG.cginc"

				//struct appdata
				//{
				//    float4 vertex : POSITION;
				//	float4 texcoord : TEXCOORD0;
				//	fixed4 color : COLOR;
				//};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float2 uv2 : TEXCOORD1;
					//UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
					fixed4 color : TEXCOORD2;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

				sampler2D _DetailTex;
				float4 _DetailTex_ST;

				float4 _TintColor;

				float _AMultiplier;

				float _ScrollX;
				float _ScrollY;
				float _SndScrollX;
				float _SndScrollY;

				v2f vert(appdata_full v)
				{
					v2f o;
					o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord.xy, _MainTex) + frac(float2(_ScrollX, _ScrollY) * _Time); //frac 返回输入值的小数部分，或者每个元素的小数部分
					//UNITY_TRANSFER_FOG(o,o.vertex);
					o.uv2 = TRANSFORM_TEX(v.texcoord.xy, _DetailTex) + frac(float2(_SndScrollX, _SndScrollY) * _Time);
					o.color = v.color * _TintColor * _TintColor.a;
					o.color.xyz *= _AMultiplier;

					return o;
				}
			 
				ENDCG

				Pass
				{
					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma fragmentoption ARB_precision_hint_fastest
					fixed4 frag(v2f i) : SV_Target
					{
						// sample the texture
						fixed4 col = tex2D(_MainTex, i.uv);
						fixed4 col2 = tex2D(_DetailTex, i.uv2);

						fixed4 output;

						output = (col + col2) * i.color;

						// apply fog
						//UNITY_APPLY_FOG(i.fogCoord, col);
						return output;
					}

					ENDCG
			    }
		}

}
