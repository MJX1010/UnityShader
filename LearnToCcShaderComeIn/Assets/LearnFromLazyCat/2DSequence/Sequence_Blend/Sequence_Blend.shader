/***************************************************************

#Vertex & Fragment Shader#

function: # Sequence Blend 多贴图混合序列帧动画 #
 
created by dayelongshe

***************************************************************/

Shader "Learn_Shader/Sequence_Blend"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "white" {}
		_BlendTex("BlendTex", 2D) = "white"{}
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		//AlphaTest Greater 0.1

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _BlendTex;
			sampler2D _MainTex;
			float4 _MainTex_ST;//用于调节Unity 面板中 的 offset 和 scale 
			float _SpeedX;
			float _SpeedY;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//move main texture by x
				float uv_x = i.uv.x + -0.5 * _Time;
				float2 uv_mainTex = float2(uv_x,i.uv.y);
				// sample the texture
				fixed4 col_mainTex = tex2D(_MainTex,uv_mainTex);
				
				//move blend texture 
				uv_x = i.uv.x + -1 * _Time;
				float uv_y = i.uv.y + -0.2 * _Time;
				float2 uv_blendTex = float2(uv_x, uv_y);
				fixed4 tex_blendDepth = tex2D(_BlendTex, uv_blendTex);

				fixed4 col_blendTex = float4(1, 1, 1, 0) * tex_blendDepth.x;

				return lerp(col_mainTex, col_blendTex,0.5f);
			}
			ENDCG
		}
	}
}
