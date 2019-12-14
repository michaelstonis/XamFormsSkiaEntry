using System;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamFormsSkiaEntry;
using XamFormsSkiaEntry.Android;
using XamFormsSkiaEntry.Droid;

[assembly: ExportRenderer(typeof(SkiaEntry), typeof(SkiaEntryDroidRenderer))]
namespace XamFormsSkiaEntry.Android
{
    public class SkiaEntryDroidRenderer : ViewRenderer<SkiaEntry, SkiaEntryDroid>
    {
        public SkiaEntryDroidRenderer(Context context)
            : base(context)
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
                    this.SetNativeControl(new SkiaEntryDroid(this.Context));
                }
            }
        }
    }
}
