Shader "学习Shader/混合/BaseBlend" {
	Properties {
		_MainTex("TexToBlend", 2D) = "black"{ }
		_Color("MainColor", Color) = (1,1,1,0)
	}
	SubShader {

	//	Tags{ "Queue"="Transparent" }
			Tags{"RenderType"="Opaque"}
		Pass
		{

			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}
			Lighting On

			//进行混合
			//对计算后产生的颜色乘以帧缓冲区源颜色的值
			//对屏幕已存在的颜色清空
			Blend One OneMinusDstColor

			//SetTexture[_MainTex]{ combine texture }

			SetTexture[_MainTex]
			{
				//使颜色属性进入混合器
				constantColor[_Color]
				//使用纹理的alpha通道插值混合顶点颜色
				combine constant lerp(texture) previous

			}
		}
	}
	//FallBack "Diffuse"
}
