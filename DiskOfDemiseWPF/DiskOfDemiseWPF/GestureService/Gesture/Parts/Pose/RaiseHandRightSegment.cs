using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using DiskOfDemiseWPF.Gesture;

namespace DiskOfDemiseWPF.Gesture.Parts
{
    class RaiseHandRightSegment : GestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
            {
                if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ShoulderCenter].Position.Y)
                {
                    if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Head].Position.Y)
                    {
                        return GesturePartResult.Succeed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }
}