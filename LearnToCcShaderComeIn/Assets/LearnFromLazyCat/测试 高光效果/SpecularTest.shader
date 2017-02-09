/***************************************************************

#Vertex & Fragment Shader#

function: #  #
 
created by dayelongshe

***************************************************************/

Shader "LearnShader_Specular/SpecularTest"
{
		//属性  
		Properties
		{
			_Diffuse("Diffuse", Color) = (1, 1, 1, 1)
			_Specular("Specular", Color) = (1, 1, 1, 1)
			_Gloss("Gloss", Range(1.0, 256)) = 20
		}

		//子着色器    
		SubShader
			{
				Pass
				{
					//定义Tags  
					Tags{ "LightingMode" = "ForwardBase" }

					CGPROGRAM
					//引入头文件  
#include "Lighting.cginc"  

					//定义函数  
#pragma vertex vert  
#pragma fragment frag  

					//定义Properties中的变量  
					fixed4 _Diffuse;
					fixed4 _Specular;
					float _Gloss;

					//定义结构体：应用阶段到vertex shader阶段的数据  
					struct a2v
					{
						float4 vertex : POSITION;
						float3 normal : NORMAL;
					};

					//定义结构体：vertex shader阶段输出的内容  
					struct v2f
					{
						float4 pos : SV_POSITION;
						float3 worldNormal : NORMAL;
						float3 viewDir : TEXCOORD1;
					};

					//顶点shader  
					v2f vert(a2v v)
					{
						v2f o;
						o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
						//法线转化到世界空间  
						o.worldNormal = normalize(mul(v.normal, (float3x3)_World2Object));
						//顶点位置转化到世界空间   
						float3 worldPos = mul(_Object2World, v.vertex).xyz;
							//计算视线方向（相机位置 - 像素对应位置）  
							o.viewDir = _WorldSpaceCameraPos - worldPos;
						return o;
					}

					//片元shader  
					fixed4 frag(v2f i) : SV_Target
					{
						//环境光  
						fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * _Diffuse;
						//世界空间下光线方向  
						fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
						//需要再次normalize  
						fixed3 worldNormal = normalize(i.worldNormal);
						//计算Diffuse  
						fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));
						//normalize  
						fixed3 viewDir = normalize(i.viewDir);
						//计算半角向量（光线方向 + 视线方向，结果归一化）  
						fixed3 halfDir = normalize(worldLight + viewDir);
						//计算Specular（Blinn-Phong计算的是）  
						fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(halfDir, worldNormal)), _Gloss);
						//结果为diffuse + ambient + specular  
						fixed3 color = diffuse + ambient + specular;
						return fixed4(color, 1.0);
					}
						ENDCG
				}
			}
			//前面的Shader失效的话，使用默认的Diffuse  
			FallBack "Diffuse"
}
