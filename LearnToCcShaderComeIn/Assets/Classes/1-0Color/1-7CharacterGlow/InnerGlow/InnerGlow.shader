// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:547,x:32798,y:32708,varname:node_547,prsc:2|custl-5682-OUT;n:type:ShaderForge.SFN_Tex2d,id:4049,x:32254,y:32738,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8df37bc00f54bd343b1a09b3b3327528,ntxv:0,isnm:False|UVIN-664-UVOUT;n:type:ShaderForge.SFN_Add,id:5682,x:32620,y:32948,varname:node_5682,prsc:2|A-4049-RGB,B-2523-OUT;n:type:ShaderForge.SFN_Fresnel,id:7775,x:32254,y:33160,varname:node_7775,prsc:2|NRM-6154-OUT,EXP-6668-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6668,x:32089,y:33329,ptovrint:False,ptlb:FresnelExp,ptin:_FresnelExp,varname:_FresnelExp,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2523,x:32436,y:32970,varname:node_2523,prsc:2|A-520-RGB,B-7775-OUT,C-7012-OUT,D-6327-OUT;n:type:ShaderForge.SFN_Color,id:520,x:32254,y:32970,ptovrint:False,ptlb:GlowColor,ptin:_GlowColor,varname:_GlowColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4632353,c3:0.4632353,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:7012,x:32254,y:33307,ptovrint:False,ptlb:GlowIntensity,ptin:_GlowIntensity,varname:_GlowIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ToggleProperty,id:6327,x:32254,y:33429,ptovrint:False,ptlb:InnerGlow,ptin:_InnerGlow,varname:_InnerGlow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_TexCoord,id:664,x:32080,y:32745,varname:node_664,prsc:2,uv:0;n:type:ShaderForge.SFN_NormalVector,id:6154,x:32089,y:33160,prsc:2,pt:False;proporder:6327-520-4049-6668-7012;pass:END;sub:END;*/

Shader "Custom/InnerGlow" {
    Properties {
        [MaterialToggle] _InnerGlow ("InnerGlow", Float ) = 1
        _GlowColor ("GlowColor", Color) = (1,0.4632353,0.4632353,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _FresnelExp ("FresnelExp", Float ) = 1
        _GlowIntensity ("GlowIntensity", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _FresnelExp;
            uniform float4 _GlowColor;
            uniform float _GlowIntensity;
            uniform fixed _InnerGlow;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 finalColor = (_MainTex_var.rgb+(_GlowColor.rgb*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_FresnelExp)*_GlowIntensity*_InnerGlow));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
