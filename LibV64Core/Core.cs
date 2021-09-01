using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibV64Core.Types;

namespace LibV64Core
{
    public class Core
    {
        #region Camera
        public static bool CameraFrozen;

        /// <summary>
        /// Freezes/unfreezes the game camera. Defaults to true.
        /// </summary>
        /// <param name="freeze"></param>
        public static void FreezeCamera(bool freeze = true)
        {
            if (!Memory.IsEmulatorOpen || Memory.BaseAddress == 0)
                return;

            if (freeze) {
                byte[] writeData = { 0x80 };
                Memory.WriteBytes(Memory.BaseAddress + 0x33C84B, writeData);
                CameraFrozen = true;
            }
            else
            {
                byte[] writeData = { 0x00 };
                Memory.WriteBytes(Memory.BaseAddress + 0x33C84B, writeData);
                CameraFrozen = false;
            }
        }

        /// <summary>
        /// Zero-fills the camera zoom out flags, allowing the camera to be frozen in any level.
        /// </summary>
        public static void FixCameraZoomOut()
        {
            if (!Memory.IsEmulatorOpen || Memory.BaseAddress == 0)
                return;

            Memory.WriteBytes(Memory.BaseAddress + 0x32F870, new byte[20]);
        }
        #endregion

        #region Color Codes
        /// <summary>
        /// Applies a color code to the game.
        /// </summary>
        /// <param name="colorCode"></param>
        public static void ApplyColorCode(ColorCode colorCode)
        {
            if (!Memory.IsEmulatorOpen || Memory.BaseAddress == 0)
                return;

            Utils.ApplyLightToAddress(0x07EC40, colorCode.Shirt.Main);
            Utils.ApplyLightToAddress(0x07EC38, colorCode.Shirt.Shade);
            Utils.ApplyLightToAddress(0x07EC28, colorCode.Overalls.Main);
            Utils.ApplyLightToAddress(0x07EC20, colorCode.Overalls.Shade);
            Utils.ApplyLightToAddress(0x07EC58, colorCode.Gloves.Main);
            Utils.ApplyLightToAddress(0x07EC50, colorCode.Gloves.Shade);
            Utils.ApplyLightToAddress(0x07EC70, colorCode.Shoes.Main);
            Utils.ApplyLightToAddress(0x07EC68, colorCode.Shoes.Shade);
            Utils.ApplyLightToAddress(0x07EC88, colorCode.Skin.Main);
            Utils.ApplyLightToAddress(0x07EC80, colorCode.Skin.Shade);
            Utils.ApplyLightToAddress(0x07ECA0, colorCode.Hair.Main);
            Utils.ApplyLightToAddress(0x07EC98, colorCode.Hair.Shade);
        }

        /// <summary>
        /// Loads a color code from the game.
        /// </summary>
        /// <returns></returns>
        public static ColorCode LoadColorCodeFromGame()
        {
            ColorCode colorCode = new ColorCode();

            if (!Memory.IsEmulatorOpen || Memory.BaseAddress == 0)
                return colorCode;

            // Begin building color code.

            colorCode.Shirt.Main = Utils.LoadLightFromAddress(0x07EC40);
            colorCode.Shirt.Shade = Utils.LoadLightFromAddress(0x07EC38);
            colorCode.Overalls.Main = Utils.LoadLightFromAddress(0x07EC28);
            colorCode.Overalls.Shade = Utils.LoadLightFromAddress(0x07EC20);
            colorCode.Gloves.Main = Utils.LoadLightFromAddress(0x07EC58);
            colorCode.Gloves.Shade = Utils.LoadLightFromAddress(0x07EC50);
            colorCode.Shoes.Main = Utils.LoadLightFromAddress(0x07EC70);
            colorCode.Shoes.Shade = Utils.LoadLightFromAddress(0x07EC68);
            colorCode.Skin.Main = Utils.LoadLightFromAddress(0x07EC88);
            colorCode.Skin.Shade = Utils.LoadLightFromAddress(0x07EC80);
            colorCode.Hair.Main = Utils.LoadLightFromAddress(0x07ECA0);
            colorCode.Hair.Shade = Utils.LoadLightFromAddress(0x07EC98);

            return colorCode;
        }
        #endregion

        /// <summary>
        /// Reloads all core values (such as CameraFrozen) from their in-game addresses. Useful when the tool relaunches after values were modified.
        /// </summary>
        public static void ReloadPreviousValues()
        {
            byte[] freezeCameraData = Memory.SwapEndian(Memory.ReadBytes(Memory.BaseAddress + 0x33C84B, 1), 4);

            if (freezeCameraData[0] == 0x80)
                CameraFrozen = true;
        }
    }
}
