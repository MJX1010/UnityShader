Shader "Masked/Mask" {
	
	SubShader {
		// Render the mask after regular geometry, but before masked geometry and
		// transparent things.
		
		Tags {"Queue" = "Geometry" }
		
		// Don't draw in the RGBA channels; just the depth buffer
		//设置颜色通道的写掩码（write mask） 语法： ColorMask RGB | A | 0  
		//设为0时，该Pass通道不写入任何颜色通道
		ColorMask 0 
		ZWrite On
		//ZTest Off
		//Lighting Off
		
		// Do nothing specific in the pass:
		
		Pass {}
	}
}