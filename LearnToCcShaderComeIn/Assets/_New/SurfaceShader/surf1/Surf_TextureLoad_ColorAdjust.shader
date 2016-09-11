Shader "学习Shader/Surface/Surf_TextureLoad_ColorAdjust" {
	Properties {
		_ColorTint ("Tint", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Bump (Normal)", 2D) = ""{}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200//Level of Detail 在Quality设置中可设置
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert finalcolor:setcolor    //finalcolor:ColorFunction - 自定义的最终颜色函数(final color function)

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0//相当于DirectD 9 上的shader Model 3.0

		//变量声明
		sampler2D _MainTex;
		sampler2D _BumpMap;//凹凸纹理
		fixed4 _ColorTint;

		//输入结构
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;//凹凸纹理UV
			//fixed4 color : Color()语义常用于 顶点片元着色器
		};

		void setcolor(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			color *= _ColorTint;//将自定义颜色值自乘
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			//纹理采样，并获取纹理的RGB颜色值
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			////从凹凸纹理中获取法线值（fixed4 -->  fixed3）
			//o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));

			
		}
		ENDCG
	}

	//备胎 回滚  普通漫反射
	FallBack "Diffuse"
}
