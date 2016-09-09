
/*顶点光照 + 纹理Alpha 自发光混合*/

Shader "学习Shader/Alpha_Tex_Emission_VertexLighting" {
	Properties {
		
		_MainTex ("基础纹理 (RGB)-自发光（A） ", 2D) = "red" {}
		
		_IlluminColor("自发光（RGB）-自发光（A）",Color) = (1,1,1,1)
		_MainColor("主颜色", Color) = (1,1,1,0)
		_SpecColor("高光颜色", Color) = (1,1,1,1)
		_EmissionColor("自发光颜色", Color) = (0,0,0,0)
		_Shininess("光泽度",Range(0.01,1)) = 0.7
	}
	SubShader
	{
		//固定函数着色器
		pass
		{
			//【1】设置顶点光照值
			Material
			{
				Diffuse[_MainColor]
				Ambient[_MainColor]
				Shininess[_Shininess]
				Specular[_SpecColor]
				Emission[_EmissionColor]
			}
			//【2】开启光照
			Lighting On

			//【3】  开启独立镜面反射 
			//**********************让顶点光照和纹理颜色可以结合********************
			SeparateSpecular On

			//使用纹理的Alpha通道来插值混合颜色（1，1，1，1）
			//可调色自发光面板

			//【4】阶段一：纹理的Alpha值在顶点颜色和纯白色之间混合
			SetTexture[_MainTex]
		    {
				//constantColor(1,1,1,1)
				constantColor[_IlluminColor]

				//使用源2的透明度通道值在源3和源1中进行插值，
				//注意插值是反向的：当透明度值是1是使用源1，透明度为0时使用源3

				combine constant lerp(texture) previous //Constant是被ConstantColor定义的颜色
			}

			//和纹理相乘

			//【5】阶段二：乘入纹理的RGB通道
			SetTexture[_MainTex]
			{
				combine previous * texture //previous表示前一个SetTexture处理后的结果
			}

		    //【6】乘以顶点纹理
			SetTexture[_MainTex]
			{
				combine previous * primary DOUBLE,previous * primary //primary表示来自光照计算的颜色或是当它绑定时的顶点颜色
			}

		} //注意 没有 ；
	}
}
