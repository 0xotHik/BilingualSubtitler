using System;
using System.Diagnostics;
using System.Windows.Forms;
using WindowsInput;
using Settings = BilingualSubtitler.Properties.Settings;

using NeatInput.Windows;
using NeatInput.Windows.Events;

namespace BilingualSubtitler
{
    class KeyboardEventReceiver : IKeyboardEventReceiver
    {
        private NeatInput.Windows.Processing.Keyboard.Enums.Keys m_videoPlayerPauseKey;

        private MainForm m_mainForm;
        private string m_videoPlayerProcessName;
        private NeatInput.Windows.Processing.Keyboard.Enums.Keys[] m_biligualSubtitlersHotkeys;

        public KeyboardEventReceiver(string videoPlayerProcessName, int[] biligualSubtitlersHotkeysCodes, int pauseKeyCode, MainForm mainForm)
        {
            m_mainForm = mainForm;
            m_videoPlayerProcessName = videoPlayerProcessName;
            m_biligualSubtitlersHotkeys = new NeatInput.Windows.Processing.Keyboard.Enums.Keys[biligualSubtitlersHotkeysCodes.Length];

            for (int i=0; i< biligualSubtitlersHotkeysCodes.Length; i++)
            {
                m_biligualSubtitlersHotkeys[i] = (NeatInput.Windows.Processing.Keyboard.Enums.Keys)biligualSubtitlersHotkeysCodes[i];
            }

            m_videoPlayerPauseKey = (NeatInput.Windows.Processing.Keyboard.Enums.Keys)pauseKeyCode;
        }

        public void Receive(KeyboardEvent @event)
        {
            // TODO
            // СДелать флаг, чтобы отсекать нажатия, эмулируемые Bilingual Subtitler'ом?

            // Периодически пригождается
            //
            //#if DEBUG
            //            Debug.WriteLine($"RECEIVE || {DateTime.Now} ||" + @event.Key + " | " + @event.State);
            //#endif

            #region Лог ресива

//// Num0
//RECEIVE 27.07.2022 22:40:16 | NumPad0 | Down
//RECEIVE  27.07.2022 22:40:16 NumPad0 | Up
//The thread 0x489c has exited with code 0(0x0).
//RECEIVE  27.07.2022 22:40:20 NumPad0 | Down
//RECEIVE  27.07.2022 22:40:20 LShiftKey | Down
//RECEIVE  27.07.2022 22:40:20 S | Down
//RECEIVE  27.07.2022 22:40:20 S | Up
//RECEIVE  27.07.2022 22:40:20 LShiftKey | Up
//RECEIVE  27.07.2022 22:40:20 NumPad0 | Up
//The thread 0x4ea8 has exited with code 0(0x0).
//The thread 0x5350 has exited with code 0(0x0).
//The thread 0x5224 has exited with code 0(0x0).
//RECEIVE  27.07.2022 22:40:31 NumPad0 | Down
//RECEIVE  27.07.2022 22:40:31 NumPad0 | Up
//RECEIVE  27.07.2022 22:40:34 NumPad0 | Down
//RECEIVE  27.07.2022 22:40:34 LShiftKey | Down
//RECEIVE  27.07.2022 22:40:34 S | Down
//RECEIVE  27.07.2022 22:40:34 S | Up
//RECEIVE  27.07.2022 22:40:34 LShiftKey | Up
//RECEIVE  27.07.2022 22:40:34 NumPad0 | Up

//// Space
//The thread 0x2c78 has exited with code 0(0x0).
//RECEIVE  27.07.2022 22:42:44 Space | Down
//RECEIVE  27.07.2022 22:42:44 Space | Up
//RECEIVE  27.07.2022 22:42:48 Space | Down
//RECEIVE  27.07.2022 22:42:48 LShiftKey | Down
//RECEIVE  27.07.2022 22:42:48 S | Down
//RECEIVE  27.07.2022 22:42:48 S | Up
//RECEIVE  27.07.2022 22:42:48 LShiftKey | Up
//RECEIVE  27.07.2022 22:42:48 Space | Up
//The thread 0x41d4 has exited with code 0(0x0).
//RECEIVE  27.07.2022 22:42:53 Space | Down
//RECEIVE  27.07.2022 22:42:53 Space | Up
//The thread 0x18a8 has exited with code 0(0x0).
//The thread 0x4740 has exited with code 0(0x0).
//RECEIVE  27.07.2022 22:42:55 Space | Down
//RECEIVE  27.07.2022 22:42:55 LShiftKey | Down
//RECEIVE  27.07.2022 22:42:55 S | Down
//RECEIVE  27.07.2022 22:42:55 S | Up
//RECEIVE  27.07.2022 22:42:55 LShiftKey | Up
//RECEIVE  27.07.2022 22:42:55 || Space | Up

            #endregion

            if (@event.State == NeatInput.Windows.Processing.Keyboard.Enums.KeyStates.Down)
            {
                var activeProcess = m_mainForm.GetActiveProcess();
                if (activeProcess.ProcessName != m_videoPlayerProcessName)
                    return;
                m_mainForm.VideoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

                // Если есть среди массива горячих клавиш
                foreach (var hotkey in m_biligualSubtitlersHotkeys)
                {
                    if (@event.Key == hotkey)
                    {
                        // Если это кнопка паузы
                         if (@event.Key == m_videoPlayerPauseKey)
                        {
                            //e.Handled = true;
                        }
                        else
                        {
                            m_mainForm.PostMessagePauseKey();
                        }

                        //Debug.WriteLine("Перед свитчем");
                        m_mainForm.SwitchSubtitlesStatesAndComboBox();

                        break;
                    }
                }
            }
        }
    }

    partial class MainForm
    {
        const UInt32 WM_KEYDOWN = 0x0100;

        private string m_videoPlayerProcessName;
        private IntPtr m_videoPlayerProcessMainWindowHandle;

        public IntPtr VideoPlayerProcessMainWindowHandle
        {
            set => m_videoPlayerProcessMainWindowHandle = value;
        }

        private int m_videoplayerPauseHotkey;

        private InputSimulator m_inputSimulator;

        private int[] m_biligualSubtitlersHotkeysCodes;
        private char[] m_biligualSubtitlersHotkeysChars;
        private char m_videoPlayerPauseKeyChar = ' ';

        private Stopwatch m_stopwatch;

        private KeyboardEventReceiver m_keyboardEventReceiver;
        private InputSource m_inputSource = null;

        private void InputHandlingConstructor()
        {
            m_inputSimulator = new InputSimulator();
        }

        private void SetInputHandlingAccordiongToSettings()
        {
            if (m_inputSource != null)
                m_inputSource.Dispose();

            // Хоткеи программы
            m_biligualSubtitlersHotkeysCodes = new int[Settings.Default.Hotkeys.Count];
            for (int i = 0; i < Settings.Default.Hotkeys.Count; i++)
            {
                var hotkey = new Hotkey(Settings.Default.Hotkeys[i]);
                m_biligualSubtitlersHotkeysCodes[i] = hotkey.KeyCode;

            }

            m_keyboardEventReceiver = new KeyboardEventReceiver(m_videoPlayerProcessName, m_biligualSubtitlersHotkeysCodes, m_videoplayerPauseHotkey, this);
            //
            m_inputSource = new InputSource(m_keyboardEventReceiver);
            m_inputSource.Listen();


        }


        #region Былое
        //private void ActionForHotkeyThatAreNotPauseButton()
        //{
        //    var millisecondsFromLastKeyDown = m_stopwatch.ElapsedMilliseconds;
        //    Debug.WriteLine(millisecondsFromLastKeyDown);
        //    if (millisecondsFromLastKeyDown < 50)
        //    {
        //        m_stopwatch.Restart();
        //        return;
        //    }
        //    //
        //    m_stopwatch.Restart();

        //    var activeProcess = GetActiveProcess();
        //    if (activeProcess.ProcessName != m_videoPlayerProcessName)
        //        return;
        //    m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

        //    PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_videoplayerPauseHotkey, 0);
        //    SwitchSubtitlesStatesAndComboBox();
        //}

        ///// <summary>
        ///// Былое.
        ///// </summary>
        //private void ActionForHotkeyThatArePauseButton()
        //{
        //    //if (GetActiveProcessName() != m_videoPlayerProcessName)
        //    //    return;

        //    var activeProcess = GetActiveProcess();
        //    if (activeProcess.ProcessName != m_videoPlayerProcessName)
        //        return;
        //    m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

        //    SwitchSubtitlesStatesAndComboBox();
        //}
        #endregion

        public void PostMessagePauseKey()
        {
            PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_videoplayerPauseHotkey, 0);
        }

        

        public void SwitchSubtitlesStatesAndComboBox()
        {
            // Я так понимаю, сюда мы попадаем еще до переключения паузы/воспроизведения

            if (m_videoState == VideoState.Playing)
            {
                if (m_subtitlesState == SubtitlesState.Original)
                {
                    // Переключаемся на двуязычные

                    m_changeSubtitlesToBilingualDelegate.Invoke();

                    m_subtitlesState = SubtitlesState.Bilingual;
                }

                // Ставим Paused в КомбоБоксе
                m_videoState = VideoState.Paused;
            }
            else
            {
                if (m_subtitlesState == SubtitlesState.Bilingual)
                {
                    // Переключаемся на оригинальные

                    m_changeSubtitlesToOriginalDelegate.Invoke();

                    m_subtitlesState = SubtitlesState.Original;
                }

                // Ставим Playing в КомбоБоксе
                m_videoState = VideoState.Playing;
            }

            BeginInvoke(m_changeVideoAndSubtitlesComboBoxesDelegate);

            #region Былое:
            //
            //
            //// Я так понимаю, сюда мы попадаем еще до переключения паузы/воспроизведения

            //if (m_videoState == VideoState.Playing)
            //{
            //    if (m_subtitlesState == SubtitlesState.Original)
            //    {
            //        // Переключаемся на двуязычные

            //        m_changeSubtitlesToBilingualDelegate.Invoke();
            //        //m_changeSubtitlesToBilingualDelegate.BeginInvoke(null, null);

            //        m_subtitlesState = SubtitlesState.Bilingual;
            //    }

            //    // Ставим Paused в КомбоБоксе
            //    m_videoState = VideoState.Paused;
            //}
            //else
            //{
            //    if (m_subtitlesState == SubtitlesState.Bilingual)
            //    {
            //        // Переключаемся на оригинальные

            //        m_changeSubtitlesToOriginalDelegate.Invoke();
            //        //m_changeSubtitlesToOriginalDelegate.BeginInvoke(null, null);

            //        m_subtitlesState = SubtitlesState.Original;
            //    }

            //    // Ставим Playing в КомбоБоксе
            //    m_videoState = VideoState.Playing;
            //}

            //BeginInvoke(m_changeVideoAndSubtitlesComboBoxesDelegate);
            #endregion
        }

        private void ChangeSubtitlesToBilingualByPostMessage()
        {
            PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_changeSubtitlesToBilingualHotkeyCode, 0);
        }

        private void ChangeSubtitlesToBilingualByInputSimulator()
        {
            m_inputSimulator.Keyboard.ModifiedKeyStroke(
                m_changeSubtitlesToBilingualHotkeyModifierKeyVirtualKeyCode.Value,
                m_changeSubtitlesToBilingualHotkeyVirtualKeyCode.Value);
        }

        private void ChangeSubtitlesToOriginalByPostMessage()
        {
            PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_changeSubtitlesToOriginalHotkeyCode, 0);
        }

        private void ChangeSubtitlesToOriginalByInputSimulator()
        {
            m_inputSimulator.Keyboard.ModifiedKeyStroke(
                m_changeSubtitlesToOriginalHotkeyModifierKeyVirtualKeyCode.Value,
                m_changeSubtitlesToOriginalHotkeyVirtualKeyCode.Value);
        }

        string GetActiveProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        public Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            return Process.GetProcessById((int)pid);
        }


        #region NonInvasiveKeyboardHook 2.1.1

        //using NonInvasiveKeyboardHookLibrary;

        //private KeyboardHookManager m_keyboardHookManager;

        //private void InputHandlingConstructor()
        //{
        //    m_keyboardHookManager = new KeyboardHookManager();
        //    m_keyboardHookManager.Start();
        //}

        //private void SetInputHandlingAccordiongToSettings()
        //{
        //    // Хоткеи программы
        //    m_keyboardHookManager.Stop();
        //    m_keyboardHookManager.UnregisterAll();

        //    foreach (var keyCode in m_biligualSubtitlersHotkeysCodes)
        //    {
        //        if (keyCode != videoPlayerPauseHotkey.KeyCode)
        //            m_keyboardHookManager.RegisterHotkey(keyCode, ActionForHotkeyThatAreNotPauseButton);
        //        else
        //            m_keyboardHookManager.RegisterHotkey(keyCode, ActionForHotkeyThatArePauseButton);
        //    }

        //    //// TODO TEMP
        //    //m_biligualSubtitlersHotkeysChars = new char[Settings.Default.Hotkeys.Count];
        //    //KeysConverter kc = new KeysConverter();
        //    //for (int i = 0; i < Settings.Default.Hotkeys.Count; i++)
        //    //{
        //    //    var hotkey = new Hotkey(Settings.Default.Hotkeys[i]);
        //    //    m_biligualSubtitlersHotkeysChars[i] = GetCharProducingByPressingKeyWithThisCode(hotkey.KeyCode);

        //    //    // TODO TEMP
        //    //    //if (keyCode != videoPlayerPauseHotkey.KeyCode)
        //    //}

        //    m_keyboardHookManager.Start();

        //    m_stopwatch = new Stopwatch();
        //    m_stopwatch.Start();

        //}

        //private void ActionForHotkeyThatAreNotPauseButton()
        //{
        //    var millisecondsFromLastKeyDown = m_stopwatch.ElapsedMilliseconds;
        //    Debug.WriteLine(millisecondsFromLastKeyDown);
        //    if (millisecondsFromLastKeyDown < 50)
        //    {
        //        m_stopwatch.Restart();
        //        return;
        //    }
        //    //
        //    m_stopwatch.Restart();

        //    var activeProcess = GetActiveProcess();
        //    if (activeProcess.ProcessName != m_videoPlayerProcessName)
        //        return;
        //    m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

        //    PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_videoplayerPauseHotkey, 0);
        //    SwitchSubtitlesStatesAndComboBox();
        //}

        //private void ActionForHotkeyThatArePauseButton()
        //{
        //    //if (GetActiveProcessName() != m_videoPlayerProcessName)
        //    //    return;

        //    var activeProcess = GetActiveProcess();
        //    if (activeProcess.ProcessName != m_videoPlayerProcessName)
        //        return;
        //    m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

        //    SwitchSubtitlesStatesAndComboBox();
        //}

        //public void SwitchSubtitlesStatesAndComboBox()
        //{
        //    // Я так понимаю, сюда мы попадаем еще до переключения паузы/воспроизведения

        //    if (m_videoState == VideoState.Playing)
        //    {
        //        if (m_subtitlesState == SubtitlesState.Original)
        //        {
        //            // Переключаемся на двуязычные

        //            m_changeSubtitlesToBilingualDelegate.Invoke();

        //            m_subtitlesState = SubtitlesState.Bilingual;
        //        }

        //        // Ставим Paused в КомбоБоксе
        //        m_videoState = VideoState.Paused;
        //    }
        //    else
        //    {
        //        if (m_subtitlesState == SubtitlesState.Bilingual)
        //        {
        //            // Переключаемся на оригинальные

        //            m_changeSubtitlesToOriginalDelegate.Invoke();

        //            m_subtitlesState = SubtitlesState.Original;
        //        }

        //        // Ставим Playing в КомбоБоксе
        //        m_videoState = VideoState.Playing;
        //    }

        //    BeginInvoke(m_changeVideoAndSubtitlesComboBoxesDelegate);

        //    // Былое:
        //    //
        //    //
        //    //// Я так понимаю, сюда мы попадаем еще до переключения паузы/воспроизведения

        //    //if (m_videoState == VideoState.Playing)
        //    //{
        //    //    if (m_subtitlesState == SubtitlesState.Original)
        //    //    {
        //    //        // Переключаемся на двуязычные

        //    //        m_changeSubtitlesToBilingualDelegate.Invoke();
        //    //        //m_changeSubtitlesToBilingualDelegate.BeginInvoke(null, null);

        //    //        m_subtitlesState = SubtitlesState.Bilingual;
        //    //    }

        //    //    // Ставим Paused в КомбоБоксе
        //    //    m_videoState = VideoState.Paused;
        //    //}
        //    //else
        //    //{
        //    //    if (m_subtitlesState == SubtitlesState.Bilingual)
        //    //    {
        //    //        // Переключаемся на оригинальные

        //    //        m_changeSubtitlesToOriginalDelegate.Invoke();
        //    //        //m_changeSubtitlesToOriginalDelegate.BeginInvoke(null, null);

        //    //        m_subtitlesState = SubtitlesState.Original;
        //    //    }

        //    //    // Ставим Playing в КомбоБоксе
        //    //    m_videoState = VideoState.Playing;
        //    //}

        //    //BeginInvoke(m_changeVideoAndSubtitlesComboBoxesDelegate);
        //}



        #endregion

        #region Gma.System.MouseKeyHook
        // using Gma.System.MouseKeyHook;

        //private IKeyboardMouseEvents m_keyboardMouseEvents;

        //private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        //{
        //    var activeProcess = GetActiveProcess();
        //    if (activeProcess.ProcessName != m_videoPlayerProcessName)
        //        return;
        //    m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

        //    // Если есть среди массива горячих клавиш
        //    foreach (var hotkey in m_biligualSubtitlersHotkeysChars)
        //    {
        //        if (e.KeyChar == hotkey)
        //        {
        //            // Если это кнопка паузы
        //            if (e.KeyChar == m_videoPlayerPauseKeyChar)
        //            {
        //                e.Handled = true;
        //            }
        //            else
        //            {
        //                PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_videoplayerPauseHotkey, 0);
        //            }

        //            SwitchSubtitlesStatesAndComboBox();


        //            //if (e.KeyChar == '7')
        //            ////if (e.KeyChar == ' ')
        //            //{
        //            //    e.Handled = true;
        //            //    ActionForHotkeyThatAreNotPauseButton();
        //            //}
        //        }
        //    }
        //}

        #endregion

    }
}
