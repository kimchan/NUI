using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using DiskOfDemiseWPF.Gesture;

namespace DiskOfDemiseWPF.Gesture.Parts.SwipeRight
{
    class KickLeftSegment : GestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // right ankle in front of right knee
            if (skeleton.Joints[JointType.AnkleLeft].Position.Z < skeleton.Joints[JointType.KneeLeft].Position.Z)
            {
                // right knee in front of hip center
                if (skeleton.Joints[JointType.KneeLeft].Position.Z < skeleton.Joints[JointType.HipCenter].Position.Z)
                {
                    // distance from hip to knee is "small"
                    if (Math.Abs(skeleton.Joints[JointType.HipCenter].Position.Y - skeleton.Joints[JointType.AnkleLeft].Position.Y) < 0.33)
                    {
                        return GesturePartResult.Succeed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }
}