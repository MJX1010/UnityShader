
/*Alpha 通道混合*/

Shader "学习Shader/Alpha_Texture_Blend" {
	Properties {
		
		_MainTex ("基础纹理 (RGBA)", 2D) = "white" {}
		_BlendTex("混合纹理（RGBA）", 2D) = "white"{}
	}
	SubShader
	{
		//固定函数着色器
		pass
		{
			//应用主纹理
			SetTexture[_MainTex]{combine texture}
			//使用相乘操作来进行Alpha纹理混合
			SetTexture[_BlendTex]{combine texture * previous}
		} //注意 没有 ；

	}
}
