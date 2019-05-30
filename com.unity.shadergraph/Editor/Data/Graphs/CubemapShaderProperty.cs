// using System;
// using System.Text;
// using UnityEditor.Graphing;
// using UnityEngine;

// namespace UnityEditor.ShaderGraph
// {
//     [Serializable]
//     class CubemapShaderProperty : ShaderProperty<SerializableCubemap>
//     {
//         public CubemapShaderProperty()
//         {
//             displayName = "Cubemap";
//             value = new SerializableCubemap();
//         }

// #region Type
//         public override PropertyType propertyType => PropertyType.Cubemap;
// #endregion

// #region Capabilities
//         public override bool isBatchable => false;
//         public override bool isExposable => true;
//         public override bool isRenamable => true;
// #endregion

// #region PropertyBlock
//         public string modifiableTagString => modifiable ? "" : "[NonModifiableTextureData]";

//         public override string GetPropertyBlockString()
//         {
//             return $"{hideTagString}{modifiableTagString}[NoScaleOffset] {referenceName}(\"{displayName}\", CUBE) = \"\" {{}}";
//         }
// #endregion

// #region ShaderValue
//         public override string GetPropertyDeclarationString(string delimiter = ";")
//         {
//             return $"TEXTURECUBE({referenceName}){delimiter} SAMPLER(sampler{referenceName}){delimiter}";
//         }

//         public override string GetPropertyAsArgumentString()
//         {
//             return $"TEXTURECUBE_PARAM({referenceName}, sampler{referenceName})";
//         }
// #endregion

// #region Options
//         [SerializeField]
//         private bool m_Modifiable = true;

//         public bool modifiable
//         {
//             get => m_Modifiable;
//             set => m_Modifiable = value;
//         }
// #endregion

// #region Utility
//         public override AbstractMaterialNode ToConcreteNode()
//         {
//             return new CubemapAssetNode { cubemap = value.cubemap };
//         }

//         public override PreviewProperty GetPreviewMaterialProperty()
//         {
//             return new PreviewProperty(propertyType)
//             {
//                 name = referenceName,
//                 cubemapValue = value.cubemap
//             };
//         }

//         public override ShaderProperty Copy()
//         {
//             var copied = new CubemapShaderProperty();
//             copied.displayName = displayName;
//             copied.value = value;
//             return copied;
//         }
// #endregion
//     }
// }
