using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroDB_DotNet_Driver
{
    public class RecordSet
    {

        List<String> labels = null;
        List<String> types = null;
        List<String> keyNames = null;
        List<Node> nodes = null;
        List<Link> links = null;
        List<List<ColVal>> records = null;

        public RecordSet()
        {
            nodes = new List<Node>();
            links = new List<Link>();
            records = new List<List<ColVal>>();
        }

        public List<String> getLabels()
        {
            return labels;
        }

        public void setLabels(List<String> labels)
        {
            this.labels = labels;
        }

        public List<String> getTypes()
        {
            return types;
        }

        public void setTypes(List<String> types)
        {
            this.types = types;
        }

        public List<String> getKeyNames()
        {
            return keyNames;
        }

        public void setKeyNames(List<String> keyNames)
        {
            this.keyNames = keyNames;
        }

        public List<Node> getNodes()
        {
            return nodes;
        }

        public void setNodes(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public List<Link> getLinks()
        {
            return links;
        }

        public void setLinks(List<Link> links)
        {
            this.links = links;
        }

        public List<List<ColVal>> getRecords()
        {
            return records;
        }

        public void setRecords(List<List<ColVal>> records)
        {
            this.records = records;
        }
    }
}
