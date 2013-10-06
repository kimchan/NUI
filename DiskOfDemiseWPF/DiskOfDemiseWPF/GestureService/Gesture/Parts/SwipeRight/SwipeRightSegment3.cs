using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace DiskOfDemiseWPF.Gesture.Parts.SwipeRight
{
    class SwipeRightSegment3 : GestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // //left hand in front of left Shoulder
            if (skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.HipCenter].Position.Y)
            {
                // Debug.WriteLine("GesturePart 2 - left hand in front of left Shoulder - PASS");
                // //left hand below shoulder height but above hip height
                if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y && skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
                {
                    // Debug.WriteLine("GesturePart 2 - left hand below shoulder height but above hip height - PASS");
                    // //left hand left of left Shoulder
                    if (skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderRight].Position.X)
                    {
                        // Debug.WriteLine("GesturePart 2 - left hand left of left Shoulder - PASS");
                        return GesturePartResult.Succeed;
                    }

                    // Debug.WriteLine("GesturePart 2 - left hand left of left Shoulder - UNDETERMINED");
                    return GesturePartResult.Pausing;
                }

                // Debug.WriteLine("GesturePart 2 - left hand below shoulder height but above hip height - FAIL");
                return GesturePartResult.Fail;
            }

            // Debug.WriteLine("GesturePart 2 - left hand in front of left Shoulder - FAIL");
            return GesturePartResult.Fail;
        }
    }
}
