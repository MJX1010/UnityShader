		sampler2D _FurSpecularMap;
		sampler2D _SpecGlossMap;

		#include "IFCommonVariables.cginc"

		half _FurSpecular;
		half _Specular;

		void surfSkin (Input IN, inout SurfaceOutput o) {
			_SpecColor = float4(1,1,1,1);
			if (_SkinMode == 0) {
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _FurColor;
				o.Albedo = c.rgb;
			
				if (_UseFurSecondMap != 0) {
					float4 m = tex2D(_FurSpecularMap, IN.uv_MainTex);
					o.Specular = m.r;
					o.Gloss = m.a; 
				} else {
					o.Specular = _FurSpecular;
					o.Gloss = _FurGlossiness;
				}
			} else {
				fixed4 c = tex2D (_SkinTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
			
				if (_UseSkinSecondMap != 0) {
					float4 m = tex2D(_SpecGlossMap, IN.uv_MainTex);
					o.Specular = m.r;
					o.Gloss = m.a; 
				} else {
					o.Specular = _Specular;
					o.Gloss = _Glossiness;
				}

				o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_MainTex)) * _BumpScale;
			}
		}

		void surf (Input IN, inout SurfaceOutput o) {	
			#include "IFCommonSurface.cginc"
          	
          	_SpecColor = float4(1,1,1,1);
			if (_UseFurSecondMap != 0) {
				float4 m = tex2D(_FurSpecularMap, IN.uv_MainTex);
				o.Specular = m.r;
				o.Gloss = m.a * n.r; 
			} else {
				o.Specular = _FurSpecular;
				o.Gloss = _FurGlossiness * n.r;
			}  	
		}
		
		#include "IFCommonVert.cginc"
		
