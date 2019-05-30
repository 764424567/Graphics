// using System;
// using System.Collections.Generic;
// using System.Text;
// using System.Linq;
// using UnityEditor.Graphing;
// using UnityEngine;

// namespace UnityEditor.ShaderGraph
// {
//     enum FloatType { Default, Slider, Integer, Enum }

//     public enum EnumType { Enum, CSharpEnum, KeywordEnum, }

//     [Serializable]
//     [FormerName("UnityEditor.ShaderGraph.FloatShaderProperty")]
//     class Vector1ShaderProperty : ShaderProperty<float>
//     {
//         public Vector1ShaderProperty()
//         {
//             displayName = "Vector1";
//         }

// #region Type
//         public override PropertyType propertyType => PropertyType.Vector1;
// #endregion

// #region Capabilities
//         public override bool isBatchable => true;
//         public override bool isExposable => true;
//         public override bool isRenamable => true;
// #endregion

// #region PropertyBlock
//         private string enumTagString
//         {
//             get
//             {
//                 switch(enumType)
//                 {
//                     case EnumType.CSharpEnum:
//                         return $"[Enum({m_CSharpEnumType.ToString()})]";
//                     case EnumType.KeywordEnum:
//                         return $"[KeywordEnum({string.Join(", ", enumNames)})]";
//                     default:
//                         string enumValuesString = "";
//                         for (int i = 0; i < enumNames.Count; i++)
//                         {
//                             int value = (i < enumValues.Count) ? enumValues[i] : i;
//                             enumValuesString += (enumNames[i] + ", " + value + ((i != enumNames.Count - 1) ? ", " : ""));
//                         }
//                         return $"[Enum({enumValuesString})]";
//                 }
//             }
//         }

//         public override string GetPropertyBlockString()
//         {
//             switch(floatType)
//             {
//                 case FloatType.Slider:
//                     return $"{hideTagString} {referenceName}(\"{displayName}\", Range({NodeUtils.FloatToShaderValue(m_RangeValues.x)}, {NodeUtils.FloatToShaderValue(m_RangeValues.y)})) = {NodeUtils.FloatToShaderValue(value)}";
//                 case FloatType.Integer:
//                     return $"{hideTagString} {referenceName}(\"{displayName}\", Int) = {NodeUtils.FloatToShaderValue(value)}";
//                 case FloatType.Enum:
//                     return $"{hideTagString}{enumTagString} {referenceName}(\"{displayName}\", Float) = {NodeUtils.FloatToShaderValue(value)}";
//                 default:
//                     return $"{hideTagString} {referenceName}(\"{displayName}\", Float) = {NodeUtils.FloatToShaderValue(value)}";
//             }
//         }
// #endregion

// #region Options
//         [SerializeField]
//         private FloatType m_FloatType = FloatType.Default;

//         public FloatType floatType
//         {
//             get => m_FloatType;
//             set => m_FloatType = value;
//         }

//         [SerializeField]
//         private Vector2 m_RangeValues = new Vector2(0, 1);

//         public Vector2 rangeValues
//         {
//             get => m_RangeValues;
//             set => m_RangeValues = value;
//         }

//         private EnumType m_EnumType = EnumType.Enum;

//         public EnumType enumType
//         {
//             get => m_EnumType;
//             set => m_EnumType = value;
//         }
    
//         private Type m_CSharpEnumType;

//         public Type cSharpEnumType
//         {
//             get => m_CSharpEnumType;
//             set => m_CSharpEnumType = value;
//         }

//         private List<string> m_EnumNames = new List<string>();
//         private List<int> m_EnumValues = new List<int>();

//         public List<string> enumNames
//         {
//             get => m_EnumNames;
//             set => m_EnumNames = value;
//         }


//         public List<int> enumValues
//         {
//             get => m_EnumValues;
//             set => m_EnumValues = value;
//         }
// #endregion

// #region Utility
//         public override AbstractMaterialNode ToConcreteNode()
//         {
//             switch (m_FloatType)
//             {
//                 case FloatType.Slider:
//                     return new SliderNode { value = new Vector3(value, m_RangeValues.x, m_RangeValues.y) };
//                 case FloatType.Integer:
//                     return new IntegerNode { value = (int)value };
//                 default:
//                     var node = new Vector1Node();
//                     node.FindInputSlot<Vector1MaterialSlot>(Vector1Node.InputSlotXId).value = value;
//                     return node;
//             }
//         }

//         public override PreviewProperty GetPreviewMaterialProperty()
//         {
//             return new PreviewProperty(propertyType)
//             {
//                 name = referenceName,
//                 floatValue = value
//             };
//         }

//         public override ShaderProperty Copy()
//         {
//             var copied = new Vector1ShaderProperty();
//             copied.displayName = displayName;
//             copied.value = value;
//             copied.floatType = floatType;
//             copied.rangeValues = rangeValues;
//             copied.enumType = enumType;
//             copied.enumNames = enumNames;
//             copied.enumValues = enumValues;
//             return copied;
//         }
// #endregion
//     }
// }
