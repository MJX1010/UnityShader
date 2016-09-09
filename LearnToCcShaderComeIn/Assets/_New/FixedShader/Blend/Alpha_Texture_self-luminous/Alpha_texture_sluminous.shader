
/*纹理Alpha与自发光混合可调色版*/
/*纹理的Alpha通道与自发光相混合*/

Shader "学习Shader/Alpha_texture_sluminous" {
	Properties {
		
		_MainTex ("基础纹理 (RGB)-自发光（A） ", 2D) = "red" {}
		
		_EmissionColor("自发光（RGB）-自发光（A）",Color) = (1,1,1,1)
	}
	SubShader
	{
		//固定函数着色器
		pass
		{
			Material
			{
				Diffuse(1,1,1,1)
				Ambient(1,1,1,1)
			}

			Lighting On

			//使用纹理的Alpha通道来插值混合颜色（1，1，1，1）
			//可调色自发光面板

			//阶段一：纹理的Alpha值在顶点颜色和纯白色之间混合
			SetTexture[_MainTex]
		    {
				//constantColor(1,1,1,1)
				constantColor[_EmissionColor]

				//使用源2的透明度通道值在源3和源1中进行插值，
				//注意插值是反向的：当透明度值是1是使用源1，透明度为0时使用源3

				combine constant lerp(texture) previous //Constant是被ConstantColor定义的颜色
			}

			//和纹理相乘

			//阶段二：乘入纹理的RGB通道
			SetTexture[_MainTex]
			{
				combine previous * texture //previous表示前一个SetTexture处理后的结果
			}
		} //注意 没有 ；
	}
}
