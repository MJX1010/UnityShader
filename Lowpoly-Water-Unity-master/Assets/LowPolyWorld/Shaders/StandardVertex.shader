 Shader "Custom/StandardVertex" {
     Properties {
         _Color ("ColorTint", Color) = (1,1,1)
         _Glossiness ("Smoothness", Range(0,1)) = 0.5
         _Metallic ("Metallic", Range(0,1)) = 0.0
     }
     SubShader {
         Tags { "RenderType"="Opaque" }
         LOD 200
         
         CGPROGRAM
         #pragma surface surf Standard vertex : vert fullforwardshadows
         #pragma target 3.0
         struct Input {
             float4 vertexColor; // Vertex color stored here by vert() method
         };
         
         /*struct v2f {
           float4 pos : SV_POSITION;
           fixed3 color : COLOR;
         };*/
 
         void vert (inout appdata_full v, out Input o)
         {
             UNITY_INITIALIZE_OUTPUT(Input,o);//Initializes the variable name of given type to zero.
             o.vertexColor = v.color; // Save the Vertex Color in the Input for the surf() method
         }

		 fixed3 _Color;
         half _Glossiness;
         half _Metallic;
         
 
         void surf (Input IN, inout SurfaceOutputStandard o) 
         {
             // Albedo comes from a texture tinted by color
             o.Albedo = _Color.rgb * IN.vertexColor; // Combine normal color with the vertex color
             // Metallic and smoothness come from slider variables
             o.Metallic = _Metallic;
             o.Smoothness = _Glossiness;
         }
         ENDCG
     } 
     FallBack "Diffuse"
 }