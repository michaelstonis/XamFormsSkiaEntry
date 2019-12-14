using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace XamFormsSkiaEntry
{
    public class CustomDrawer : SkiaSharp.Views.Forms.SKCanvasView
    {
        public CustomDrawer()
        {
            this.EnableTouchEvents = true;
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;

            canvas.DrawColor(SKColors.Chartreuse);
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            base.OnTouch(e);


        }
    }
}
