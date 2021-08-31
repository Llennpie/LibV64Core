using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
