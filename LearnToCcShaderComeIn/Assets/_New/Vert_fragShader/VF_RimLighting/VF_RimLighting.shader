Shader "学习Shader/V&F/VF_RimLighting"
{
	/*下列shaderlab是基于Unity默认创建shader格式修改的*/
	//面板显示属性
	properties//Properties 大小写不区分
	{
		//漫反射纹理
		_TextureDiffuse("TextureDiffuse", 2D) = "white" {}
		//凹凸纹理
		_BumpMap("BumpMap", 2D) = "bump"{}
		//凹凸纹理程度
		_BumpMapPower("BumpMap Power",Range(0.0,5.0))=1.0//当为0时，法线纹理不会对光照产生影响
		//主颜色
		_MainColor("Main Color", color) = (1, 1, 1, 1)
			//边缘发光颜色
			_RimColor("Rim Color", color) = (0, 0, 0, 1)
			//边缘发光强度
			_RimPower("Rim Power", range(0.0, 36)) = 0.1
			//边缘发光强度系数
			_RimIntensity("Rim Intensity", Range(0.0, 100)) = 3
	}
	subshader//subshader 大小写不区分
		{
			//设置当前SubShader渲染类型Opaque，不透明
			Tags{ "RenderType" = "Opaque" }
			LOD 100
			pass//pass 大小写不区分
			{
				//设置通道名，前向渲染
				Name "ForwardBase"

				//设置光照模式||LightMode Forward
				//如果需要使用环境光等Unity内建光照，需要开启
				Tags
				{
					"LightMode" = "ForwardBase"
				}
				//开启CG着色器编程
				CGPROGRAM

				//【1】指定顶点和片段着色函数名
#pragma vertex vert
#pragma fragment frag
				// make fog work ， Unity自动生成的Fog编译指令
				//#pragma multi_compile_fog

				//【2】包含头文件，下列两个头文件，提供了顶点输入结构体和光照模型等
#include "UnityCG.cginc"
#include "AutoLight.cginc"

				//【3】指定Shader Model 3.0 
#pragma target 3.0

				//【4】顶点输入结构体，从应用通过CPU输入到GPU
				//为 UnityCG.cginc头文件内的预定义的宏
				/*一般输入结构体都是声明一些需要使用到的数据结构*/
				//语义不区分大小写
					struct appdata
					{
						//顶点坐标位置
						float4 vertex : POSITION;
						//法线向量坐标
						float3 normal : NORMAL;
						//一级纹理坐标
						float2 texcoord : TEXCOORD0;
						//切线向量
						float4 tangent : TANGENT;
					};

					//【5】顶点输出到片元着色器的结构体
					//vertex output to fragment
					struct v2f
					{
						//一级纹理坐标
						float4 uv : TEXCOORD0;
						//UNITY_FOG_COORDS(1)
						//像素位置
						float4 pos : SV_POSITION;
						//法线向量坐标，法线的声明及赋值放在frag中
						float3 normal : Normal;

						//世界空间中的坐标位置
						//使用TEXCOORD1语义的原因是：除了COLOR TEXCOORD NORMAL POSITION等对应特定类型的语义，
						//其他定义的变量可使用TEXCOODn(n=0~..)这个语义保证输入输出变量一致性
						float4 posWorld : Texcoord1;
						//为AutoLight.cginc中的预定义的宏，3 4指明了变量的存储位置
						//包含了阴影和光照衰减，其实就是对纹理进行采样并将结果乘以RGB
						LIGHTING_COORDS(3, 4)    //之后不需要加 ；号
					};

					//【6】变量声明
					//系统光照颜色
					uniform float4 _LightColor0;//

					//注意，uniform关键词是修饰CG变量和参数的一种修饰词，
					//仅仅提供一些关于变量的初始值是如何指定和存储的
					//uniform 在unity shader中可以省略
					uniform sampler2D _TextureDiffuse;
					//ST:SamplerTexture 声明了漫反射贴图的采样图（会进行uv运算）
					float4 _TextureDiffuse_ST;
					sampler2D _BumpMap;
					float4 _BumpMap_ST;
					float _BumpMapPower;

					float4 _MainColor;
					uniform float4 _RimColor;
					uniform float _RimPower;
					uniform float _RimIntensity;

					//【7】顶点着色函数
					v2f vert(appdata v)
					{
						//声明一个顶点输出结构对象
						v2f o;
						//填充该结构体
						//将输入纹理坐标赋值给输出纹理坐标
						//o.uv = v.texcoord;
						//将_MainTex和_BumpMap同时使用一组纹理坐标
						o.uv.xy = v.texcoord.xy * _TextureDiffuse_ST.xy + _TextureDiffuse_ST.zw;
						o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;

						//使用Unity内置定义好的（CGIncludes文件夹中）
						//UNITY_MATRIX_MVP变换矩阵进行变换，将模型顶点从模型坐标空间转到裁剪空间
						o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
						
						//使用float4将float3的Normal转到4维，在左乘一个世界转模型的变换矩阵
						//因为 _World2Object 是 
						/******** 向量转四维，补0，点转四维，补1  ********/
						o.normal = mul(float4(v.normal, 0), _World2Object).xyz;
						//o.normal = mul(_World2Object, float4(v.normal, 0)).xyz;//感觉左乘和右乘区别不大

						//获取模型顶点在世界空间中的位置坐标
						o.posWorld = mul(_Object2World, v.vertex);

						//UNITY_TRANSFER_FOG(o,o.vertex);
						return o;
					}

					//【8】片元/片段着色器，实现边缘发光主要部分
					fixed4 frag(v2f i) : SV_Target//也可使用Color，推荐使用SV_Target
					{
						// sample the texture
						//fixed4 col = tex2D(_MainTex, i.uv);
						// apply fog
						//UNITY_APPLY_FOG(i.fogCoord, col);


						//【8.1】方向参数准备
						//视角方向，视角到模型顶点的向量
						//_WorldSpaceCameraPos为UnityCG.cginc中头文件中包含
						//_WorldSpaceCameraPos为坐标点（注意下划线）
						float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
						//光照方向(_WorldSpaceLightPos0为经过计算的向量)
						float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
						//法线方向
						float3 normalection = normalize(i.normal);
						//法线纹理中存储的是把法线经过映射后得到的像素值，需要在给定的坐标上进行反映射（采样）
						float4 packedNormal = tex2D(_BumpMap, i.uv.zw);
						//UnpackNormal特定函数区分大小写，内置函数得到法线方向
						float3 tangentNormal = UnpackNormal(packedNormal);
						tangentNormal.xy *= _BumpMapPower;
						tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
						tangentNormal = normalize(tangentNormal);

						//【8.2】计算光照衰减
						//衰减值
						float attenuation = LIGHT_ATTENUATION(i);
						//衰减后的颜色值
						float3 attenColor = attenuation * (_LightColor0.x*tangentNormal.x + _LightColor0.y*tangentNormal.y + _LightColor0.z*tangentNormal.z);
							//【8.3】计算慢反射
							float NdotL = dot(normalection, lightDirection);
						//漫反射颜色 + 环境光颜色
						float3 Diffuse = max(0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz;
							//【8.4】准备自发光参数
							//计算边缘强度
						half Rim = 1.0 - max(0, dot(normalection, viewDirection));

						//计算出边缘自发光的强度
						float3 Emissive = _RimColor.rgb * pow(Rim, _RimPower) * _RimIntensity;
							//【8.5】在最终颜色重加入自发光颜色

							//最终颜色 = （漫反射 × 纹理颜色 × rgb自定义颜色）+自发光颜色
							//说明：漫反射颜色中包括了 环境光颜色
							//      纹理颜色需要将对纹理进行采样，即进行uv运算
							//      使用TRANSFORM_TEX进行采样运算，作用：拿纹理的uv去和材质球中的Tiling和Offset做对应，使之有效
							//      使用前必须先声明一个采样纹理对象的变量，如“_MainTex_ST”,注意添加“_ST”
							//      此时，如果在面板中修改对应uv的Tiling和Offset，可以得到对应UV变化的效果
							//      参考：http://blog.sina.com.cn/s/blog_471132920101dayd.html
							//     
							float3 finalColor = Diffuse * tex2D(_TextureDiffuse, TRANSFORM_TEX(i.uv, _BumpMap)).rgb * _MainColor.rgb + Emissive;

							//【8.6】返回最终的颜色
							return fixed4(finalColor, 1);
					}
						ENDCG
						//结束CG
			}
		}
		//上述Shader与GPU不兼容时  回滚 普通漫反射
		FallBack "Diffuse"
}

