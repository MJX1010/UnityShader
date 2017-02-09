Shader "Custom/AlphaHand" {
	Properties {
         _MainTex ("Base (RGB)", 2D) = "white" {} // Regular object texture 
        _Color ("Main Color", Color) = (1, 1, 1, 1)
         _Alpha ("Alpha", Range(0,1)) = 1.0 // How close does the player have to be to make object visible
     }
     SubShader {
		 Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
         
		 //Extra pass that renders to depth buffer only
		 Pass
		 {
		 ZWrite On
		 ColorMask 0 //设置 0 时 关闭所有颜色通道的渲染
		 }
		 
		 Pass {
         //Zwrite on
         Blend SrcAlpha OneMinusSrcAlpha
         LOD 200
         Cull back
     
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
 
         // Access the shaderlab properties
         uniform sampler2D _MainTex; 
         uniform float _Alpha;
         uniform float4 _Color;
         // Input to vertex shader
         struct vertexInput {
             float4 vertex : POSITION;
             float4 texcoord : TEXCOORD0;
          };
         // Input to fragment shader
          struct vertexOutput {
             float4 pos : SV_POSITION;
             float4 position_in_world_space : TEXCOORD0;
             float4 tex : TEXCOORD1;
          };
          
          // VERTEX SHADER
          vertexOutput vert(vertexInput input) 
          {
             vertexOutput output; 
             output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
             output.position_in_world_space = input.vertex;
             output.tex = input.texcoord;
             return output;
          }
  
          // FRAGMENT SHADER
         float4 frag(vertexOutput input) : COLOR 
         {
         	float4 col = tex2D(_MainTex, float2(input.tex.xy));
         	col.rgb = _Color.rgb;
         	col.a = col.a*1.5+_Alpha;
            return col;
          }
 
         ENDCG
         }
     } 
     //FallBack "Diffuse"
}

