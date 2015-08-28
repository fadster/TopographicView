using ColossalFramework.UI;
using ColossalFramework;
using ICities;
using UnityEngine;
using System;

namespace TopographicView
{
    public class TopographicViewMod : ThreadingExtensionBase, IUserMod
    {
        public string Name { get { return "Topographical View"; } }
        public string Description { get { return "Terrain heightmap info view."; } }

        private static InfoManager.InfoMode m_previousMode;
        private static InfoManager.SubInfoMode m_previousSubInfoMode;

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (Event.current.alt && Input.GetKeyDown(KeyCode.T))
            {
                var manager = Singleton<InfoManager>.instance;
                UIView.playSoundDelegate(UIView.GetAView().defaultClickSound, 1f);

                if (manager.CurrentMode != InfoManager.InfoMode.TerrainHeight) {
                    BuildingTool currentTool = ToolsModifierControl.GetCurrentTool<BuildingTool>();
                    if (currentTool != null && currentTool.m_relocate != 0) {
                        currentTool.CancelRelocate();
                    }
                    Singleton<InfoViewsPanel>.instance.CloseToolbar();
                    WorldInfoPanel.HideAllWorldInfoPanels();
                    if (Singleton<InfoManager>.exists) {
                        m_previousMode = manager.CurrentMode;
                        m_previousSubInfoMode = manager.CurrentSubMode;
                        Singleton<InfoManager>.instance.SetCurrentMode(InfoManager.InfoMode.TerrainHeight, InfoManager.SubInfoMode.Default);
                        Singleton<GuideManager>.instance.InfoViewUsed();
                    }
                } else {
                    Singleton<InfoViewsPanel>.instance.CloseToolbar();
                    manager.SetCurrentMode(m_previousMode, m_previousSubInfoMode);
                    Singleton<GuideManager>.instance.InfoViewUsed();
                }
            }
        }
    }
}

