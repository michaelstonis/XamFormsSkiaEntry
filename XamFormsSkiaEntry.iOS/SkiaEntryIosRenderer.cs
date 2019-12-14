using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamFormsSkiaEntry;
using XamFormsSkiaEntry.iOS;

[assembly: ExportRenderer(typeof(SkiaEntry), typeof(SkiaEntryIosRenderer))]
namespace XamFormsSkiaEntry.iOS
{
    public class SkiaEntryIosRenderer : ViewRenderer<SkiaEntry, SkiaEntryIos>
    {
        public SkiaEntryIosRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SkiaEntry> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement != null)
            {
                //shut down other stuff like disposals and event handlers
            }

            if(e.NewElement != null)
            {
                if(this.Control == null)
                {
                    this.SetNativeControl(new SkiaEntryIos());
                }
            }
        }
    }
}
