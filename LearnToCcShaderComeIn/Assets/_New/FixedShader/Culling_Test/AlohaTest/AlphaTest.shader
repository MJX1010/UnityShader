Shader "学习Shader/AlphaTest" {
	Properties {
	
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		
		_Color ("主颜色", Color) = (1,1,1,0)  
        _SpecColor ("高光颜色", Color) = (1,1,1,1)  
        _Emission ("光泽颜色", Color) = (0,0,0,0)  
        _Shininess ("光泽度", Range (0.01, 1)) = 0.7  

		_CutOff("Alpha透明度阈值",Range(0,1)) = 0.5
	}
	SubShader
	{
		pass
		{
			/*【1】*/
			//透明度大于60%的像素
			//注意  请使用一张带有透明通道的纹理贴图
			//对象只会在透明度大于0.6 时显示
			//AlphaTest Greater 0.6

			/*【2】*/
			AlphaTest Greater[_CutOff]

			Material   
            {  
                Diffuse [_Color]  
                Ambient [_Color]  
                Shininess [_Shininess]  
                Specular [_SpecColor]  
                Emission [_Emission]  
            }  

			Lighting On

			SetTexture[_MainTex]
			{
				combine texture * primary
			}
		}
	}
}
