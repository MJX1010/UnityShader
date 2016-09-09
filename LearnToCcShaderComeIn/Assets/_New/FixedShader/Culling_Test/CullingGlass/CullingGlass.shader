Shader "学习Shader/剔除/CullingGlass" {
	
	Properties
	{
		_Color("MainColor", Color) = (1,1,1,0)
		_SpecColor("SpecularColor", Color) = (1,1,1,1)
		_Emission("EmissionColor", Color) = (0,0,0,0)
		_Shininess("Shininess",Range(0.01,1)) = 0.7
		_MainTex("MainTex", 2D) = "white"{}
	}

	SubShader
	{
		//设置顶点光照
		Material
		{
			Diffuse[_Color]
			Ambient[_Color]
			Shininess[_Shininess]
			Specular[_SpecColor]
			Emission[_Emission]
		}

		//开启光照
		Lighting On

		//开启独立镜面反射
		SeparateSpecular On

		//开启透明度混合（alpha blending）
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			//如果对象是凸型，那么总是离镜头离的比前面更远
			Cull Front
			SetTexture[_MainTex]
			{
				Combine Primary * Texture
			}
		}

		//需要新开一个通道
		/*【通道二】*/
		Pass
		{
			Cull Back
			SetTexture[_MainTex]
			{
				Combine Primary * Texture
			}
		}
		
	}
}
