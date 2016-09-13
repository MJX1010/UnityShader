Shader "学习Shader/V&F/VF_RimLighting"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BumpMap("BumpMap", 2D) = "bump"{}
		_Color("Color", color) = (1,1,1,1)
		_RimColor("Rim Color", color) = (0,0,0,1)
		_RimPower("Rim Power",range(0.5,8.0))=3.0
	}
	SubShader
	{
		//设置当前SubShader渲染类型Opaque，不透明
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			//设置通道名，前向渲染
			Name "ForwardBase"

			//设置光照模式||LightMode Forward
			Tags
			{
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
            #include "AutoLight.cginc"

            #pragma target 3.0

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
