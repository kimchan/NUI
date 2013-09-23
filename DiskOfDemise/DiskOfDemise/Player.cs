using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskOfDemise
{
    class Player
    {
        private String color = "";
        private ArrayList bodyParts = new ArrayList(5);

        public Player(String color)
        {
            this.color = color;
            bodyParts.Add("head");
            bodyParts.Add("rightArm");
            bodyParts.Add("leftArm");
            bodyParts.Add("rightLeg");
            bodyParts.Add("leftLeg");
        }

        public void removeLimb(String limb)
        {
            if (bodyParts.IndexOf(limb) != -1)
            {
                bodyParts.Remove(limb);
            }
        }

        public void showBodyParts()
        {
            Console.WriteLine(color);
            for(int i = 0; i < bodyParts.Count; i++)
            {
                Console.WriteLine(bodyParts[i]);
            }
        }
    }
}
