using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroDB_DotNet_Driver
{
    public class Node
    {
        long id;
        List<String> labels;
        Hashtable properties;

        public Node(long id, List<String> labels, Hashtable properties)
        {
            this.id = id;
            this.labels = labels;
            this.properties = properties;
        }

        public long getId()
        {
            return id;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public List<String> getLabels()
        {
            return labels;
        }

        public void setLabels(List<String> labels)
        {
            this.labels = labels;
        }

        public Hashtable getProperties()
        {
            return properties;
        }

        public void setProperties(Hashtable properties)
        {
            this.properties = properties;
        }
    }

}
