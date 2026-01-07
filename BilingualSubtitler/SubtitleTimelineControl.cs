using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BilingualSubtitler
{
    // ======================== DATA ========================


    public class SubtitleTrack
    {
        public Subtitle[] Subtitles { get; set; } = Array.Empty<Subtitle>();
        public Color Color { get; set; } = Color.SteelBlue;
    }

    // ======================== CONTROL ========================


    public partial class SubtitleTimelineControl : UserControl
    {
        // -------- Tracks --------

        public SubtitleTrack[] Tracks { get; } =
        {
            new(), new(), new(), new(), new(), new()
        };

        // -------- Zoom --------

        public float PixelsPerSecond { get; private set; } = 50f;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MinZoom { get; set; } = 1f;      // << большее отдаление
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MaxZoom { get; set; } = 1000f;   // << большее приближение

        // -------- Layout --------

        private const int TrackHeight = 30;
        private const int TrackStartY = 20;
        private const int TrackSpacing = 20;

        // -------- Controls --------

        private readonly HScrollBar _hScroll;
        private readonly TrackBar _zoomBar;
        private readonly TimelineView _view;
        private bool _updatingZoomInternally;

        // -------- Public API --------

        /// <summary>
        /// Перерисовать таймлайн, сохранив текущую точку взгляда
        /// (центр экрана по времени + текущий зум)
        /// </summary>
        public void RefreshTimelineKeepView()
        {
            if (!_view.IsHandleCreated)
                return;

            float centerTime =
                (_view.ScrollX + _view.ClientSize.Width / 2f) / PixelsPerSecond;

            // ❗ напрямую обновляем scroll под текущий зум
            _view.ScrollX = (int)(centerTime * PixelsPerSecond
                                 - _view.ClientSize.Width / 2f);

            _view.ClampScroll();
            UpdateScrollBar();

            _view.Invalidate(); 

            //float centerTime = (_view.ScrollX + _view.ClientSize.Width / 2f) / PixelsPerSecond;
            //SetZoom(PixelsPerSecond, centerTime);
        }

        // -------- Zoom logic --------

        private void SetZoom(float newZoom, float? anchorTime = null)
        {
            newZoom = Math.Clamp(newZoom, MinZoom, MaxZoom);
            if (Math.Abs(PixelsPerSecond - newZoom) < 0.001f)
                return;

            PixelsPerSecond = newZoom;

            // sync slider (exponential mapping)
            _updatingZoomInternally = true;
            float t = (float)(Math.Log(PixelsPerSecond / MinZoom) /
                              Math.Log(MaxZoom / MinZoom));
            _zoomBar.Value = Math.Clamp(
                (int)Math.Round(t * 100),
                _zoomBar.Minimum,
                _zoomBar.Maximum);
            _updatingZoomInternally = false;

            if (anchorTime.HasValue)
            {
                _view.ScrollX = (int)(anchorTime.Value * PixelsPerSecond
                                     - _view.ClientSize.Width / 2f);
                _view.ClampScroll();
            }

            UpdateScrollBar();
            _view.Invalidate();
        }

        private void SetZoomFromSlider()
        {
            if (_updatingZoomInternally) return;

            float t = _zoomBar.Value / 100f;
            float zoom = MinZoom * (float)Math.Pow(MaxZoom / MinZoom, t);
            SetZoom(zoom);
        }

        // -------- ctor --------

        public SubtitleTimelineControl()
        {
            _view = new TimelineView(this)
            {
                Dock = DockStyle.Fill
            };

            _hScroll = new HScrollBar
            {
                Dock = DockStyle.Bottom
            };
            _hScroll.Scroll += (_, __) =>
            {
                _view.ScrollX = _hScroll.Value;
                _view.Invalidate();
            };

            _zoomBar = new TrackBar
            {
                Dock = DockStyle.Right,
                Orientation = Orientation.Vertical,
                Minimum = 0,
                Maximum = 100,
                Value = 30,
                TickFrequency = 10,
                Width = 40
            };
            _zoomBar.ValueChanged += (_, __) => SetZoomFromSlider();

            Controls.Add(_view);
            Controls.Add(_hScroll);
            Controls.Add(_zoomBar);

            UpdateScrollBar();
            SetZoomFromSlider();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            BeginInvoke(new Action(() =>
            {
                UpdateScrollBar();
                _view.Invalidate();
            }));
        }

        internal void UpdateScrollBar()
        {
            int max = Math.Max(0, (int)(_view.TimelineWidth - _view.ClientSize.Width));
            _hScroll.LargeChange = _view.ClientSize.Width;
            _hScroll.Maximum = Math.Max(0, max + _hScroll.LargeChange - 1);
            _hScroll.Value = Math.Min(_hScroll.Value, max);
        }





        // ======================== VIEW ========================

        private sealed class TimelineView : Control
        {
            private readonly SubtitleTimelineControl _owner;
            public int ScrollX;
            private readonly ToolTip _toolTip = new();
            private Subtitle? _hoveredSubtitle;

            public TimelineView(SubtitleTimelineControl owner)
            {
                _owner = owner;
                DoubleBuffered = true;

                MouseWheel += OnMouseWheel;
                Resize += (_, __) => _owner.UpdateScrollBar();



                MouseMove += OnMouseMoveTooltip;
                MouseLeave += (_, __) => _toolTip.Hide(this);
            }

            private void OnMouseMoveTooltip(object? sender, MouseEventArgs e)
            {
                var s = HitTest(e.Location);

                if (!ReferenceEquals(s, _hoveredSubtitle))
                {
                    _hoveredSubtitle = s;

                    if (s != null)
                    {
                        _toolTip.Show(
                            $"{s.Start} → {s.End}\n{s.Text}",
                            this,
                            e.Location + new Size(15, 15),
                            3000);
                    }
                    else
                    {
                        _toolTip.Hide(this);
                    }
                }
            }



            private Subtitle? HitTest(Point mouse)
            {
                PointF world = new PointF(mouse.X + ScrollX, mouse.Y);

                for (int i = 0; i < _owner.Tracks.Length; i++)
                {
                    int y = GetTrackY(i);
                    if (world.Y < y || world.Y > y + TrackHeight)
                        continue;

                    if (SubtitlesAndInfo.ThereAreSubtitles(_owner.Tracks[i].Subtitles))
                    {
                        foreach (var s in _owner.Tracks[i].Subtitles)
                        {
                            float x = (float)s.Start.TotalSeconds * _owner.PixelsPerSecond;
                            float w = (float)(s.End - s.Start).TotalSeconds * _owner.PixelsPerSecond;

                            if (world.X >= x && world.X <= x + w)
                                return s;
                        }
                    }
                }

                return null;
            }

            public float TimelineWidth => GetTimelineEndSeconds() * _owner.PixelsPerSecond;

            private float GetTimelineEndSeconds()
            {
                return _owner.Tracks
                    .Where(t => SubtitlesAndInfo.ThereAreSubtitles(t.Subtitles))
                    .SelectMany(t => t.Subtitles)
                    .Select(s => (float)s.End.TotalSeconds)
                    .DefaultIfEmpty(0f)
                    .Max();
            }

            private void OnMouseWheel(object? sender, MouseEventArgs e)
            {
                if ((ModifierKeys & Keys.Control) != 0)
                {
                    float factor = e.Delta > 0 ? 1.1f : 0.9f;
                    float mouseTime = (e.X + ScrollX) / _owner.PixelsPerSecond;
                    _owner.SetZoom(_owner.PixelsPerSecond * factor, mouseTime);
                }
                else
                {
                    ScrollX -= e.Delta;
                    ClampScroll();
                    Invalidate();
                }
            }

            public void ClampScroll()
            {
                int max = Math.Max(0, (int)(TimelineWidth - ClientSize.Width));

                // если скроллить вообще некуда
                if (max <= 0)
                {
                    ScrollX = 0;

                    _owner._hScroll.Enabled = false;
                    _owner._hScroll.Minimum = 0;
                    _owner._hScroll.Maximum = 0;
                    _owner._hScroll.Value = 0;

                    return;
                }

                _owner._hScroll.Enabled = true;

                ScrollX = Math.Clamp(ScrollX, 0, max);

                // ❗ ключевая формула для WinForms
                _owner._hScroll.Minimum = 0;
                _owner._hScroll.LargeChange = ClientSize.Width;
                _owner._hScroll.Maximum = max + _owner._hScroll.LargeChange - 1;

                // ❗ Value ВСЕГДА последним
                _owner._hScroll.Value = Math.Min(
                    ScrollX,
                    _owner._hScroll.Maximum
                );

                //int max = Math.Max(0, (int)(TimelineWidth - ClientSize.Width));
                //ScrollX = Math.Clamp(ScrollX, 0, max);
                //_owner._hScroll.Value = ScrollX;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                // ❗ вообще нет субтитров
                if (!_owner.Tracks.Any(t => SubtitlesAndInfo.ThereAreSubtitles(t.Subtitles)))
                    return;

                var g = e.Graphics;
                g.Clear(Color.White);
                g.TranslateTransform(-ScrollX, 0);

                DrawStartGuides(g);

                for (int i = 0; i < _owner.Tracks.Length; i++)
                {
                    // ❗ пустой трек просто пропускаем
                    if (!SubtitlesAndInfo.ThereAreSubtitles(_owner.Tracks[i].Subtitles))
                        continue;

                    int y = GetTrackY(i);
                    DrawTrack(g, _owner.Tracks[i], y);
                }

                DrawTimeAxis(g);
            }

            private int GetTrackY(int trackIndex)
            {
                int trackCount = _owner.Tracks.Length;

                // 0-й трек — ближе всех к шкале времени
                int visualIndex = trackCount - 1 - trackIndex;

                return TrackStartY + visualIndex * (TrackHeight + TrackSpacing);
            }

            private void DrawStartGuides(Graphics g)
            {
                if (_owner.Tracks.Length == 0)
                    return;

                var firstTrack = _owner.Tracks[0];
                if (firstTrack.Subtitles.Length == 0)
                    return;

                int top = 0;
                int bottom =
                    TrackStartY +
                    _owner.Tracks.Length * (TrackHeight + TrackSpacing);

                using var pen = new Pen(Color.LightGray)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                };

                foreach (var s in firstTrack.Subtitles)
                {
                    float x = (float)s.Start.TotalSeconds * _owner.PixelsPerSecond;
                    g.DrawLine(pen, x, top, x, bottom);
                }
            }

            private void DrawTimeAxis(Graphics g)
            {
                int baseY =
                    TrackStartY +
                    _owner.Tracks.Length * (TrackHeight + TrackSpacing) + 5;

                using var pen = new Pen(Color.Gray);

                float pps = _owner.PixelsPerSecond;

                int stepSec =
                    pps < 20 ? 10 :
                    pps < 80 ? 5 :
                    1;

                int maxSeconds = (int)Math.Ceiling(TimelineWidth / pps);

                for (int sec = 0; sec <= maxSeconds; sec += stepSec)
                {
                    float x = sec * pps;

                    g.DrawLine(pen, x, baseY, x, baseY + 6);

                    string label = TimeSpan.FromSeconds(sec).ToString();
                    g.DrawString(label, Font, Brushes.Black, x + 2, baseY + 6);
                }
            }

            private void DrawTrack(Graphics g, SubtitleTrack track, int y)
            {
                using var brush = new SolidBrush(track.Color);
                foreach (var s in track.Subtitles)
                {
                    float x = (float)s.Start.TotalSeconds * _owner.PixelsPerSecond;
                    float w = (float)(s.End - s.Start).TotalSeconds * _owner.PixelsPerSecond;
                    g.FillRectangle(brush, x, y, w, TrackHeight);
                    g.DrawRectangle(Pens.Black, x, y, w, TrackHeight);
                }
            }
        }
    }
}



    //public class SubtitleTimelineControl : UserControl
    //{
    //    // -------- Tracks --------

    //    public List<List<Subtitle>> Tracks { get; } = new()
    //    {
    //        new(), new(), new(), new(), new(), new()
    //    };

    //    // -------- Zoom --------

    //    public float PixelsPerSecond { get; private set; } = 50f;
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //    public float MinZoom { get; set; } = 5f;
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //    public float MaxZoom { get; set; } = 500f;

    //    // -------- Layout --------

    //    private const int TrackHeight = 30;
    //    private const int TrackStartY = 20;
    //    private const int TrackSpacing = 20;
    //    private const int AxisHeight = 30;

    //    // -------- Controls --------

    //    private readonly HScrollBar _hScroll;
    //    private readonly TrackBar _zoomBar;
    //    private readonly TimelineView _view;

    //    private bool _updatingZoomInternally;


    //    private void SetZoom(float newZoom, float? anchorTime = null)
    //    {
    //        newZoom = Math.Clamp(newZoom, MinZoom, MaxZoom);
    //        if (Math.Abs(PixelsPerSecond - newZoom) < 0.001f)
    //            return;


    //        PixelsPerSecond = newZoom;


    //        // sync slider (exponential mapping)
    //        _updatingZoomInternally = true;
    //        float t = (float)(Math.Log(PixelsPerSecond / MinZoom) / Math.Log(MaxZoom / MinZoom));
    //        _zoomBar.Value = Math.Clamp((int)Math.Round(t * 100), _zoomBar.Minimum, _zoomBar.Maximum);
    //        _updatingZoomInternally = false;


    //        if (anchorTime.HasValue)
    //        {
    //            _view.ScrollX = (int)(anchorTime.Value * PixelsPerSecond - _view.LastMouseX);
    //            _view.ClampScroll();
    //        }


    //        UpdateScrollBar();
    //        _view.Invalidate();
    //    }


    //    private void SetZoomFromSlider()
    //    {
    //        if (_updatingZoomInternally)
    //            return;


    //        float t = _zoomBar.Value / 100f;
    //        float zoom = MinZoom * (float)Math.Pow(MaxZoom / MinZoom, t);
    //        SetZoom(zoom);
    //    }

    //    public SubtitleTimelineControl()
    //    {
    //        Dock = DockStyle.Fill;

    //        _view = new TimelineView(this)
    //        {
    //            Dock = DockStyle.Fill
    //        };

    //        _hScroll = new HScrollBar
    //        {
    //            Dock = DockStyle.Bottom
    //        };
    //        _hScroll.Scroll += (_, __) =>
    //        {
    //            _view.ScrollX = _hScroll.Value;
    //            _view.Invalidate();
    //        };

    //        _zoomBar = new TrackBar
    //        {
    //            Dock = DockStyle.Right,
    //            Orientation = Orientation.Vertical,
    //            Minimum = 0,
    //            Maximum = 100,
    //            Value = 30,
    //            TickFrequency = 10,
    //            Width = 40
    //        };
    //        _zoomBar.ValueChanged += (_, __) =>
    //        {
    //            SetZoomFromSlider();
    //        };

    //        Controls.Add(_view);
    //        Controls.Add(_hScroll);
    //        Controls.Add(_zoomBar);

    //        UpdateScrollBar();
    //        SetZoomFromSlider();
    //    }

    //    internal void UpdateScrollBar()
    //    {
    //        int max = Math.Max(0, (int)(_view.TimelineWidth - _view.ClientSize.Width));
    //        _hScroll.Maximum = Math.Max(0, max + _hScroll.LargeChange - 1);
    //        _hScroll.LargeChange = _view.ClientSize.Width;
    //        _hScroll.SmallChange = 50;
    //        _hScroll.Value = Math.Min(_hScroll.Value, max);
    //    }

    //    // ===================================================================

    //    private class TimelineView : Control
    //    {
    //        private readonly SubtitleTimelineControl _owner;
    //        public int ScrollX;

    //        private readonly ToolTip _toolTip = new();
    //        private Subtitle? _hoveredSubtitle;

    //        public int LastMouseX { get; private set; }


    //        private void OnMouseWheel(object? sender, MouseEventArgs e)
    //        {
    //            LastMouseX = e.X;


    //            if ((ModifierKeys & Keys.Control) != 0)
    //            {
    //                float oldZoom = _owner.PixelsPerSecond;
    //                float factor = e.Delta > 0 ? 1.1f : 0.9f;
    //                float newZoom = oldZoom * factor;


    //                float mouseTime = (e.X + ScrollX) / oldZoom;
    //                _owner.SetZoom(newZoom, mouseTime);
    //            }
    //            else
    //            {
    //                ScrollX -= e.Delta;
    //                ClampScroll();
    //                Invalidate();
    //            }
    //        }

    //        public TimelineView(SubtitleTimelineControl owner)
    //        {
    //            _owner = owner;
    //            DoubleBuffered = true;

    //            MouseWheel += OnMouseWheel;
    //            MouseMove += OnMouseMoveTooltip;
    //            MouseLeave += (_, __) => _toolTip.Hide(this);
    //            Resize += (_, __) => _owner.UpdateScrollBar();
    //        }

    //        public float TimelineWidth => GetTimelineEndSeconds() * _owner.PixelsPerSecond;

    //        private float GetTimelineEndSeconds()
    //        {
    //            return _owner.Tracks.SelectMany(t => t)
    //                .Select(s => (float)s.End.TotalSeconds)
    //                .DefaultIfEmpty(0f)
    //                .Max();
    //        }

    //        public void ClampScroll()
    //        {
    //            int max = Math.Max(0, (int)(TimelineWidth - ClientSize.Width));
    //            ScrollX = Math.Max(0, Math.Min(ScrollX, max));
    //            _owner._hScroll.Value = ScrollX;
    //        }

    //        protected override void OnPaint(PaintEventArgs e)
    //        {
    //            var g = e.Graphics;
    //            g.Clear(Color.White);
    //            g.TranslateTransform(-ScrollX, 0);

    //            for (int i = 0; i < _owner.Tracks.Count; i++)
    //            {
    //                int y = TrackStartY + i * (TrackHeight + TrackSpacing);
    //                DrawTrack(g, _owner.Tracks[i], y);
    //            }

    //            DrawTimeAxis(g);
    //        }

    //        private void DrawTrack(Graphics g, List<Subtitle> subs, int y)
    //        {
    //            Brush brush = Brushes.SteelBlue;
    //            foreach (var s in subs)
    //            {
    //                float x = (float)s.Start.TotalSeconds * _owner.PixelsPerSecond;
    //                float w = (float)(s.End - s.Start).TotalSeconds * _owner.PixelsPerSecond;
    //                g.FillRectangle(brush, x, y, w, TrackHeight);
    //                g.DrawRectangle(Pens.Black, x, y, w, TrackHeight);
    //            }
    //        }

    //        private void DrawTimeAxis(Graphics g)
    //        {
    //            int baseY = TrackStartY + _owner.Tracks.Count * (TrackHeight + TrackSpacing) + 5;
    //            using var pen = new Pen(Color.Gray);

    //            float pps = _owner.PixelsPerSecond;
    //            int stepSec = pps < 20 ? 10 : pps < 80 ? 5 : 1;

    //            int maxSeconds = (int)Math.Ceiling(TimelineWidth / pps);
    //            for (int sec = 0; sec <= maxSeconds; sec += stepSec)
    //            {
    //                float x = sec * pps;
    //                g.DrawLine(pen, x, baseY, x, baseY + 6);

    //                // string label = TimeSpan.FromSeconds(sec).ToString(@"h\\:mm\\:ss");
    //                string label = TimeSpan.FromSeconds(sec).ToString();
    //                g.DrawString(label, Font, Brushes.Black, x + 2, baseY + 6);
    //            }
    //        }


    //        private void OnMouseMoveTooltip(object? sender, MouseEventArgs e)
    //        {
    //            var s = HitTest(e.Location);
    //            if (!ReferenceEquals(s, _hoveredSubtitle))
    //            {
    //                _hoveredSubtitle = s;
    //                if (s != null)
    //                    _toolTip.Show($"{s.Start} → {s.End}\n{s.Text}", this, e.Location + new Size(15, 15), 3000);
    //                else
    //                    _toolTip.Hide(this);
    //            }
    //        }

    //        private Subtitle? HitTest(Point mouse)
    //        {
    //            PointF world = new PointF(mouse.X + ScrollX, mouse.Y);

    //            for (int i = 0; i < _owner.Tracks.Count; i++)
    //            {
    //                int y = TrackStartY + i * (TrackHeight + TrackSpacing);
    //                if (world.Y < y || world.Y > y + TrackHeight) continue;

    //                foreach (var s in _owner.Tracks[i])
    //                {
    //                    float x = (float)s.Start.TotalSeconds * _owner.PixelsPerSecond;
    //                    float w = (float)(s.End - s.Start).TotalSeconds * _owner.PixelsPerSecond;
    //                    if (world.X >= x && world.X <= x + w)
    //                        return s;
    //                }
    //            }
    //            return null;
    //        }
    //    }
    //}

