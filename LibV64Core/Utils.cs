using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibV64Core.Types;

namespace LibV64Core
{
    public class Utils
    {
        #region Color Codes
        /// <summary>
        /// Applies a Light (RGB) to a specified address.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="light"></param>
        public static void ApplyLightToAddress(int address, Light light)
        {
            byte[] rColorData = { BitConverter.GetBytes(light.R)[0] };
            Memory.WriteBytes(Memory.BaseAddress + address + 3, rColorData);
            byte[] gColorData = { BitConverter.GetBytes(light.G)[0] };
            Memory.WriteBytes(Memory.BaseAddress + address + 2, gColorData);
            byte[] bColorData = { BitConverter.GetBytes(light.B)[0] };
            Memory.WriteBytes(Memory.BaseAddress + address + 1, bColorData);
        }

        /// <summary>
        /// Loads a Light (RGB) from a specified address.
        /// </summary>
        /// <param name="startAddress"></param>
        /// <returns></returns>
        public static Light LoadLightFromAddress(int startAddress)
        {
            Light light = new Light();

            if (!Memory.IsEmulatorOpen || Memory.BaseAddress == 0)
                return light;

            // Begin building light.

            light.R = Int32.Parse(BitConverter.ToString(Memory.ReadBytes(Memory.BaseAddress + startAddress + 3, 1)), System.Globalization.NumberStyles.HexNumber);
            light.G = Int32.Parse(BitConverter.ToString(Memory.ReadBytes(Memory.BaseAddress + startAddress + 2, 1)), System.Globalization.NumberStyles.HexNumber);
            light.B = Int32.Parse(BitConverter.ToString(Memory.ReadBytes(Memory.BaseAddress + startAddress + 1, 1)), System.Globalization.NumberStyles.HexNumber);

            return light;
        }
        #endregion
    }
}
