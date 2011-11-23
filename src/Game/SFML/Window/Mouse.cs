using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SFML
{
    namespace Window
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Give access to the real-time state of the mouse
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class Mouse
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Mouse buttons
            /// </summary>
            ////////////////////////////////////////////////////////////
            public enum Button
            {
                /// <summary>The left mouse button</summary>
                Left,

                /// <summary>The right mouse button</summary>
                Right,

                /// <summary>The middle (wheel) mouse button</summary>
                Middle,

                /// <summary>The first extra mouse button</summary>
                XButton1,

                /// <summary>The second extra mouse button</summary>
                XButton2,

                /// <summary>Keep last -- the total number of mouse buttons</summary>
                ButtonCount
            };

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Check if a mouse button is pressed
            /// </summary>
            /// <param name="button">Button to check</param>
            /// <returns>True if the button is pressed, false otherwise</returns>
            ////////////////////////////////////////////////////////////
            public static bool IsButtonPressed(Button button)
            {
                return sfMouse_IsButtonPressed(button);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the current position of the mouse
            /// </summary>
            /// This function returns the current position of the mouse
            /// cursor in desktop coordinates.
            /// <returns>Current position of the mouse</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2i GetPosition()
            {
                Vector2i position;
                sfMouse_GetPosition(out position.X, out position.Y, IntPtr.Zero);
                return position;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the current position of the mouse
            /// </summary>
            /// This function returns the current position of the mouse
            /// cursor relative to a window.
            /// <param name="relativeTo">Reference window</param>
            /// <returns>Current position of the mouse</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2i GetPosition(Window relativeTo)
            {
                Vector2i position;
                sfMouse_GetPosition(out position.X, out position.Y, relativeTo.This);
                return position;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Set the current position of the mouse
            /// </summary>
            /// This function sets the current position of the mouse
            /// cursor in desktop coordinates.
            /// <param name="position">New position of the mouse</param>
            ////////////////////////////////////////////////////////////
            public static void SetPosition(Vector2i position)
            {
                sfMouse_SetPosition(position.X, position.Y, IntPtr.Zero);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Set the current position of the mouse
            /// </summary>
            /// This function sets the current position of the mouse
            /// cursor relative to a window.
            /// <param name="position">New position of the mouse</param>
            /// <param name="relativeTo">Reference window</param>
            ////////////////////////////////////////////////////////////
            public static void SetPosition(Vector2i position, Window relativeTo)
            {
                sfMouse_SetPosition(position.X, position.Y, relativeTo.This);
            }

            #region Imports
            [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfMouse_IsButtonPressed(Button button);

            [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMouse_GetPosition(out int x, out int y, IntPtr relativeTo);

            [DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMouse_SetPosition(int x, int y, IntPtr relativeTo);
            #endregion
        }
    }
}
