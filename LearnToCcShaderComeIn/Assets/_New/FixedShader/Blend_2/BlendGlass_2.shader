Shader "学习Shader/混合/BlendGlass_2" {
	Properties{
		_Color("MainColor", Color) = (1, 1, 1, 1)
		_MainTex("Base (RGB) Transparency(A)", 2D) = "white"{}
		_Reflections("Base (RGB) Gloss(A)", Cube) = "skybox"{ TexGen CubeReflect }
	}
	SubShader{

		Tags{ "Queue" = "Transparent" }

		//添加一个通道
		Pass
		{
			//Blend One One

			Blend SrcAlpha OneMinusSrcAlpha

			Material
			{
				Diffuse[_Color]
			}

			Lighting On

			SetTexture[_MainTex]
			{
				//combine texture * primary double,texture * primary

				combine primary * texture double , primary * texture
			}
		}

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

	//-------------------------------【属性】-----------------------------------------
	//Properties
	//{
	//	_Color("Main Color", Color) = (1, 1, 1, 1)
	//	_MainTex("Base (RGB) Transparency (A)", 2D) = "white" {}
	//	_Reflections("Base (RGB) Gloss (A)", Cube) = "skybox" { TexGen CubeReflect }
	//}

	//	//--------------------------------【子着色器】----------------------------------
	//	SubShader
	//{
	//	//-----------子着色器标签----------
	//	Tags{ "Queue" = "Transparent" }

	//	//----------------通道1--------------
	//	Pass
	//	{
	//		Blend SrcAlpha OneMinusSrcAlpha

	//		Material
	//		{
	//			Diffuse[_Color]
	//		}

	//		Lighting On
	//		SetTexture[_MainTex] {
	//				// combine texture * primary double, texture * primary
	//				combine primary * texture double, primary * texture
	//			}
	//	}

	//	//----------------通道2--------------
	//	Pass
	//				{
	//					//进行纹理混合
	//					Blend One One

	//					//设置材质
	//					Material
	//					{
	//						Diffuse[_Color]
	//					}

	//					//开光照
	//					Lighting On

	//					//和纹理相乘
	//					SetTexture[_Reflections]
	//						{
	//							combine texture
	//								Matrix[_Reflection]
	//						}
	//				}
	//}
}
