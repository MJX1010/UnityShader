Shader "学习Shader/剔除/CullingBack_2" {
	
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
		
		Pass
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

			SetTexture[_MainTex]
			{
				Combine Primary * Texture
			}
		}

		//需要新开一个通道
		/*【通道二】*/
		Pass
		{
			Color(0, 0, 1, 1)
			Cull Front
		}
		
	}
}
