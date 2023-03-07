using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroDB_DotNet_Driver
{
    public class ResultSet
    {
        int status = 0;
        int cursor = 0;
        int results = 0;
        int addNodes = 0;
        int addLinks = 0;
        int modifyNodes = 0;
        int modifyLinks = 0;
        int deleteNodes = 0;
        int deleteLinks = 0;
        String msg = null;
        RecordSet recordSet = null;

        public int getStatus()
        {
            return status;
        }

        public void setStatus(int status)
        {
            this.status = status;
        }

        public int getCursor()
        {
            return cursor;
        }

        public void setCursor(int cursor)
        {
            this.cursor = cursor;
        }

        public int getResults()
        {
            return results;
        }

        public void setResults(int results)
        {
            this.results = results;
        }

        public int getAddNodes()
        {
            return addNodes;
        }

        public void setAddNodes(int addNodes)
        {
            this.addNodes = addNodes;
        }

        public int getAddLinks()
        {
            return addLinks;
        }

        public void setAddLinks(int addLinks)
        {
            this.addLinks = addLinks;
        }

        public int getModifyNodes()
        {
            return modifyNodes;
        }

        public void setModifyNodes(int modifyNodes)
        {
            this.modifyNodes = modifyNodes;
        }

        public int getModifyLinks()
        {
            return modifyLinks;
        }

        public void setModifyLinks(int modifyLinks)
        {
            this.modifyLinks = modifyLinks;
        }

        public int getDeleteNodes()
        {
            return deleteNodes;
        }

        public void setDeleteNodes(int deleteNodes)
        {
            this.deleteNodes = deleteNodes;
        }

        public int getDeleteLinks()
        {
            return deleteLinks;
        }

        public void setDeleteLinks(int deleteLinks)
        {
            this.deleteLinks = deleteLinks;
        }

        public String getMsg()
        {
            return msg;
        }

        public void setMsg(String msg)
        {
            this.msg = msg;
        }

        public RecordSet getRecordSet()
        {
            return recordSet;
        }

        public void setRecordSet(RecordSet recordSet)
        {
            this.recordSet = recordSet;
        }
    }
}
