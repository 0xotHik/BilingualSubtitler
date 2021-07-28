using System;
using System.Diagnostics;

namespace BilingualSubtitler
{
    partial class MainForm
    {
        private void ActionForHotkeyThatAreNotPauseButton()
        {
            //if (GetActiveProcessName() != m_videoPlayerProcessName)
            //    return;
            //m_videoPlayerProcess = Process.GetProcessesByName("mpc-hc64")[0];

            var activeProcess = GetActiveProcess();
            if (activeProcess.ProcessName != m_videoPlayerProcessName)
                return;
            m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

            PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_videoplayerPauseHotkey, 0);
            SwitchSubtitles();
        }

        private void ActionForHotkeyThatArePauseButton()
        {
            //if (GetActiveProcessName() != m_videoPlayerProcessName)
            //    return;

            var activeProcess = GetActiveProcess();
            if (activeProcess.ProcessName != m_videoPlayerProcessName)
                return;
            m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

            SwitchSubtitles();
        }

        private void SwitchSubtitles()
        {
            // Я так понимаю, сюда мы попадаем еще до переключения паузы/воспроизведения
            if (m_videoState == VideoState.Playing)
            {
                if (m_subtitlesState == SubtitlesState.Original)
                {
                    // Переключаемся на двуязычные

                    m_changeSubtitlesToBilingualDelegate.Invoke();
                    //m_changeSubtitlesToBilingualDelegate.BeginInvoke(null, null);

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
                    //m_changeSubtitlesToOriginalDelegate.BeginInvoke(null, null);

                    m_subtitlesState = SubtitlesState.Original;
                }

                // Ставим Playing в КомбоБоксе
                m_videoState = VideoState.Playing;
            }

            BeginInvoke(m_changeVideoAndSubtitlesComboBoxesDelegate);
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

        Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            return Process.GetProcessById((int)pid);
        }
    }
}
