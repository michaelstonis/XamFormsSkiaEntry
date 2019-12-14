using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using SkiaSharp;
using SkiaSharp.Views.Android;

namespace XamFormsSkiaEntry.Droid
{
    public class SkiaEntryDroid : SkiaSharp.Views.Android.SKCanvasView, KeyEvent.ICallback
    {
        private string _entryText = string.Empty;
        private SKRect _textRect = SKRect.Empty;

        public bool HasText => !string.IsNullOrEmpty(_entryText);

        public SkiaEntryDroid(Context context)
            : base(context)
        {
            this.IgnorePixelScaling = true;

            this.Focusable = true;
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            switch (keyCode)
            {
                case Keycode.Del:
                    if(HasText)
                    {
                        _entryText = _entryText.Substring(0, _entryText.Length - 1);
                    }

                    break;
                    
                default:
                    _entryText += (char)e.UnicodeChar;
                    break;
            }
            
            this.Invalidate();

            return base.OnKeyUp(keyCode, e);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {

            if(e.ActionMasked == MotionEventActions.Down)
            {
                this.RequestFocusFromTouch();
            }

            return base.OnTouchEvent(e);
        }

        protected override void OnFocusChanged(bool gainFocus, [GeneratedEnum] FocusSearchDirection direction, Rect previouslyFocusedRect)
        {
            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);

            if(this.Context.GetSystemService(Context.InputMethodService) is InputMethodManager imm)
            {
                if (gainFocus)
                {
                    imm.ShowSoftInput(this, ShowFlags.Implicit);
                }
                else
                {
                    imm.HideSoftInputFromWindow(this.WindowToken, HideSoftInputFlags.None);
                }
            }

        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;

            var canvasSize = e.Info.Rect;

            using (var paint = new SKPaint())
            {
                paint.Color = SKColors.Tomato;
                canvas.DrawPaint(paint);

                paint.Color = SKColors.GhostWhite;
                paint.IsAntialias = true;
                paint.TextSize = 16f;

                using (new SKAutoCanvasRestore(canvas))
                {
                    var width = paint.MeasureText(_entryText, ref _textRect);

                    canvas
                        .Translate(
                            -(_textRect.Width * .5f),
                            ((-paint.FontMetrics.Ascent + paint.FontMetrics.Descent) / 2f) - paint.FontMetrics.Descent);

                    canvas.DrawText(_entryText, canvasSize.MidX, canvasSize.MidY, paint);
                }
            }
        }
    }
}
