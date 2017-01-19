Shader "学习Shader/顶点动画/VertexMoving"
{

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
			
			v2f vert (appdata v)
			{
				float angle = length(v.vertex) * _SinTime.x;
				float x = cos(angle)*v.vertex.x + sin(angle)*v.vertex.z;
				float z = cos(angle)*v.vertex.z - sin(angle)*v.vertex.x;
				v.vertex.x = x;
				v.vertex.z = z;

				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = fixed4(0,1,1,1);
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
