using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025.Managers
{
    #region Public User Action Type Enum

    /// <summary>
    /// Enum for all actions the user can do in the game.
    /// </summary>
    public enum UserAction
    {
        // Default
        None,

        // UI
        PauseGame,
        MouseShowHide,
        UIShowHide,

        // Character actions
        MoveForward,
        MoveBackward,
        TurnLeft,
        TurnRight,
        Sprint,
        Jump,
        Sit,

        // Camera actions
        CameraZoomIn,
        CameraZoomOut,
        CameraMoveForward,
        CameraMoveBackward,
        CameraMoveLeft,
        CameraMoveRight,
        CameraMoveUp,
        CameraMoveDown,
        CameraSprint,
        CameraRotateHorizontal,
        CameraRotateVertical,
        CameraSelectTarget,
        CameraReleaseTarget,
    }

    #endregion

    /// <summary>
    /// InputManager is a wrapper singleton around the Unity input system.
    /// </summary>
    class InputManager
    {
        #region Fields

        // Singleton instance
        static InputManager instance;

        // User action input dictionary
        Dictionary<UserAction, CustomInput> inputs;

        #endregion

        #region Constructor

        /// <summary>
        /// Private InputManager Constructor.
        /// </summary>
        private InputManager()
        {
            // Create and populate the user action input dictionary
            inputs = new Dictionary<UserAction, CustomInput>()
            {
                // UI
                { UserAction.PauseGame, new CustomInput(KeyCode.P) },
                { UserAction.MouseShowHide, new CustomInput(KeyCode.E) },
                { UserAction.UIShowHide, new CustomInput(KeyCode.F) },

                // Character actions
                { UserAction.MoveForward, new CustomInput("Vertical", InputType.Axis) },
                { UserAction.MoveBackward, new CustomInput("Vertical", InputType.Axis) },
                { UserAction.TurnLeft, new CustomInput("Horizontal", InputType.Axis) },
                { UserAction.TurnRight, new CustomInput("Horizontal", InputType.Axis) },
                { UserAction.Sprint, new CustomInput(KeyCode.LeftShift) },
                { UserAction.Jump, new CustomInput(KeyCode.Space) },
                { UserAction.Sit, new CustomInput(KeyCode.J) },
                //{ UserAction.FirePrimary, new CustomInput("0", InputType.MouseButton) },
                //{ UserAction.FireSecondary, new CustomInput("1", InputType.MouseButton) },
                //{ UserAction.Interact, new CustomInput(KeyCode.E) },
                //{ UserAction.Button1, new CustomInput(KeyCode.Alpha1) },
                //{ UserAction.Button2, new CustomInput(KeyCode.Alpha2) },
                //{ UserAction.Button3, new CustomInput(KeyCode.Alpha3) },
                //{ UserAction.Button4, new CustomInput(KeyCode.Alpha4) },
                //{ UserAction.Button5, new CustomInput(KeyCode.Alpha5) },

                // Camera actions
                { UserAction.CameraZoomIn, new CustomInput("Mouse ScrollWheel", InputType.Axis) },
                { UserAction.CameraZoomOut, new CustomInput("Mouse ScrollWheel", InputType.Axis) },
                { UserAction.CameraMoveForward, new CustomInput("Vertical", InputType.Axis) },
                { UserAction.CameraMoveBackward, new CustomInput("Vertical", InputType.Axis) },
                { UserAction.CameraMoveLeft, new CustomInput("Horizontal", InputType.Axis) },
                { UserAction.CameraMoveRight, new CustomInput("Horizontal", InputType.Axis) },
                { UserAction.CameraMoveUp, new CustomInput(KeyCode.Space) },
                { UserAction.CameraMoveDown, new CustomInput(KeyCode.C) },
                { UserAction.CameraSprint, new CustomInput(KeyCode.LeftShift) },
                { UserAction.CameraRotateHorizontal, new CustomInput("Mouse X", InputType.Axis) },
                { UserAction.CameraRotateVertical, new CustomInput("Mouse Y", InputType.Axis) },
                { UserAction.CameraSelectTarget, new CustomInput("0", InputType.MouseButton) },
                { UserAction.CameraReleaseTarget, new CustomInput("1", InputType.MouseButton) },
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the input manager.
        /// </summary>
        public static InputManager Instance
        {
            get { return instance ??= new InputManager(); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the value of the virtual axis identified by axisName.
        /// 
        /// The value will be in the range -1...1 for keyboard and joystick input devices.
        /// 
        /// The meaning of this value depends on the type of input control, for example with a joystick's horizontal axis
        /// a value of 1 means the stick is pushed all the way to the right and a value of -1 means it's all the way to the left;
        /// a value of 0 means the joystick is in its neutral position.
        /// 
        /// If the axis is mapped to the mouse, the value is different and will not be in the range of -1...1.
        /// Instead it'll be the current mouse delta multiplied by the axis sensitivity. Typically a positive value means the mouse
        /// is moving right/down and a negative value means the mouse is moving left/up.
        /// 
        /// This is frame-rate independent; you do not need to be concerned about varying frame-rates when using this value.
        /// 
        /// Note: The Horizontal and Vertical ranges change from 0 to +1 or -1 with increase/decrease in 0.05f steps.
        /// GetAxisRaw has changes from 0 to 1 or -1 immediately, so with no steps.
        /// </summary>
        /// <returns>the value of the axis</returns>
        public float GetAxis(UserAction value)
        {
            return inputs[value].GetAxis();
        }

        /// <summary>
        /// Returns the value of the virtual axis identified by axisName with no smoothing filtering applied.
        /// 
        /// The value will be in the range -1...1 for keyboard and joystick input.Since input is not smoothed,
        /// keyboard input will always be either -1, 0 or 1.
        /// This is useful if you want to do all smoothing of keyboard input processing yourself.
        /// </summary>
        /// <returns>the value of the axis</returns>
        public float GetAxisRaw(UserAction value)
        {
            return inputs[value].GetAxisRaw();
        }

        /// <summary>
        /// Returns true while the virtual button is held down.
        /// </summary>
        /// <returns>true while the button is held down</returns>
        public bool GetButton(UserAction value)
        {
            return inputs[value].GetButton();
        }

        /// <summary>
        /// Returns true during the frame the user pressed down the virtual button.
        /// 
        /// Call this function from the Update method, since the state gets reset each frame.
        /// It will not return true until the user has released the button and pressed it again.
        /// </summary>
        /// <returns>ture on the first frame this button is pressed</returns>
        public bool GetButtonDown(UserAction value)
        {
            return inputs[value].GetButtonDown();
        }

        /// <summary>
        /// Returns true during the frame the user released the virtual button.
        /// 
        /// Call this function from the Update method, since the state gets reset each frame.
        /// It will not return true until the user has pressed the button and released it again.
        /// </summary>
        /// <returns>ture on the first frame this button is released</returns>
        public bool GetButtonUp(UserAction value)
        {
            return inputs[value].GetButtonUp();
        }

        #endregion

        #region Private Methods



        #endregion

        #region Internal Input Class

        /// <summary>
        /// enum for which input type this new input will be
        /// </summary>
        public enum InputType
        {
            Axis,
            Button,
            MouseButton,
        }

        /// <summary>
        /// CustomInput handles if it's a button, an axis, or a mouse button
        /// and act accordingly. Works for any controller.
        /// </summary>
        private class CustomInput
        {
            #region Fields

            string inputAxisString;
            bool canActivate = true;
            InputType inputType;
            KeyCode key = KeyCode.None;

            #endregion

            #region Constructor

            /// <summary>
            /// Constructor for axis and joystick controllers.
            /// </summary>
            /// <param name="input">the name of the string axis</param>
            /// <param name="type">the type of the input</param>
            public CustomInput(string input, InputType type)
            {
                inputAxisString = input;
                inputType = type;
            }

            /// <summary>
            /// Constructor to handle keyboard key codes.
            /// </summary>
            /// <param name="keyCode">keyboard key code</param>
            public CustomInput(KeyCode keyCode)
            {
                key = keyCode;
                inputType = InputType.Button;
            }

            #endregion

            #region Properties



            #endregion

            #region Public Methods

            /// <summary>
            /// Returns the value of the virtual axis identified by axisName.
            /// 
            /// The value will be in the range -1...1 for keyboard and joystick input devices.
            /// 
            /// The meaning of this value depends on the type of input control, for example with a joystick's horizontal axis
            /// a value of 1 means the stick is pushed all the way to the right and a value of -1 means it's all the way to the left;
            /// a value of 0 means the joystick is in its neutral position.
            /// 
            /// If the axis is mapped to the mouse, the value is different and will not be in the range of -1...1.
            /// Instead it'll be the current mouse delta multiplied by the axis sensitivity. Typically a positive value means the mouse
            /// is moving right/down and a negative value means the mouse is moving left/up.
            /// 
            /// This is frame-rate independent; you do not need to be concerned about varying frame-rates when using this value.
            /// 
            /// Note: The Horizontal and Vertical ranges change from 0 to +1 or -1 with increase/decrease in 0.05f steps.
            /// GetAxisRaw has changes from 0 to 1 or -1 immediately, so with no steps.
            /// </summary>
            /// <returns>the value of the axis</returns>
            public float GetAxis()
            {
                if (inputType != InputType.Axis)
                {
                    Debug.Log("Error in CustomInput GetAxis() using " + inputType + ". Returning 0f.");
                    return 0f;
                }

                return Input.GetAxis(inputAxisString);
            }

            /// <summary>
            /// Returns the value of the virtual axis identified by axisName with no smoothing filtering applied.
            /// 
            /// The value will be in the range -1...1 for keyboard and joystick input.Since input is not smoothed,
            /// keyboard input will always be either -1, 0 or 1.
            /// This is useful if you want to do all smoothing of keyboard input processing yourself.
            /// </summary>
            /// <returns>the value of the axis</returns>
            public float GetAxisRaw()
            {
                if (inputType != InputType.Axis)
                {
                    Debug.Log("CustomInput GetAxisRaw() using " + inputType + " is an invalid type! Returning 0f.");
                    return 0f;
                }

                return Input.GetAxisRaw(inputAxisString);
            }

            /// <summary>
            /// Returns true while the virtual button is held down.
            /// </summary>
            /// <returns>true while the button is held down</returns>
            public bool GetButton()
            {
                switch (inputType)
                {
                    case InputType.Axis:
                        return Input.GetAxis(inputAxisString) == 1f ? true : false;
                    case InputType.Button:
                        return Input.GetKey(key);
                    case InputType.MouseButton:
                        if (inputAxisString == "0")
                        {
                            return Input.GetMouseButton(0);
                        }
                        if (inputAxisString == "1")
                        {
                            return Input.GetMouseButton(1);
                        }
                        Debug.Log("CustomInput GetButton() in mouse button is an invalid string button! Returning false.");
                        return false;
                    default:
                        Debug.Log("CustomInput GetButton() using " + inputType + " is an invalid type! Returning false.");
                        return false;
                }
            }

            /// <summary>
            /// Returns true during the frame the user pressed down the virtual button.
            /// 
            /// Call this function from the Update method, since the state gets reset each frame.
            /// It will not return true until the user has released the button and pressed it again.
            /// </summary>
            /// <returns>ture on the first frame this button is pressed</returns>
            public bool GetButtonDown()
            {
                switch (inputType)
                {
                    case InputType.Axis:
                        bool result = false;
                        if (canActivate && Input.GetAxisRaw(inputAxisString) == 1f)
                        {
                            result = true;
                            canActivate = false;
                        }
                        else if (Input.GetAxisRaw(inputAxisString) == 0f)
                        {
                            canActivate = true;
                        }
                        return result;
                    case InputType.Button:
                        return Input.GetKeyDown(key);
                    case InputType.MouseButton:
                        if (inputAxisString == "0")
                        {
                            return Input.GetMouseButtonDown(0);
                        }
                        if (inputAxisString == "1")
                        {
                            return Input.GetMouseButtonDown(1);
                        }
                        Debug.Log("CustomInput GetButtonDown() in mouse button is an invalid string button! Returning false.");
                        return false;
                    default:
                        Debug.Log("CustomInput GetButtonDown() using " + inputType + " is an invalid type! Returning false.");
                        return false;
                }
            }

            /// <summary>
            /// Returns true during the frame the user released the virtual button.
            /// 
            /// Call this function from the Update method, since the state gets reset each frame.
            /// It will not return true until the user has pressed the button and released it again.
            /// </summary>
            /// <returns>ture on the first frame this button is released</returns>
            public bool GetButtonUp()
            {
                switch (inputType)
                {
                    case InputType.Axis:
                        return Input.GetAxis(inputAxisString) == 1f ? true : false;
                    case InputType.Button:
                        return Input.GetKeyUp(key);
                    case InputType.MouseButton:
                        if (inputAxisString == "0")
                        {
                            return Input.GetMouseButtonUp(0);
                        }
                        else if (inputAxisString == "1")
                        {
                            return Input.GetMouseButtonUp(1);
                        }
                        Debug.Log("CustomInput GetButtonUp() in mouse button is an invalid string button! Returning false.");
                        return false;
                    default:
                        Debug.Log("CustomInput GetButtonUp() using " + inputType + " is an invalid type! Returning false.");
                        return false;
                }
            }

            #endregion
        }

        #endregion
    }
}