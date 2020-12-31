using UnityEngine;
using Harmony;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Sensor/KeyboardInputSensor")]
    public class KeyboardInputSensor : InputSensor
    {
        private Keyboard keyboard;

        private KeyboardsInputDevice keyboardsInputDevice;

        public IInputDevice Keyboards
        {
            get { return keyboardsInputDevice; }
        }

        private void InjectKeyboardInputDevice([ApplicationScope] Keyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        private void Awake()
        {
            InjectDependencies("InjectKeyboardInputDevice");

            keyboardsInputDevice = new KeyboardsInputDevice(keyboard);
        }

        private void Update()
        {
            keyboardsInputDevice.Update();
        }

        private class KeyboardsInputDevice : InputDevice
        {
            private readonly Keyboard keyboard;

            public KeyboardsInputDevice(Keyboard keyboard)
            {
                this.keyboard = keyboard;
            }

            public void Update()
            {
                HandleUiInput();
                HandleActionInput();
                HandleDirectionInput();
                HandleRotationInput();
            }

            public override IInputDevice this[int deviceIndex]
            {
                get { return this; }
            }

            private void HandleUiInput()
            {
                if (keyboard.GetKeyDown(KeyCode.UpArrow))
                {
                    NotifyUp();
                }
                if (keyboard.GetKeyDown(KeyCode.DownArrow))
                {
                    NotifyDown();
                }
                if (keyboard.GetKeyDown(KeyCode.Return))
                {
                    NotifyConfirm();
                }
            }

            private void HandleActionInput()
            {
                if (keyboard.GetKeyDown(KeyCode.Space))
                {
                    NotifyFire();
                }
                if (keyboard.GetKeyDown(KeyCode.LeftShift))
                {
                    NotifySwitchWeapon();
                }
                if (keyboard.GetKeyDown(KeyCode.LeftControl))
                {
                    NotifyMeleeAttack();
                }
                if (keyboard.GetKeyDown(KeyCode.Escape))
                {
                    NotifyTogglePause();
                }

                if (keyboard.GetKeyDown(KeyCode.Keypad1))
                {
                    NotifySkill1();
                }
                if (keyboard.GetKeyDown(KeyCode.Keypad2))
                {
                    NotifyBuild();
                }
                if (keyboard.GetKeyDown(KeyCode.Keypad3))
                {
                    NotifySkill2();
                }
                if (keyboard.GetKeyDown(KeyCode.Keypad4))
                {
                    NotifyRepair();
                }
            }

            private void HandleDirectionInput()
            {
                if (keyboard.GetKey(KeyCode.D))
                {
                    NotifyMoveX(1);
                }
                if (keyboard.GetKey(KeyCode.A))
                {
                    NotifyMoveX(-1);
                }
                if (keyboard.GetKey(KeyCode.W))
                {
                    NotifyMoveY(1);
                }
                if (keyboard.GetKey(KeyCode.S))
                {
                    NotifyMoveY(-1);
                }
            }

            private void HandleRotationInput()
            {
                if (keyboard.GetKey(KeyCode.LeftArrow))
                {
                    NotifyAimX(-1.0f);
                }
                if (keyboard.GetKey(KeyCode.RightArrow))
                {
                    NotifyAimX(1.0f);
                }
                if (keyboard.GetKey(KeyCode.DownArrow))
                {
                    NotifyAimY(-1.0f);
                }
                if (keyboard.GetKey(KeyCode.UpArrow))
                {
                    NotifyAimY(1.0f);
                }
            }
        }
    }
}