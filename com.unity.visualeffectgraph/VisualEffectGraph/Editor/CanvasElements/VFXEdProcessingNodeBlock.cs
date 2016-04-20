using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEditor.Experimental;
using UnityEditor.Experimental.Graph;
using Object = UnityEngine.Object;

namespace UnityEditor.Experimental
{
    internal class VFXEdProcessingNodeBlock : VFXEdNodeBlockDraggable
    {
        public VFXBlockModel Model { get { return m_Model; } }
        private VFXProperty[] Properties { get { return Model.Properties; } }

        private VFXBlockModel m_Model;

        public VFXEdProcessingNodeBlock(VFXBlock block, VFXEdDataSource dataSource) : base(dataSource)
        {
            m_Model = new VFXBlockModel(block);
            

            // For selection
            target = ScriptableObject.CreateInstance<VFXEdProcessingNodeBlockTarget>();
            (target as VFXEdProcessingNodeBlockTarget).targetNodeBlock = this;

            if (Properties != null && Properties.Length > 0)
            {
                int nbProperties = Properties.Length;
                m_Fields = new VFXEdNodeBlockParameterField[nbProperties];
                for (int i = 0; i < nbProperties; ++i)
                {
                    m_Fields[i] = new VFXEdNodeBlockParameterField(dataSource as VFXEdDataSource, block.m_Params[i].m_Name, Model.GetSlot(i), true, Direction.Input, i);
                    AddChild(m_Fields[i]);
                }
            }

            m_LibraryName = block.m_Name;

            AddChild(new VFXEdNodeBlockHeader(m_LibraryName, VFXEditor.styles.GetIcon(block.m_IconPath == "" ? "Default" : block.m_IconPath), block.m_Params.Length > 0));
            AddManipulator(new ImguiContainer());

            Layout();
        }

        public override void OnRemoved()
        {
            base.OnRemoved();
            Model.Detach();
        }

        /*public override VFXParamValue GetParamValue(string ParamName)
        {
            for(int i = 0; i < m_Params.Length; i++)
            {
                if(m_Params[i].m_Name == LibraryName)
                {
                   return m_ParamValues[i]; 
                }
            }
            return null;
        }

        public override void SetParamValue(string name, VFXParamValue Value)
        {
            for(int i = 0; i < m_Params.Length; i++)
            {
                if(m_Params[i].m_Name == name)
                {
                    m_ParamValues[i].SetValue(Value); 
                }
            }
        }*/

        protected override GUIStyle GetNodeBlockStyle()
        {
            return VFXEditor.styles.NodeBlock;
        }

        protected override GUIStyle GetNodeBlockSelectedStyle()
        {
            return VFXEditor.styles.NodeBlockSelected;
        }

    }
}
