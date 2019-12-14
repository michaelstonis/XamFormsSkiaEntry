using System;
using SkiaSharp;
using SkiaSharp.Views.iOS;
using UIKit;

namespace XamFormsSkiaEntry.iOS
{
    public class SkiaEntryIos : SkiaSharp.Views.iOS.SKCanvasView, IUIKeyInput
    {
        private string _entryText = string.Empty;
        private SKRect _textRect = SKRect.Empty;

        UITapGestureRecognizer _tapGesture;

        public SkiaEntryIos()
        {
            //Skia Configuration
            this.IgnorePixelScaling = true;

            //iOS Configuration
            _tapGesture = new UITapGestureRecognizer();
            _tapGesture.AddTarget(() => this.BecomeFirstResponder());

            this.AddGestureRecognizer(_tapGesture);
        }

        //We can respond to interaction
        public override bool CanBecomeFirstResponder => true;

        public bool HasText => !string.IsNullOrEmpty(_entryText);

        public UITextAutocapitalizationType AutocapitalizationType { get; set; }
        public UITextAutocorrectionType AutocorrectionType { get; set; }
        public UIKeyboardType KeyboardType { get; set; }
        public UIKeyboardAppearance KeyboardAppearance { get; set; }
        public UIReturnKeyType ReturnKeyType { get; set; }
        public bool EnablesReturnKeyAutomatically { get; set; }
        public bool SecureTextEntry { get; set; }
        public UITextSpellCheckingType SpellCheckingType { get; set; }

        public void DeleteBackward()
        {
            if(!HasText)
            {
                return;
            }

            _entryText = _entryText.Substring(0, _entryText.Length - 1);

            this.SetNeedsDisplay();
        }

        public void InsertText(string text)
        {
            _entryText += text;

            this.SetNeedsDisplay();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;

            var canvasSize = e.Info.Rect;

            using(var paint = new SKPaint())
            {
                paint.Color = SKColors.Tomato;
                canvas.DrawPaint(paint);

                paint.Color = SKColors.GhostWhite;
                paint.IsAntialias = true;
                paint.TextSize = 16f;

                using(new SKAutoCanvasRestore(canvas))
                {
                    var width = paint.MeasureText(_entryText, ref _textRect);

                    var halfTextWidth = _textRect.Width * .5f;

                    canvas
                        .Translate(
                            -halfTextWidth,
                            ((-paint.FontMetrics.Ascent + paint.FontMetrics.Descent) / 2f) - paint.FontMetrics.Descent);

                    canvas.DrawText(_entryText, canvasSize.MidX, canvasSize.MidY, paint);

                    //SKRect cursorRect = SKRect.Empty;

                    //paint.MeasureText("X", ref cursorRect);

                    //canvas.DrawRoundRect(
                    //    new SKRoundRect(
                    //        new SKRect(canvasSize.MidX + width, canvasSize.MidY - cursorRect.Height, canvasSize.MidX + width + cursorRect.Width, canvasSize.MidY + cursorRect.Height), 2f, 2f),
                    //        paint);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(_tapGesture != null)
                {
                    this.RemoveGestureRecognizer(_tapGesture);
                    _tapGesture?.Dispose();
                    _tapGesture = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
