using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroDB_DotNet_Driver
{
    public class ColVal
    {
        Object val;
        int type;
        int aryLen = 0;
        double getNum()
        {
            return Double.Parse((string) val);
        }

        double[] getNumArray()
        {
            return (double[])val;
        }

        String getString()
        {
            return (string)val;
        }

        String[] getStringArry()
        {
            return (String[])val;
        }

        Node getNode()
        {
            return (Node)val;
        }

        Link getLink()
        {
            return (Link)val;
        }

        List<Object> getPath()
        {
            return (List<Object>)val;
        }

        public int getAryLen()
        {
            return aryLen;
        }

        public void setAryLen(int aryLen)
        {
            this.aryLen = aryLen;
        }

        public Object getVal()
        {
            return val;
        }

        public void setVal(Object val)
        {
            this.val = val;
        }

        public int getType()
        {
            return type;
        }

        public void setType(int type)
        {
            this.type = type;
        }
    }
}
