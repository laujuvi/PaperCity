// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "KitchenFloor-Shader"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_AmbienmtOcclution("Ambienmt Occlution", 2D) = "white" {}
		_Darkness("Darkness", Range( 0 , 1)) = 0
		_Color0("Color 0", Color) = (0,0,0,0)
		_Roughtness("Roughtness", Float) = 0
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
			float3 worldPos;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _TextureSample0;
		uniform float4 _Color0;
		uniform float _Darkness;
		uniform float _Roughtness;
		uniform sampler2D _AmbienmtOcclution;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 temp_output_5_0 = ( (ase_worldPos).xz * 0.25 );
			o.Normal = tex2D( _Normal, temp_output_5_0 ).rgb;
			o.Albedo = ( ( tex2D( _TextureSample0, temp_output_5_0 ) * _Color0 ) * _Darkness ).rgb;
			o.Smoothness = _Roughtness;
			o.Occlusion = tex2D( _AmbienmtOcclution, temp_output_5_0 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
186;143;1560;474;915.0505;-49.31004;1.3;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-1582.867,237.4033;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ComponentMaskNode;4;-1301.079,289.6309;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1234.551,433.0871;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;0.25;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-1054.55,315.0871;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-672.7007,-19.16195;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;7b191b8009a9a784a9c2d4420db677b0;244a43d9d394e5c4dbf358a75f78ed64;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;14;-364.46,-242.3654;Inherit;False;Property;_Color0;Color 0;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.7264151,0.526898,0.3255162,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;2.831505,-151.351;Inherit;False;Property;_Darkness;Darkness;4;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-101.3335,-55.80762;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;-670.4993,181.0089;Inherit;True;Property;_Normal;Normal;1;0;Create;True;0;0;0;False;0;False;-1;d5539ddc339c1c54c8decb4bf3f7c24e;d5539ddc339c1c54c8decb4bf3f7c24e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-669.4584,368.1126;Inherit;True;Property;_Roughness;Roughness;2;0;Create;True;0;0;0;False;0;False;-1;d5539ddc339c1c54c8decb4bf3f7c24e;d5539ddc339c1c54c8decb4bf3f7c24e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-667.2302,553.8228;Inherit;True;Property;_AmbienmtOcclution;Ambienmt Occlution;3;0;Create;True;0;0;0;False;0;False;-1;d5539ddc339c1c54c8decb4bf3f7c24e;d5539ddc339c1c54c8decb4bf3f7c24e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;313.7902,-55.14152;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;16;227.6496,250.81;Inherit;False;Property;_Roughtness;Roughtness;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;600.8737,106.4167;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;KitchenFloor-Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;1;1;5;0
WireConnection;15;0;1;0
WireConnection;15;1;14;0
WireConnection;7;1;5;0
WireConnection;12;1;5;0
WireConnection;13;1;5;0
WireConnection;10;0;15;0
WireConnection;10;1;11;0
WireConnection;0;0;10;0
WireConnection;0;1;7;0
WireConnection;0;4;16;0
WireConnection;0;5;13;0
ASEEND*/
//CHKSM=95D12FF6BAF4150F2382A8B317E5067AB8990E79