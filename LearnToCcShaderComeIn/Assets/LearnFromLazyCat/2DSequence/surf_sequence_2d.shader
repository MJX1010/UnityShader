/***************************************************************

#Surface Shader#

function: # UV animation #

UV 动画实现原理 ：
首先明确 GPU 的并行执行能力
1、根据当前时间(_Time)乘上自定义速度(_Speed)得到 用于计算的总时间 t
2、在表面着色器中，根据输入的主贴图UV值(uv)，按照序列帧的水平(_SizeX)和竖直(_SizeY)方向进行划分，得到一个单位UV区域 cellUV
3、用 t / _SizeX 得到 商 作为 行 索引 ， t / _SizeX 得到  列 索引

created by dayelongshe

***************************************************************/

Shader "Learn_Shader/surf_sequence_2d" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base(RGB) Trans(A)", 2D) = "white" {}
		_SizeX("Column 序列帧图片行数", float) = 1
		_SizeY("Row 序列帧图片列数",float) = 4
		_Speed("Play Speed",float) = 150
	}
	SubShader {
		
			//一般序列帧都是 png 带透明通道图，可以当做半透明对象处理
			//使用半透明标配的标签来声明
			Tags{ "Queue" = "Transparent"
			"IgnoreProjector" = "true"
			"RenderType" = "Transparent"
			"LightMode" = "ForwardBase" }
			ZWrite Off
			Blend SrcAlpha oneMinusSrcAlpha

			CGPROGRAM

#pragma surface surf Lambert alpha

		//declare some parameters that receiving from scene panel
		fixed4 _Color;
		sampler2D _MainTex;
		uniform fixed _SizeX;
		uniform fixed _SizeY;
		uniform fixed _Speed;

		//get uv data for input struct
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			
			//获取当前时间，用作索引增量，计算当前索引
			//_Time.y（自该场景加载后经过的时间）* _Speed = 模拟的时间
			float time = floor(_Time.y * _Speed);//取整
			//计算行列索引
			//商作为行索引，余数为列索引
			float rowIndex = floor(time / _SizeX);
		    float columnIndex = fmod(time, _SizeX);
			//float columnIndex = time - rowIndex * _SizeY;
			
			//按行列数平分单元格，得到单元格对应的uv
			float2 cellUV = float2(IN.uv_MainTex.x / _SizeX, IN.uv_MainTex.y / _SizeY);
			//单元格增量
			float deltaX = 1 / _SizeX;
			float deltaY = 1 / _SizeY;
			//根据列索引 偏移单元格uv（从左往右）
			cellUV.x += columnIndex * deltaX;
			//根据行索引 偏移单元格uv 
			//Tip:（Unity 纹理坐标的竖直方向的顺序，从下往上逐渐增大）
			//序列帧纹理的播放顺序（从上至下播放），此时坐标偏移用减法
			cellUV.y -= rowIndex * deltaY;//小于O时会自动置为0

			/*上述根据单元格偏移UV 可替换成*/
			/*float2 cellUV = IN.uv_MainTex + float2(columnIndex, -rowIndex);
		    cellUV.x /= _SizeX;
			cellUV.y /= _SizeY;*/

			float4 c = tex2D(_MainTex, cellUV) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}
