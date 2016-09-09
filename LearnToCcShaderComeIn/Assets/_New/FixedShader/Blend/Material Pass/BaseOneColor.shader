Shader "学习Shader/BaseOneColor" {

	Properties
	{
		_MainColor("MainColor",Color) = (1,0.1,0.5,1)
	}
	SubShader {
		
		pass{
			//设置纯色
			Color(0,0,1,0)
			//设置材质
			Material
			{
				//将漫反射和环境光设为一致
				//其中的A值可以忽略
				//shader默认为黑色，可以注释代码
				/*Diffuse(0.9,0.5,0.4)              //使用括号
				Ambient(0.9, 0.5, 0.4)*/

				//可调节的漫反射光和环境光反射颜色
				Diffuse[_MainColor]                 //使用方括号
				Ambient[_MainColor]
			}
			Lighting On//放在Pass内或者SubShader中
		}
	}

}
