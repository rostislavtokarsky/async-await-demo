using System;

using UIKit;
using Shared;

namespace iOSClient
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Example.Main();
        }
    }
}
