using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace DiskOfDemiseWPF.Gesture.Parts.SwipeLeft
{
    public class SwipeLeftSegment2 : GestureSegment
    {
            /// <summary>
            /// Checks the gesture.
            /// </summary>
            /// <param name="skeleton">The skeleton.</param>
            /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                // //Right hand in front of right shoulder
                if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.HipCenter].Position.Y)
                {
                    // Debug.WriteLine("GesturePart 1 - Right hand in front of right shoulder - PASS");
                    // //right hand below shoulder height but above hip height
                    if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y && skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
                    {
                        // Debug.WriteLine("GesturePart 1 - right hand below shoulder height but above hip height - PASS");
                        // //right hand left of right shoulder & right of left shoulder
                        if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X)
                        {
                            // Debug.WriteLine("GesturePart 1 - right hand left of right shoulder & right of left shoulder - PASS");
                            return GesturePartResult.Succeed;
                        }

                        // Debug.WriteLine("GesturePart 1 - right hand left of right shoulder & right of left shoulder - UNDETERMINED");
                        return GesturePartResult.Pausing;
                    }

                    // Debug.WriteLine("GesturePart 1 - right hand below shoulder height but above hip height - FAIL");
                    return GesturePartResult.Fail;
                }

                // Debug.WriteLine("GesturePart 1 - Right hand in front of right shoulder - FAIL");
                return GesturePartResult.Fail;
            }
        }
}
