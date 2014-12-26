using System;

namespace VirtualScene.Entities
{
    /// <summary>
    /// Arguments of moving object in scene event
    /// </summary>
    public class MoveEventArgs: EventArgs
    {
        /// <summary>
        /// Offset on X axis
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Offset on Y axis
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// Offset on Z axis
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveEventArgs" />
        /// </summary>
        /// <param name="x">Offset on X axis</param>
        /// <param name="y">Offset on Y axis</param>
        /// <param name="z">Offset on Z axis</param>
        public MoveEventArgs(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}