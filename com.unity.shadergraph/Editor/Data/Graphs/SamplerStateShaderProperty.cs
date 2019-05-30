// using System;
// using UnityEditor.Graphing;
// using UnityEngine;

// namespace UnityEditor.ShaderGraph
// {
//     class SamplerStateShaderProperty : ShaderProperty<TextureSamplerState>
//     {
//         public SamplerStateShaderProperty()
//         {
//             displayName = "SamplerState";
//             value = new TextureSamplerState();
//         }

// #region Name
//         public override string referenceName => $"{concreteShaderValueType.ToShaderString()}_{GuidEncoder.Encode(guid)}_{value.filter}_{value.wrap}";
// #endregion

// #region Type
//         public override PropertyType propertyType => PropertyType.SamplerState;
// #endregion

// #region Capabilities
//         public override bool isBatchable => false;
//         public override bool isExposable => false;
//         public override bool isRenamable => false;
// #endregion

// #region ShaderValue
//         public override string GetPropertyDeclarationString(string delimiter = ";")
//         {
//             return $"SAMPLER({referenceName}){delimiter}";
//         }

//         public override string GetPropertyAsArgumentString()
//         {
//             return $"SamplerState {referenceName}";
//         }
// #endregion

// #region Utility
//         public override AbstractMaterialNode ToConcreteNode()
//         {
//             return new SamplerStateNode() 
//             {
//                 filter = value.filter,
//                 wrap = value.wrap
//             };
//         }

//         public override PreviewProperty GetPreviewMaterialProperty()
//         {
//             return default(PreviewProperty);
//         }

//         public override ShaderProperty Copy()
//         {
//             var copied = new SamplerStateShaderProperty();
//             copied.displayName = displayName;
//             copied.overrideReferenceName = overrideReferenceName;
//             copied.value = value;
//             return copied;
//         }
// #endregion
//     }
// }
