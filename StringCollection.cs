using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace mp3packer
{
    public class StringCollection : CollectionBase
    {
        public int Add(String objItemToAdd)
        {
            return (List.Add(objItemToAdd));
        }

        public String this[int index]
        {
            get
            {
                return ((String)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public void Remove(String objItemToRemove)
        {
            List.Remove(objItemToRemove);
        }

        public int IndexOf(String value)
        {
            return (List.IndexOf(value));
        }

    }
}
