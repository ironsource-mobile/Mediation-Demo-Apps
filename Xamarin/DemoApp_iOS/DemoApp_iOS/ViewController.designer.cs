// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DemoApp_iOS
{
    [Register("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton LoadIS { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton ShowIS { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton ShowOW { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton ShowRV { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UILabel version_lbl { get; set; }

        [Action("LoadIS_TouchUpInside:")]
        [GeneratedCode("iOS Designer", "1.0")]
        partial void LoadIS_TouchUpInside(UIKit.UIButton sender);

        [Action("ShowIS_TouchUpInside:")]
        [GeneratedCode("iOS Designer", "1.0")]
        partial void ShowIS_TouchUpInside(UIKit.UIButton sender);

        [Action("ShowOW_TouchUpInside:")]
        [GeneratedCode("iOS Designer", "1.0")]
        partial void ShowOW_TouchUpInside(UIKit.UIButton sender);

        [Action("ShowRV_TouchUpInside:")]
        [GeneratedCode("iOS Designer", "1.0")]
        partial void ShowRV_TouchUpInside(UIKit.UIButton sender);

        void ReleaseDesignerOutlets()
        {
            if (LoadIS != null)
            {
                LoadIS.Dispose();
                LoadIS = null;
            }

            if (ShowIS != null)
            {
                ShowIS.Dispose();
                ShowIS = null;
            }

            if (ShowOW != null)
            {
                ShowOW.Dispose();
                ShowOW = null;
            }

            if (ShowRV != null)
            {
                ShowRV.Dispose();
                ShowRV = null;
            }

            if (version_lbl != null)
            {
                version_lbl.Dispose();
                version_lbl = null;
            }
        }
    }
}