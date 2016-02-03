using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace mp3packer
{

    public class SoundfileCollection : CollectionBase
    {
        public int Add(Soundfile objItemToAdd)
        {
            return (List.Add(objItemToAdd));
        }

        public Soundfile this[int index]
        {
            get
            {
                return ((Soundfile)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public void Remove(Soundfile objItemToRemove)
        {
            List.Remove(objItemToRemove);
        }

    }
}
