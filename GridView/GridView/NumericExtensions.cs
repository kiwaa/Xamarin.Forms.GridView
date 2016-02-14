// Decompiled with JetBrains decompiler
// Type: Xamarin.Forms.NumericExtensions
// Assembly: Xamarin.Forms.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03441CEF-5527-44E5-B4BE-55AFB61DFF41
// Assembly location: D:\projects\oDesk\44sounds2\src\packages\Xamarin.Forms.2.0.1.6505\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+MonoTouch10+Xamarin.iOS10\Xamarin.Forms.Core.dll

using System;

namespace Xamarin.Forms
{
    internal static class NumericExtensions
    {
        public static double Clamp(this double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }

        public static int Clamp(this int self, int min, int max)
        {
            return Math.Min(max, Math.Max(self, min));
        }
    }
}
