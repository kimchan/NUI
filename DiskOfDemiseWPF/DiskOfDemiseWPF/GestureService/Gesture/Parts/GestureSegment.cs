using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using DiskOfDemiseWPF.Gesture;

namespace DiskOfDemiseWPF.Gesture.Parts
{
    /// <summary>
    /// the gesture part result
    /// </summary>
    public enum GesturePartResult : byte
    {
        Fail = 0,
        Succeed = 1,
        Pausing = 2
    }

    public interface GestureSegment
    {
        GesturePartResult CheckGesture(Skeleton skeleton);
    }


}
