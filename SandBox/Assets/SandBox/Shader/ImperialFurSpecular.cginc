		#include "UnityCG.cginc"
		#include "AutoLight.cginc"
 
		sampler2D _FurSpecularMap;
		sampler2D _SpecGlossMap;
		
		#include "IFCommonVariables.cginc"

		fixed3 _FurSpecularColor;
		fixed3 _SpecularColor;

		void surfSkin (Input IN, inout SurfaceOutputStandardSpecular o) {
			if (_SkinMode == 0) {
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _FurColor;				
				o.Albedo = c.rgb;
			
				if (_UseFurSecondMap != 0) {
					float4 m = tex2D(_FurSpecularMap, IN.uv_MainTex);
					o.Specular = m.rgb;
					o.Smoothness = m.a; 
				} else {
					o.Specular = _FurSpecularColor;
					o.Smoothness = _FurGlossiness;
				}
			} else {
				fixed4 c = tex2D (_SkinTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
			
				if (_UseSkinSecondMap != 0) {
					float4 m = tex2D(_SpecGlossMap, IN.uv_MainTex);
					o.Specular = m.rgb;
					o.Smoothness = m.a; 
				} else {
					o.Specular = _SpecularColor;
					o.Smoothness = _Glossiness;
				}
				
				o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_MainTex)) * _BumpScale;
			}
		}

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			#include "IFCommonSurface.cginc"
			
			if (_UseFurSecondMap != 0) {
				float4 m = tex2D(_FurSpecularMap, IN.uv_MainTex);
				o.Specular = m.rgb;
				o.Smoothness = m.a * n.r; 
			} else {
				o.Specular = _FurSpecularColor;
				o.Smoothness = _FurGlossiness * n.r;
			}          				
		}
		
		#include "IFCommonVert.cginc"
		
