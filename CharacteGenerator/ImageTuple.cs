using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacteGenerator
{
    class ImageTuple
    {
        public string imageID;
        public List<ControlledVocabualry> controlledVocabualries;
        public ImageTuple()
        {
            controlledVocabualries = new List<ControlledVocabualry>();
        }
    }
}
