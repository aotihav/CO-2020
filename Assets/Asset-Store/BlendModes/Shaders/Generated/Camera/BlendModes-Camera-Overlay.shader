

Shader "Hidden/BlendModes/Camera/Overlay" 
{
	Properties 
	{
		_MainTex ("Screen", 2D) = "" {}
		_BLENDMODES_OverlayTexture("Overlay Texture", 2D) = "white" {}
        _BLENDMODES_OverlayColor("Overlay Color", Color) = (1,1,1,1)
	}
	
	SubShader 
	{
		ZTest Always 
		Cull Off 
		ZWrite Off
		ColorMask RGB 
		
		Pass 
		{  
			CGPROGRAM
			
			#include "UnityCG.cginc"
			#include "../../BlendModesCG.cginc"

			#pragma multi_compile BLENDMODES_MODE_DARKEN BLENDMODES_MODE_MULTIPLY BLENDMODES_MODE_COLORBURN BLENDMODES_MODE_LINEARBURN BLENDMODES_MODE_DARKERCOLOR BLENDMODES_MODE_LIGHTEN BLENDMODES_MODE_SCREEN BLENDMODES_MODE_COLORDODGE BLENDMODES_MODE_LINEARDODGE BLENDMODES_MODE_LIGHTERCOLOR BLENDMODES_MODE_OVERLAY BLENDMODES_MODE_SOFTLIGHT BLENDMODES_MODE_HARDLIGHT BLENDMODES_MODE_VIVIDLIGHT BLENDMODES_MODE_LINEARLIGHT BLENDMODES_MODE_PINLIGHT BLENDMODES_MODE_HARDMIX BLENDMODES_MODE_DIFFERENCE BLENDMODES_MODE_EXCLUSION BLENDMODES_MODE_SUBTRACT BLENDMODES_MODE_DIVIDE BLENDMODES_MODE_HUE BLENDMODES_MODE_SATURATION BLENDMODES_MODE_COLOR BLENDMODES_MODE_LUMINOSITY
			#pragma vertex ComputeVertex
			#pragma fragment ComputeFragment
			
			sampler2D _MainTex;
			half4 _MainTex_TexelSize;
			half4 _UV_Transform = half4(1, 0, 0, 1);
			BLENDMODES_OVERLAY_VARIABLES

			struct VertexInput
            {
                float4 Vertex : POSITION;
                float2 TexCoord : TEXCOORD0;
                
            };

			struct VertexOutput 
			{
				float4 ScreenPos : SV_POSITION;
				float2 ScreenUV[2] : TEXCOORD0;
				BLENDMODES_OVERLAY_TEX_COORD(2)
			};
			
			VertexOutput ComputeVertex(VertexInput vertexInput)
			{
				VertexOutput vertexOutput;
				
				vertexOutput.ScreenPos = UnityObjectToClipPos(vertexInput.Vertex);
				
				vertexOutput.ScreenUV[0] = float2(
					dot(vertexInput.TexCoord.xy, _UV_Transform.xy),
					dot(vertexInput.TexCoord.xy, _UV_Transform.zw)
				);
		
				#if UNITY_UV_STARTS_AT_TOP
				if(_MainTex_TexelSize.y < 0.0)
					vertexOutput.ScreenUV[0].y = 1.0 - vertexOutput.ScreenUV[0].y;
				#endif

				BLENDMODES_TRANSFORM_OVERLAY_TEX(vertexOutput.ScreenUV[0], vertexOutput)
		
				vertexOutput.ScreenUV[1] =  vertexInput.TexCoord.xy;	
				return vertexOutput;
			}
			
			fixed4 ComputeFragment(VertexOutput vertexOutput) : SV_Target
			{
                #if UNITY_SINGLE_PASS_STEREO
                vertexOutput.ScreenUV[1] = TransformStereoScreenSpaceTex(vertexOutput.ScreenUV[1], vertexOutput.ScreenPos.w);
                #endif 
				fixed4 color = tex2D(_MainTex, vertexOutput.ScreenUV[1]);

				BLENDMODES_BLEND_PIXEL_OVERLAY(color, vertexOutput)

				return color; 
			}
			
			ENDCG
		}
	}

	Fallback off
}
