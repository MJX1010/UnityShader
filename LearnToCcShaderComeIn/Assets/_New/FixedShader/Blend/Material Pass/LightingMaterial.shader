
/*顶点光照 材质*/

Shader "学习Shader/LightingMaterial" {
	Properties {

		//主颜色
		_Color ("Color", Color) = (1,1,1,1)
		
		//反射高光颜色
		_SpecColor("SpecColor", Color) = (1,1,1,1)
		
		//光泽度颜色 == 自发光颜色 效果类似
		_Emission("Emission", Color) = (0,0,0,0)

		//光泽度
		_Shininess("Shininess",Range(0.01,1)) = 0.7 

		_MainTex("BaseTex",2D) = ""{}
	}

	SubShader 
	{
		pass
		{
			
			//设置材质
			//设置顶点光照值
			Material
			{
				//可配置的漫反射和环境光
				Diffuse[_Color]
				Ambient[_Color]
				//光泽度
				Shininess[_Shininess]
				//高光
				Specular[_SpecColor]
				//光泽度颜色
				Emission[_Emission]
			}

			//开启光照
			Lighting On
			//开启  独立  镜面反射
			SeparateSpecular On

			//设置纹理并进行纹理混合
			SetTexture[_MainTex]
		    {
				//在SetTexture中被定义的纹理颜色  texture
				//来自光照计算的颜色或者当它绑定时的顶点颜色  primary
				//两者相乘
				//分离了  透明度 和 颜色 的混合
				//对RGB颜色做乘  然后 对Alpha透明度 相乘
				Combine texture * primary DOUBLE, texture * primary
			}
		}

	}

}
