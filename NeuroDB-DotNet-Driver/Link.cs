using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroDB_DotNet_Driver
{
    public class Link
    {
        long id;
        long startNodeId;
        long endNodeId;

        String Type;
        Hashtable properties;

        public Link(long id, long startNodeId, long endNodeId, String type, Hashtable properties)
        {
            this.id = id;
            this.startNodeId = startNodeId;
            this.endNodeId = endNodeId;
            Type = type;
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

        public long getStartNodeId()
        {
            return startNodeId;
        }

        public void setStartNodeId(long startNodeId)
        {
            this.startNodeId = startNodeId;
        }

        public long getEndNodeId()
        {
            return endNodeId;
        }

        public void setEndNodeId(long endNodeId)
        {
            this.endNodeId = endNodeId;
        }

        public String getType()
        {
            return Type;
        }

        public void setType(String type)
        {
            Type = type;
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
