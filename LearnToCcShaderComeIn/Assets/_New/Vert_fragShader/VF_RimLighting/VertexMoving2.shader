Shader "学习Shader/顶点动画/VertexMoving2"
{
	Properties{
		//_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Color("Main Color", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};
			
			fixed4 _Color;
			v2f vert (appdata v)
			{
				/*float angle = length(v.vertex) * _SinTime.x;
				float x = cos(angle)*v.vertex.x + sin(angle)*v.vertex.z;
				float z = cos(angle)*v.vertex.z - sin(angle)*v.vertex.x;
				v.vertex.x = x;
				v.vertex.z = z;*/
				v.vertex.y += 0.2 *sin((v.vertex.x + v.vertex.z) + _Time.y);
				v.vertex.y += 0.3 *sin((v.vertex.x - v.vertex.z) +_Time.w);

				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				//o.color = fixed4(0,1,1,1);
				o.color = fixed4(v.vertex.y, v.vertex.y, v.vertex.y, 0.5)*_Color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return i.color;
			}
			ENDCG
		}
	}
}
