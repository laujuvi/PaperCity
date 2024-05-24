// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "KitchenFloor-Shader"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_wood_floor_disp_4k("wood_floor_disp_4k", 2D) = "white" {}
		_Metalic("Metalic", Float) = 0
		_Smoothness("Smoothness", Float) = 0
		_Darkness("Darkness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _wood_floor_disp_4k;
		uniform float4 _wood_floor_disp_4k_ST;
		uniform sampler2D _TextureSample0;
		uniform float _Darkness;
		uniform float _Metalic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_wood_floor_disp_4k = i.uv_texcoord * _wood_floor_disp_4k_ST.xy + _wood_floor_disp_4k_ST.zw;
			o.Normal = tex2D( _wood_floor_disp_4k, uv_wood_floor_disp_4k ).rgb;
			float3 ase_worldPos = i.worldPos;
			o.Albedo = ( tex2D( _TextureSample0, ( (ase_worldPos).xz * 0.25 ) ) * _Darkness ).rgb;
			o.Metallic = _Metalic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
41;92;1560;474;1139.206;159.0909;1;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-1369.064,-16.48731;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;6;-1020.748,179.1964;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;0.25;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;4;-1087.276,35.74024;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-840.7477,61.19644;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-672.7007,-19.16195;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;7b191b8009a9a784a9c2d4420db677b0;244a43d9d394e5c4dbf358a75f78ed64;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-362.2061,124.9091;Inherit;False;Property;_Darkness;Darkness;4;0;Create;True;0;0;0;False;0;False;0;0.5647059;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-190.2063,286.9091;Inherit;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-197.2063,205.9091;Inherit;False;Property;_Metalic;Metalic;2;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-738.6788,313.4447;Inherit;True;Property;_wood_floor_disp_4k;wood_floor_disp_4k;1;0;Create;True;0;0;0;False;0;False;-1;d5539ddc339c1c54c8decb4bf3f7c24e;d5539ddc339c1c54c8decb4bf3f7c24e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-179.2061,-51.09094;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;173,-19;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;KitchenFloor-Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;1;1;5;0
WireConnection;10;0;1;0
WireConnection;10;1;11;0
WireConnection;0;0;10;0
WireConnection;0;1;7;0
WireConnection;0;3;9;0
WireConnection;0;4;8;0
ASEEND*/
//CHKSM=F221760C27E8D8F956DD0EAFEDA42FE3D2EC511C