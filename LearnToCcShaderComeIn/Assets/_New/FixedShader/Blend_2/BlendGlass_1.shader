Shader "学习Shader/混合/BlendGlass_1" {
	Properties {
		_Color("MainColor", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Transparency(A)", 2D) = "white"{}
		_Reflections("Base (RGB) Gloss(A)", Cube) = "skybox"{ TexGen CubeReflect }
	}
	SubShader{
		Tags{ "Queue"="Geometry" }
		Tags{ "RenderType" = "Opaque" }
		Pass
		{
			Blend One One

			Material
			{
				Diffuse[_Color]
			}

			Lighting On

			SetTexture[_Reflections]
			{
				combine texture
				Matrix[_Reflection]//****************不是_Reflections************
			}
		}
	}
}
