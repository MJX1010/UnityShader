Shader "学习Shader/剔除/CullingBack" {
	
	SubShader
	{
		pass
		{
			//设置顶点光照
			Material
			{
				Emission(0.3,0.3,0.3,0.3)
				Diffuse(1,1,1,1)
			}

			//开启光照
			Lighting On

			//不绘制面向观察者的面
			Cull Front
		}
	}
}
